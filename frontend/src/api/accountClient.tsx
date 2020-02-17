import { AccountClient } from "@kirillamurskiy/socialnetwork-client";
import { getPublicRuntimeConfig } from "../utils/runtimeConfig";
import unfetch from "isomorphic-unfetch";

const publicConfig = getPublicRuntimeConfig();
const accountClient = new AccountClient(publicConfig.apiUrl, {
  fetch: (url, init) => {



    return unfetch(url, {...init, headers: {
        ...init?.headers,
        "Authorization": ""
      }});
  }
});

export default accountClient;