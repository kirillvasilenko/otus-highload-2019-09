import ServerCookie from "next-cookies";
import Router from "next/router";
import React, { Component } from "react";
import Token, { TOKEN } from "../../utils/token";
import { NextPageContext } from "next";


export function privateRoute(WrappedComponent: any) {
  return class extends Component {
    static async getInitialProps(ctx: NextPageContext) {
      const token = Token.fromCtx(ctx);
      if (token.isExpired) {
        ctx.res?.writeHead(302, {
          Location: "/login",
        });
        ctx.res?.end();
      }
      if (WrappedComponent.getInitialProps) return WrappedComponent.getInitialProps(ctx);
      return {};
    }

    render() {
      return <WrappedComponent {...this.props} />;
    }
  };
}