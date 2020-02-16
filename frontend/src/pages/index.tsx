import React from "react";
import MainLayout from "../components/mainLayout/mainLayout";
import { privateRoute } from "../components/privateRoute/privateRoute";
import { NextPage } from "next";
import Token from "../utils/token";
import apiClient from "../api/client";
import { UserDto } from "@kirillamurskiy/socialnetwork-client";

const Index: NextPage<{ user: UserDto }> = ({ user }) => {
  return (
    <MainLayout>
      <h1>{user.email}</h1>
    </MainLayout>
  );
};

Index.getInitialProps = async (ctx) => {
  console.info("Index ctx", ctx);

  apiClient.setCtx(ctx);
  const user = await apiClient.getAccount();
  return { user };
};

export default privateRoute(Index);
