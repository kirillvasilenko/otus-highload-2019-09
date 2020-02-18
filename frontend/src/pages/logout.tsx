import React from "react";
import { NextPage } from "next";
import Token from "../utils/token";
import { isServer } from "../utils/isBrowser";
import { LOGIN_ROUTE } from "../routes.constants";
import Router from "next/router";

const Login: NextPage = () => {
  return null;
};

Login.getInitialProps = async (ctx) => {
  const token = Token.makeTokenServer(ctx);
  token.delete();

  if (isServer()) {
    ctx.res?.writeHead(302, {
      Location: LOGIN_ROUTE
    });
    ctx.res?.end();
  } else {
    await Router.push(LOGIN_ROUTE);
  }

  return {};
};

export default Login;
