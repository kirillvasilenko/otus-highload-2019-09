import React, {ChangeEvent, useCallback, useState} from 'react';
import Form from "../components/form";
import Input from "../components/input";
import styles from "./auth.module.css";
import classNames from "classnames";

const RegistrationForm = () => {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [newPassword, setNewPassword] = useState("");

  const handleChangeEmail = useCallback((e: ChangeEvent<HTMLInputElement>) => {
    setEmail(e.target.value);
  }, []);

  const handleChangePassword = useCallback((e: ChangeEvent<HTMLInputElement>) => {
    setPassword(e.target.value);
  }, []);

  const handleNewChangePassword = useCallback((e: ChangeEvent<HTMLInputElement>) => {
    setNewPassword(e.target.value);
  }, []);

  return <div className={classNames(styles.auth, styles.auth_registration)}>
    <h3>Registration</h3>
    <Form name={"registration-form"}>
      <Input placeholder={"Email"} type={"email"} autoComplete={"off"} name={"email"} value={email}
             onChange={handleChangeEmail}/>
      <Input placeholder={"Password"} type={"password"} autoComplete={"off"} name="" value={password}
             onChange={handleChangePassword}/>
      <Input placeholder={"Confirm password"} type={"password"} autoComplete={"off"} value={newPassword}
             onChange={handleNewChangePassword}/>
    </Form>
  </div>
};

export default RegistrationForm;
