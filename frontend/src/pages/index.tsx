import React from "react";
import MainLayout from "../components/mainLayout/mainLayout";
import LoginForm from "../modules/loginForm/loginForm";

const Index = () => {
  return (
    <MainLayout>
      <h1>Main page</h1>

      <LoginForm />
    </MainLayout>
  );
};

export default Index;
