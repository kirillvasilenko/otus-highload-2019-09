import React from "react";
import MainLayout from "../components/mainLayout/mainLayout";
import LoginForm from "../modules/loginForm/loginForm";
import Container from "@material-ui/core/Container";

const Index = () => {
  return (
    <MainLayout>
      <Container maxWidth={"xs"}>
        <LoginForm/>
      </Container>
    </MainLayout>
  );
};

export default Index;
