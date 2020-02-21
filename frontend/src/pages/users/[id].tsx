import React from "react";
import MainLayout from "../../components/mainLayout/mainLayout";
import clientFactory from "../../api/clientFactory";
import { NextPage, NextPageContext } from "next";
import { UserDto } from "@kirillamurskiy/socialnetwork-client";


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

interface UserContext extends NextPageContext {
  query: { id: string }
}

Index.getInitialProps = async (ctx: UserContext) => {
  const { id } = ctx.query;
  const users = clientFactory.users();
  const user = await users.getUser(parseInt(id));
  return { user };
};

export default Index;
