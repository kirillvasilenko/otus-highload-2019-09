import { RegistrationClient} from "@kirillamurskiy/socialnetwork-client";
import { getPublicRuntimeConfig } from "../utils/runtimeConfig";
import unfetch from "isomorphic-unfetch";

const publicConfig = getPublicRuntimeConfig();
const registrationClient = new RegistrationClient(publicConfig.apiUrl, {
  fetch: unfetch
});

export default registrationClient;