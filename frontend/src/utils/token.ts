import { TokenDto } from "@kirillamurskiy/socialnetwork-client";
import fromUnixTime from "date-fns/fromUnixTime";
import isAfter from "date-fns/isAfter";
import { NextPageContext } from "next";
import { TokenStorage, TokenStorageBrowser, TokenStorageServer } from "./tokenStorage";

export default class Token {
  private storage: TokenStorage;

  constructor(storage: TokenStorage) {
    this.storage = storage;
  }

  static makeTokenServer = (ctx: NextPageContext) => {
    return new Token(new TokenStorageServer(ctx));
  };

  // window нужен для того чтобы случайно не вызвать метод на сервере
  static makeTokenBrowser = (window: Window) => {
    return new Token(new TokenStorageBrowser());
  };

  get authorisationString() {
    return this.storage.get()?.accessToken || "";
  };

  static checkToken = (token?: number) => {
    if (token) {
      const expireDate = fromUnixTime(token);
      return isAfter(new Date(), expireDate);
    }

    return true;
  };

  isExist = () => {
    return this.storage.get() !== undefined;
  };

  isAccessTokenExpired = () => {
    return Token.checkToken(this.storage.get()?.accessTokenExpiresIn);
  };

  isRefreshTokenExpired = () => {
    return Token.checkToken(this.storage.get()?.refreshTokenExpiresIn);
  };

  get refreshToken(): string {
    return this.storage.get()?.refreshToken || "";
  };

  update = (token: TokenDto) => {
    this.storage.set(token);
  };

  delete = () => {
    this.storage.delete();
  }
}