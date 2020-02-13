import React from "react";
import MainLayout from "../components/mainLayout/mainLayout";
import LoginForm from "../modules/loginForm/loginForm";

function Login() {
  return (
    <MainLayout>
      <h1>Login page</h1>

      <LoginForm />
    </MainLayout>
  );
}

export default Login;
