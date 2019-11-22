import React, {ChangeEvent, useCallback, useState} from 'react';
import Form from "components/form/form";
import Input from "components/form/input";
import Surface from "components/surface/surface";

const LoginForm = () => {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");

  const handleChangeEmail = useCallback((e: ChangeEvent<HTMLInputElement>) => {
    setEmail(e.target.value);
  }, []);

  const handleChangePassword = useCallback((e: ChangeEvent<HTMLInputElement>) => {
    setPassword(e.target.value);
  }, []);

  return <Surface>
    <h4>Login</h4>
    <Form name={"login-form"}>
      <Input placeholder={"Email"} type={"email"} value={email} onChange={handleChangeEmail}/>
      <Input placeholder={"Password"} type={"password"} value={password} onChange={handleChangePassword}/>
    </Form>
  </Surface>
};

export default LoginForm;
