import { Client} from "@KirillAmurskiy/socialnetwork-client";

import { getPublicRuntimeConfig } from "../utils/runtimeConfig";
import unfetch from "isomorphic-unfetch";

const publicConfig = getPublicRuntimeConfig();
const apiClient = new Client(publicConfig.apiUrl, {
  fetch: unfetch
});

export default apiClient;