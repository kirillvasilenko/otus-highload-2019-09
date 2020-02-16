import React from "react";
import MainLayout from "../components/mainLayout/mainLayout";
import LoginForm from "../modules/loginForm/loginForm";
import Container from "@material-ui/core/Container";

function Login() {
  return (
    <MainLayout>
      <Container maxWidth={"xs"}>
        <LoginForm/>
      </Container>
    </MainLayout>
  );
}

export default Login;
