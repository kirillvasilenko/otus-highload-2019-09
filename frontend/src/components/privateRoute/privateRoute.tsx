import Router from "next/router";
import React, { Component } from "react";
import { NextPageContext } from "next";
import { LOGIN_ROUTE } from "../../routes.constants";
import { isServer } from "../../utils/isBrowser";

export function privateRoute(WrappedComponent: any) {
  return class extends Component {
    static async getInitialProps(ctx: NextPageContext) {
      if (WrappedComponent.getInitialProps) {
        try {
          return await WrappedComponent.getInitialProps(ctx);
        } catch (e) {
          console.info(e);
          if (isServer()) {
            ctx.res?.writeHead(302, {
              Location: LOGIN_ROUTE
            });
            ctx.res?.end();
          } else {
            await Router.push(LOGIN_ROUTE);
          }
          return {};
        }
      }
      return {};
    }

    render() {
      return <WrappedComponent {...this.props} />;
    }
  };
}