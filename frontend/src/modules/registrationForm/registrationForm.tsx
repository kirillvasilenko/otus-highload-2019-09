import React, { useEffect } from "react";
import { useForm } from "react-hook-form";
import apiClient from "../../api/client";
import { getToken, saveToken } from "../../utils/token";
import { Grid, TextField, Button } from "@material-ui/core";

import decode from "jwt-decode";

type RegistrationForm = {
  givenName: string;
  email: string;
  password: string;
  repeatedPassword: string;
  city: string;
  familyName: string;
  age: string;
};

const registration = async (data: RegistrationForm) => {
  try {
    const result = await apiClient.registerUser({...data, age: parseInt(data.age)});
    if (result.token) {
      saveToken(result.token);
    }
  } catch (e) {
  }
};

const RegistrationForm = () => {
  const { register, handleSubmit, errors, control } = useForm<RegistrationForm>();

  const onSubmit = handleSubmit((data) => {
    registration(data);
  });

  useEffect(() => {
    const token = getToken();
    if (token?.accessToken) {
      console.info(decode(token.accessToken));
    }
  },[]);

  return <form onSubmit={onSubmit}>
    <Grid container spacing={2}>
      <Grid item xs={12} sm={6}>
        <TextField
          fullWidth
          autoComplete={"given-name"}
          inputRef={register({ required: true })}
          label="Given name"
          name="givenName"
          error={Boolean(errors.givenName)}
        />
      </Grid>
      <Grid item xs={12} sm={6}>
        <TextField
          fullWidth
          autoComplete={"family-name"}
          inputRef={register({ required: true })}
          label="Family name"
          name="familyName"
          error={Boolean(errors.familyName)}
        />
      </Grid>
      <Grid item xs={12}>
        <TextField
          fullWidth
          type={"email"}
          autoComplete={"email"}
          inputRef={register({ required: true })}
          label="Email"
          name="email"
          error={Boolean(errors.familyName)}
        />
      </Grid>
      <Grid item xs={12}>
        <TextField
          fullWidth
          type={"number"}
          autoComplete={"age"}
          inputRef={register({ required: true })}
          label="Age"
          name="age"
          error={Boolean(errors.familyName)}
        />
      </Grid>
      <Grid item xs={12}>
        <TextField
          fullWidth
          autoComplete={"address-level2"}
          inputRef={register({ required: true })}
          label="City"
          name="city"
          error={Boolean(errors.city)}
        />
      </Grid>
      <Grid item xs={12}>
        <TextField
          fullWidth
          autoComplete={"new-password"}
          type="password"
          inputRef={register({ required: true })}
          label="Password"
          name="password"
          error={Boolean(errors.password)}
        />
      </Grid>
      <Grid item xs={12}>
        <TextField
          fullWidth
          autoComplete={"new-password"}
          type="password"
          inputRef={register({ required: true })}
          label="Repeat password"
          name="repeatedPassword"
          error={Boolean(errors.repeatedPassword)}
        />
      </Grid>
      <Button fullWidth type={"submit"} color={"primary"} variant={"contained"}>Sign up</Button>
    </Grid>
  </form>;
};

export default RegistrationForm;