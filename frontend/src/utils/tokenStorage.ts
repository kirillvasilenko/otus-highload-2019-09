import { TokenDto } from "@kirillamurskiy/socialnetwork-client";
import jsCookie from "js-cookie";
import { NextPageContext } from "next";
import { destroyCookie, parseCookies, setCookie } from "nookies";

export const TOKEN = "token";

export interface TokenStorage {
  get: () => TokenDto | undefined;
  set: (token: TokenDto) => void;
  delete: () => void;
}

export class TokenStorageServer implements TokenStorage {
  _ctx: NextPageContext;

  token?: TokenDto;

  constructor(ctx: NextPageContext) {
    this._ctx = ctx;
  }

  set = (token: TokenDto) => {
    this.token = token;
    setCookie(this._ctx, TOKEN, JSON.stringify(token), { path: "/" });
  };

  get = () => {
    if (this.token === undefined) {
      const tokenString = parseCookies(this._ctx)[TOKEN];
      if (tokenString) {
        this.token = JSON.parse(tokenString) as TokenDto;
      }
    }
    return this.token;
  };

  delete = () => {
    destroyCookie(this._ctx, TOKEN);
  };
}

export class TokenStorageBrowser implements TokenStorage {
  set = (token: TokenDto) => {
    jsCookie.set(TOKEN, JSON.stringify(token));
  };

  get = () => {
    return jsCookie.getJSON(TOKEN);
  };

  delete = () => {
    jsCookie.remove(TOKEN);
  };
}