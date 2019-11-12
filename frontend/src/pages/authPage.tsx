import React from "react";
import LoginForm from "../modules/loginForm";
import RegistrationForm from "../modules/registrationForm";
import styles from "./authPage.module.css";

const AuthPage: React.FC = () => {
  return <div className={styles.authPage}>
    <LoginForm/>
    <RegistrationForm/>
  </div>
};

export default AuthPage;
