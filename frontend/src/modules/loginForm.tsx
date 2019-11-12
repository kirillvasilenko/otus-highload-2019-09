import React, {ChangeEvent, useCallback, useState} from 'react';
import Form from "../components/form";
import Input from "../components/input";
import styles from "./auth.module.css";
import classNames from "classnames";

const LoginForm = () => {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");

  const handleChangeEmail = useCallback((e: ChangeEvent<HTMLInputElement>) => {
    setEmail(e.target.value);
  }, []);

  const handleChangePassword = useCallback((e: ChangeEvent<HTMLInputElement>) => {
    setPassword(e.target.value);
  }, []);

  return <div className={classNames(styles.auth, styles.auth_login)}>
    <h3>Login</h3>
    <Form name={"login-form"}>
      <Input placeholder={"Email"} type={"email"} value={email} onChange={handleChangeEmail}/>
      <Input placeholder={"Password"} type={"password"} value={password} onChange={handleChangePassword}/>
    </Form>
  </div>
};

export default LoginForm;
