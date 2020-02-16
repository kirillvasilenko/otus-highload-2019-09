import React from "react";
import MainLayout from "../components/mainLayout/mainLayout";
import RegistrationForm from "../modules/registrationForm/registrationForm";
import { Container } from "@material-ui/core";

function Registration() {
  return (
    <MainLayout>
      <Container maxWidth={"xs"}>
        <RegistrationForm/>
      </Container>
    </MainLayout>
  );
}

export default Registration;
