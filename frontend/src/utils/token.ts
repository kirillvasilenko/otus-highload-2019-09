import { TokenDto } from "@kirillamurskiy/socialnetwork-client";
import fromUnixTime from "date-fns/fromUnixTime";
import isAfter from "date-fns/isAfter";
const TOKEN = "token";

export const saveToken = (token: TokenDto) => {
  localStorage.setItem(TOKEN, JSON.stringify(token));
};

export const getToken = (): TokenDto | undefined => {
  try {
    const tokenString = localStorage.getItem(TOKEN);
    if (tokenString !== null) {
      return JSON.parse(tokenString)
    }
  } catch (e) {

  }
};

export const getAccessToken = () => {
  return getToken()?.accessToken;
};

export const checkExpireTime = (time?: number) => {
  if (time) {
    const expireTime = fromUnixTime(time);
    const date = new Date();
  }
};

const time = getToken()?.accessTokenExpiresIn;
checkExpireTime(time);