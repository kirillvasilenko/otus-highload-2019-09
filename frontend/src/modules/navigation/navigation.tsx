import React from "react";
import {
  LOGIN_PAGE,
  INDEX_ROUTE,
  REGISTRATION_ROUTE
} from "../../routes.constants";

import AppBar from "@material-ui/core/AppBar";
import Toolbar from "@material-ui/core/Toolbar";
import IconButton from "@material-ui/core/IconButton";
import { makeStyles } from '@material-ui/core/styles';
import Typography from '@material-ui/core/Typography';

const useStyles = makeStyles(theme => ({
  title: {
    flexGrow: 1,
  },
}));

function Navigation(props: React.PropsWithChildren<any>) {
  const classes = useStyles();
  return (
    <AppBar position="static">
      <Toolbar>
        <Typography variant="h6" className={classes.title}>
          В Партии
        </Typography>
      </Toolbar>
    </AppBar>
  );
}

export default Navigation;
