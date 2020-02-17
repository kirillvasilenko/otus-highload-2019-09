import { TokenDto } from "@kirillamurskiy/socialnetwork-client";
import jsCookie from "js-cookie";
import { NextPageContext } from "next";
import { destroyCookie, parseCookies, setCookie } from "nookies";

export const TOKEN = "token";

export interface TokenStorage {
  get: () => TokenDto;
  set: (token: TokenDto) => void;
  delete: () => void;
}

export class TokenStorageServer implements TokenStorage {
  _ctx: NextPageContext;

  constructor(ctx: NextPageContext) {
    this._ctx = ctx;
  }

  set = (token: TokenDto) => {
    setCookie(this._ctx, TOKEN, JSON.stringify(token), { path: "/" });
  };

  get = () => {
    const tokenString = parseCookies(this._ctx)[TOKEN];
    return JSON.parse(tokenString);
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