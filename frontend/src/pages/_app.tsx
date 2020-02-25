import React from "react";
import App from "next/app";
import { ThemeProvider } from "@material-ui/core/styles";
import CssBaseline from "@material-ui/core/CssBaseline";
import theme from "../theme";
import { AppContext } from "next/dist/pages/_app";
import { ApiException } from "@kirillamurskiy/socialnetwork-client";
import Error from "next/error";

class MyApp extends App<{ e?: ApiException }> {
  componentDidMount() {
    // Remove the server-side injected CSS.
    const jssStyles = document.querySelector("#jss-server-side");
    jssStyles?.parentElement?.removeChild(jssStyles);
  }

  static getInitialProps = async (appContext: AppContext) => {
    try {
      const appProps = await App.getInitialProps(appContext);
      return { ...appProps };
    } catch (e) {
      if (appContext.ctx.res) {
        appContext.ctx.res.statusCode = e.status;
      }
      return { e, pageProps: undefined };
    }
  };

  render() {
    const { Component, pageProps, e } = this.props;

    return (
      <ThemeProvider theme={theme}>
        <CssBaseline/>
        {e ? <Error statusCode={e?.status}/> : <Component {...pageProps} />}
      </ThemeProvider>
    );
  }
}

export default MyApp;