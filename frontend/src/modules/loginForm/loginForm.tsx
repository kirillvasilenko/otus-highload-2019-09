import React from "react";
import { useForm } from "react-hook-form";
import TextField from "@material-ui/core/TextField";
import { makeStyles } from "@material-ui/core/styles";
import Button from "@material-ui/core/Button";
import Grid from "@material-ui/core/Grid";
import { Link } from "@material-ui/core";
import { NextComposed } from "../../components/link/links";
import { INDEX_ROUTE, REGISTRATION_ROUTE } from "../../routes.constants";
import Router from "next/router";
import { TokenStorageBrowser } from "../../utils/tokenStorage";
import clientFactory from "../../api/clientFactory";

type LoginForm = {
  email: string;
  password: string;
}

const login = async ({ email, password }: LoginForm) => {
  try {
    const result = await clientFactory.makeAuthClient().login(email, password);
    new TokenStorageBrowser().set(result);
    await Router.push(INDEX_ROUTE);
  } catch (e) {
  }
};

const useStyles = makeStyles(theme => ({
  submit: {
    margin: theme.spacing(3, 0, 2)
  }
}));

function LoginForm() {
  const classes = useStyles();
  const { register, handleSubmit, errors } = useForm<LoginForm>();

  const onSubmit = handleSubmit((data) => {
    login(data).then();
  });

  return <form onSubmit={onSubmit}>
      <TextField
        fullWidth
        type={"email"}
        autoComplete={"email"}
        inputRef={register({ required: true })}
        label="Email"
        name="email"
        error={Boolean(errors.email)}
      />
      <TextField
        fullWidth
        autoComplete={"password"}
        type="password"
        inputRef={register({ required: true })}
        label="Password"
        name="password"
        error={Boolean(errors.password)}
      />
      <Button className={classes.submit} fullWidth type={"submit"} color={"primary"} variant={"contained"}>Sign In</Button>
      <Grid container>
        <Grid item xs>
          <Link href="#" variant="body2" component={NextComposed}>
            Forgot password?
          </Link>
        </Grid>
        <Grid item>
          <Link href={REGISTRATION_ROUTE} variant="body2" component={NextComposed}>
            {"Don't have an account? Sign Up"}
          </Link>
        </Grid>
      </Grid>
    </form>
}

export default LoginForm;