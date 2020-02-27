import { AccountClient, AuthClient, RegistrationClient, UsersClient } from "@kirillamurskiy/socialnetwork-client";
import { getApiUrl } from "../utils/runtimeConfig";
import unfetch from "isomorphic-unfetch";
import Token from "../utils/token";

class Fetcher {
  token: Token;
  createAuthClient: () => AuthClient;

  constructor(token: Token, createAuthClient: () => AuthClient) {
    this.token = token;
    this.createAuthClient = createAuthClient;
  }

  fetch = async (url: Request, init?: RequestInit) => {
    if (this.token.isAccessTokenExpired()) {
      const authClient = this.createAuthClient();
      if (!this.token.isRefreshTokenExpired()) {
        const newTokenDto = await authClient.refreshToken(this.token.refreshToken);
        this.token.update(newTokenDto);
      }
    }

    return unfetch(url, {
      ...init,
      headers: {
        ...init?.headers,
        "Authorization": `Bearer ${this.token.authorisationString}`
      }
    });
  }
}

class ClientFactory {
  baseUrl?: string;

  constructor(baseUrl?: string) {
    this.baseUrl = baseUrl;
  }

  makeAuthClient = () => {
    return new AuthClient(this.baseUrl, {
      fetch: unfetch
    })
  };

  registration = () => {
    return new RegistrationClient(this.baseUrl, {
      fetch: unfetch
    })
  };

  users = () => {
    return new UsersClient(this.baseUrl, {
      fetch: unfetch
    })
  };

  makeAccountClient = (token: Token) => {
    return new AccountClient(this.baseUrl, new Fetcher(token, this.makeAuthClient));
  }
}

const apiUrl = getApiUrl();

const clientFactory = new ClientFactory(apiUrl);
export default clientFactory;