import React from "react";
import MainLayout from "../components/mainLayout/mainLayout";
import { privateRoute } from "../components/privateRoute/privateRoute";
import { NextPage } from "next";
import { UserDto } from "@kirillamurskiy/socialnetwork-client";
import Token from "../utils/token";
import clientFactory from "../api/clientFactory";

const Index: NextPage<{ user?: UserDto }> = ({ user }) => {
  if (!user) return null;

  return (
    <MainLayout>
      <h1>{user?.givenName} {user?.familyName}</h1>
      <p>{user?.email}</p>
      <p>{user?.age}</p>
      <p>{user?.city}</p>
      <p>{user?.interests}</p>
    </MainLayout>
  );
};

Index.getInitialProps = async (ctx) => {
  const token = Token.makeTokenServer(ctx);
  const accountClient = clientFactory.makeAccountClient(token);
  const user = await accountClient.getAccount();
  return { user };
};

export default privateRoute(Index);
