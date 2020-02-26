import React from "react";
import App, { AppInitialProps, AppProps } from "next/app";
import { ThemeProvider } from "@material-ui/core/styles";
import CssBaseline from "@material-ui/core/CssBaseline";
import theme from "../theme";
import { AppContext } from "next/dist/pages/_app";
import { ApiException, UserDto } from "@kirillamurskiy/socialnetwork-client";
import Error from "next/error";
import clientFactory from "../api/clientFactory";
import Token from "../utils/token";

type MyAppProps = { e?: ApiException, user: UserDto } & AppProps;

const UserContext = React.createContext<UserDto | undefined>(undefined);
export const useUser = () => React.useContext<UserDto | undefined>(UserContext);

class MyApp extends App<MyAppProps> {
  componentDidMount() {
    // Remove the server-side injected CSS.
    const jssStyles = document.querySelector("#jss-server-side");
    jssStyles?.parentElement?.removeChild(jssStyles);
  }

  static getInitialProps = async (appContext: AppContext) => {
    let props: AppInitialProps & { e?: ApiException, user?: UserDto} = { pageProps: undefined };
    try {
      const appProps = await App.getInitialProps(appContext);
      props = { ...props, ...appProps };
    } catch (e) {
      if (appContext.ctx.res) {
        appContext.ctx.res.statusCode = e.status;
      }
      props = { ...props, e, pageProps: undefined };
    }

    try {
      const token = Token.makeTokenServer(appContext.ctx);
      if (token.isExist()) {
        const accounts = clientFactory.makeAccountClient(token);
        const user = await accounts.getAccount();
        props = { ...props, user };
      }
    } catch (e) {
      console.error(e);
    }
    return props;
  };

  render() {
    const { Component, pageProps, e, user } = this.props;
    return (
      <ThemeProvider theme={theme}>
        <CssBaseline/>
        <UserContext.Provider value={user}>
        {e ? <Error statusCode={e?.status}/> : <Component {...pageProps} />}
        </UserContext.Provider>
      </ThemeProvider>
    );
  }
}

export default MyApp;