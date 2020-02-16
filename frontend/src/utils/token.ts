import { TokenDto } from "@kirillamurskiy/socialnetwork-client";
import fromUnixTime from "date-fns/fromUnixTime";
import isAfter from "date-fns/isAfter";
import serverCookie from "next-cookies";
import { NextPageContext } from "next";
import jsCookie from "js-cookie";
import apiClient from "../api/client";

export const TOKEN = "token";

export const saveToken = (token: TokenDto) => {
  jsCookie.set(TOKEN, JSON.stringify(token));
};

export const getToken = (): TokenDto | undefined=> {
  const tokenString = localStorage.getItem(TOKEN);
  if (tokenString) {
    return JSON.parse(tokenString) as TokenDto;
  }
};

export default class Token {
  token?: TokenDto;

  constructor(token?: TokenDto) {
    this.token = {...token};
  }

  get authorisationString() {
    console.info(this.token);
    return `Bearer ${this.token?.accessToken}`
  };

  get isExpired() {
    if (this.token?.accessTokenExpiresIn) {
      const expireDate = fromUnixTime(this.token.accessTokenExpiresIn);
      return isAfter(new Date(), expireDate);
    }

    return true;
  }

  refreshToken = async () => {
    if (this.token?.refreshTokenExpiresIn && this.token?.refreshToken) {
      const expireDate = fromUnixTime(this.token.refreshTokenExpiresIn);
      if (!isAfter(new Date(), expireDate)) {
        const token = await apiClient.refreshToken(this.token?.refreshToken);
        this.token = token;
      }
    }
  };

  static fromCtx = (ctx: NextPageContext): Token => {
    const token = serverCookie(ctx)[TOKEN] as TokenDto;
    return new Token(token);
  };

  static fromBrowser = () => {
    const token = jsCookie.getJSON(TOKEN);
    return new Token(token);
  };

  static get = (ctx?: NextPageContext): Token => {
    if (ctx) return Token.fromCtx(ctx);
    return Token.fromBrowser();
  }
}