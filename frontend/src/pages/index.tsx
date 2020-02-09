import React from "react";
import MainLayout from "../components/mainLayout/mainLayout";
import BaselineGrid from "../components/baselineGrid/baselineGrid";
import {NextPage} from "next";

const Index: NextPage<{ number: number }> = ({ number }) => {
  return (
    <MainLayout>
      <h1>Index page: {number}</h1>
      <BaselineGrid />
    </MainLayout>
  );
};

Index.getInitialProps = async () => {
  const number = Math.random();
  return { number };
};

export default Index;
