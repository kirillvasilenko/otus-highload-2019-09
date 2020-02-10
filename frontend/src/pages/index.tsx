import React from "react";
import MainLayout from "../components/mainLayout/mainLayout";
import BaselineGrid from "../components/baselineGrid/baselineGrid";
import {NextPage} from "next";
import { getPublicRuntimeConfig } from "..//utils/runtimeConfig";

const Index: NextPage<{ number: number, apiUrl: string }> = ({ number, apiUrl }) => {
  return (
    <MainLayout>
      <h1>Index page: {apiUrl}</h1>
      <BaselineGrid />
    </MainLayout>
  );
};

Index.getInitialProps = async (ctx) => {
  const number = Math.random();
  const apiUrl = getPublicRuntimeConfig().apiUrl || "/";
  return { number, apiUrl };
};

export default Index;
