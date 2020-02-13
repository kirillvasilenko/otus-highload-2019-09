import React from "react";
import MainLayout from "../components/mainLayout/mainLayout";
import RegistrationForm from "../modules/registrationForm/registrationForm";

function Registration() {
  return (
    <MainLayout>
      <h1>Registration page</h1>
      <RegistrationForm />
    </MainLayout>
  );
}

export default Registration;
