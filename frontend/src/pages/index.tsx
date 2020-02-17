import React, { useEffect } from "react";
import MainLayout from "../components/mainLayout/mainLayout";
import { privateRoute } from "../components/privateRoute/privateRoute";
import { NextPage } from "next";
import { UserDto } from "@kirillamurskiy/socialnetwork-client";
import Link from "next/link";
import { LOGIN_ROUTE } from "../routes.constants";
import Token from "../utils/token";
import clientFactory from "../api/clientFactory";

const Index: NextPage<{ user?: UserDto }> = ({ user }) => {
  if (!user) return null;

  return (
    <MainLayout>
      <h1>{user.email}</h1>
      <Link href={LOGIN_ROUTE}><a>Login page</a></Link>
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
