import { UsersClient } from "@kirillamurskiy/socialnetwork-client";
import { getPublicRuntimeConfig } from "../utils/runtimeConfig";
import unfetch from "isomorphic-unfetch";

const publicConfig = getPublicRuntimeConfig();
const userClient = new UsersClient(publicConfig.apiUrl, {
  fetch: (url, init) => {

    //

    return unfetch(url, init);
  }
});

export default userClient;