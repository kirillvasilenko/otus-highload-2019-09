import React from "react";
import { useForm } from "react-hook-form";
import apiClient from "../../api/client";
import { saveToken } from "../../utils/token";
import Form from "../../components/form/form";
import Input from "../../components/form/input";

type LoginForm = {
  email: string;
  password: string;
}

const login = async ({email, password}: LoginForm) => {
  try {
    const result = await apiClient.login(email, password);
    saveToken(result);
  } catch (e) {}
};

function LoginForm () {
  const { register, handleSubmit } = useForm<LoginForm>();

  const onSubmit = handleSubmit((data) => {
    login(data);
  });

  return <Form onSubmit={onSubmit}>
    <Input placeholder="email" name="email" type="string" autoComplete={"username"} ref={register({ required: true })}/>
    <Input placeholder="password" name="password" type={"password"} autoComplete={"password"} ref={register({ required: true })}/>
    <button type="submit">Login</button>
  </Form>
}

export default LoginForm;