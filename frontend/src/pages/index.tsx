import React from "react";
import MainLayout from "../components/mainLayout/mainLayout";
import BaselineGrid from "../components/baselineGrid/baselineGrid";
import {NextPage} from "next";

const Index: NextPage<{ number: number, api?: string }> = ({ number , api= "хуй"}) => {
  return (
    <MainLayout>
      <h1>Index page: {api}</h1>
      <BaselineGrid />
    </MainLayout>
  );
};

Index.getInitialProps = async (ctx) => {
  let api;
  if (ctx.res) {
    //@ts-ignore
    api = process.apiUrl;
  }
  const number = Math.random();
  return { number, api };
};

export default Index;
