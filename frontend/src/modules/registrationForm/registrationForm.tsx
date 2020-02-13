import React from "react";
import { useForm } from "react-hook-form";
import apiClient from "../../api/client";
import { saveToken } from "../../utils/token";
import Form from "../../components/form/form";
import Input from "../../components/form/input";

type RegistrationForm = {
  givenName: string;
  email: string;
  password: string;
  repeatedPassword: string;
  city: string;
  familyName: string;
};

const registration = async (data: RegistrationForm) => {
  try {
    const result = await apiClient.registerUser(data);
    if (result.token) {
      saveToken(result.token);
    }
  } catch (e) {}
};

const RegistrationForm = () => {
  const { register, handleSubmit } = useForm<RegistrationForm>();

  const onSubmit = handleSubmit((data) => {
    registration(data);
  });

  return <Form onSubmit={onSubmit}>
      <Input placeholder="givenName" name="givenName" type="string" autoComplete={"username"} ref={register({ required: "this is required" })}/>
      <Input placeholder="familyName" name="familyName" type="string" ref={register({ required: true })}/>
      <Input placeholder="city" name="city" type="string" ref={register({ required: true })}/>
      <Input placeholder="email" name="email" type="email" autoComplete={"email"} ref={register({ required: true })}/>
      <Input placeholder="password" name="password" type={"password"} autoComplete={"new-password"} ref={register({ required: true })}/>
      <Input placeholder="repeatedPassword" name="repeatedPassword" type={"password"} autoComplete={"new-password"} ref={register({ required: true })}/>
      <button type="submit">Sign up</button>
    </Form>
};

export default RegistrationForm;