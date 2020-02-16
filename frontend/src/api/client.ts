import { Client, RegistrationUserResult } from "@kirillamurskiy/socialnetwork-client";

import { getPublicRuntimeConfig } from "../utils/runtimeConfig";
import unfetch from "isomorphic-unfetch";
import { NextPageContext } from "next";
import Token from "../utils/token";

const publicConfig = getPublicRuntimeConfig();

class MyClient extends Client {
  ctx: { ctx?: NextPageContext } = {};

  constructor(baseUrl?: string) {
    super(baseUrl, {
      fetch: async (url: RequestInfo, init?: RequestInit) => {
        const ctx = this.getCtx();

        console.info(this.ctx, "fetch ctx");

        const token = Token.get(ctx);

        return unfetch(url, {
          ...init,
          headers: {
            ...init?.headers,
            "Authorization": token.authorisationString
          }
        });
      }
    })
  }

  getCtx = (): NextPageContext | undefined => this.ctx.ctx;

  setCtx = (ctx: NextPageContext) => {
    console.info("set ctx", ctx);
    this.ctx = { ctx };
  };
}

const apiClient = new MyClient(publicConfig.apiUrl);

export default apiClient;