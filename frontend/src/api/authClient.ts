import { AuthClient } from "@kirillamurskiy/socialnetwork-client";
import { getPublicRuntimeConfig } from "../utils/runtimeConfig";
import unfetch from "isomorphic-unfetch";

const publicConfig = getPublicRuntimeConfig();
const authClient = new AuthClient(publicConfig.apiUrl, {
  fetch: unfetch
});

export default authClient;