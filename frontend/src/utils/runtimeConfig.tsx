import getConfig from "next/config";

type RuntimeConfig = {
  apiUrl?: string;
}

const { publicRuntimeConfig, serverRuntimeConfig } = getConfig();

export const getPublicRuntimeConfig = (): RuntimeConfig => {
  return publicRuntimeConfig;
};

export const getServerRuntimeConfig = (): RuntimeConfig => {
  return serverRuntimeConfig;
};

export const getApiUrl = (): string | undefined => {
  const { apiUrl: serverApiUrl } = getServerRuntimeConfig();
  const { apiUrl } = getPublicRuntimeConfig();

  return serverApiUrl || apiUrl;
};