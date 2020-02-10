import getConfig from "next/config";

type PublicRuntimeConfig = {
  apiUrl?: string;
}

const { publicRuntimeConfig } = getConfig();

export const getPublicRuntimeConfig = (): PublicRuntimeConfig => {
  return publicRuntimeConfig;
};