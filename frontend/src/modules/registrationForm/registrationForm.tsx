import React from "react";
import { useForm } from "react-hook-form";
import { Grid, TextField, Button } from "@material-ui/core";
import Router from "next/router";
import { INDEX_ROUTE } from "../../routes.constants";
import { TokenStorageBrowser } from "../../utils/tokenStorage";
import clientFactory from "../../api/clientFactory";

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
    const result = await clientFactory.registration().registerUser({...data, age: parseInt(data.age)});
    if (result.token) {
      new TokenStorageBrowser().set(result.token);
      await Router.push(INDEX_ROUTE);
    }
  } catch (e) {
  }
};

const RegistrationForm = () => {
  const { register, handleSubmit, errors } = useForm<RegistrationForm>();

  const onSubmit = handleSubmit((data) => {
    registration(data);
  });

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