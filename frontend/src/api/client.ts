import { Client, RegistrationUserResult } from "@kirillamurskiy/socialnetwork-client";

import { getPublicRuntimeConfig } from "../utils/runtimeConfig";
import unfetch from "isomorphic-unfetch";

const publicConfig = getPublicRuntimeConfig();

const getToken = () => {
  const dataString = localStorage.getItem("token");
  if (dataString) {
    const data = JSON.parse(dataString) as RegistrationUserResult;
    return data.token?.accessToken;
  }
  return undefined;
};

const refreshToken = async () => {
  const dataString = localStorage.getItem("token");
  if (dataString) {
    const data = JSON.parse(dataString) as RegistrationUserResult;

    try {
      const request = await apiClient.refreshToken(data.token?.refreshToken || null);
      data.token = request;
      localStorage.setItem("token", JSON.stringify(data));
    } catch (e) {
      console.error(e);
    }
  }
};

const apiClient = new Client(publicConfig.apiUrl, {
  fetch: async (url, init) => {

    if (false) {
      await refreshToken();
    }

    const token = getToken();

    return unfetch(url, {
      ...init,
      headers: {
        ...init?.headers,
        "Authorization": `Bearer ${token}`
      }
    });
  }
});

export default apiClient;