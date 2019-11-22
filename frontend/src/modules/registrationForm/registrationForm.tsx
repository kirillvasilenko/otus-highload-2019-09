import React, {ChangeEvent, useCallback, useState} from 'react';
import Form from "components/form/form";
import Input from "components/form/input";
import Surface from "components/surface/surface";

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

  return <Surface>
    <h4>Registration</h4>
    <Form name={"registration-form"}>
      <Input placeholder={"Email"} type={"email"} autoComplete={"off"} name={"email"} value={email}
             onChange={handleChangeEmail}/>
      <Input placeholder={"Password"} type={"password"} autoComplete={"off"} name="" value={password}
             onChange={handleChangePassword}/>
      <Input placeholder={"Confirm password"} type={"password"} autoComplete={"off"} value={newPassword}
             onChange={handleNewChangePassword}/>
    </Form>
  </Surface>
};

export default RegistrationForm;
