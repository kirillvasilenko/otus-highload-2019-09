import React, { useEffect } from "react";

import AppBar from "@material-ui/core/AppBar";
import Toolbar from "@material-ui/core/Toolbar";
import { makeStyles } from "@material-ui/core/styles";
import Typography from "@material-ui/core/Typography";
import Menu from "@material-ui/core/Menu";
import { IconButton, Link, MenuItem } from "@material-ui/core";
import { NextComposed } from "../../components/link/links";
import { LOGIN_ROUTE, LOGOUT_ROUTE, PROFILE_ROUTE } from "../../routes.constants";
import AccountCircle from "@material-ui/icons/AccountCircle";
import Token from "../../utils/token";
import Button from "@material-ui/core/Button";

const useStyles = makeStyles(theme => ({
  title: {
    flexGrow: 1
  },
  avatar: {
    color: theme.palette.primary.contrastText
  },
  link: {
    margin: theme.spacing(1, 1.5),
    color: theme.palette.primary.contrastText
  },
}));

function Navigation() {
  const classes = useStyles();
  const [anchorEl, setAnchorEl] = React.useState<Element | null>(null);
  const [auth, setAuth] = React.useState<boolean>(false);
  const handleClick = (event: React.MouseEvent<HTMLButtonElement, MouseEvent>) => {
    setAnchorEl(event.currentTarget);
  };

  useEffect(() => {
    setAuth(Token.makeTokenBrowser(window).isExist());
  }, []);

  const handleClose = () => {
    setAnchorEl(null);
  };

  return (
    <AppBar position="static">
      <Toolbar>
        <Typography variant="h6" className={classes.title}>
          Home
        </Typography>
        {auth ?
          <>
            <IconButton aria-controls="simple-menu" aria-haspopup="true" onClick={handleClick}>
              <AccountCircle className={classes.avatar} fontSize={"large"} color={"primary"}/>
            </IconButton>
            <Menu
              id="simple-menu"
              anchorEl={anchorEl}
              keepMounted
              open={Boolean(anchorEl)}
              onClose={handleClose}
            >
              <Link component={NextComposed} href={PROFILE_ROUTE}>
                <MenuItem onClick={handleClose}>Profile</MenuItem>
              </Link>
              <Link component={NextComposed} href={LOGOUT_ROUTE}>
                <MenuItem onClick={handleClose}>Logout</MenuItem>
              </Link>
            </Menu>
          </> : <Button href={LOGIN_ROUTE} color="primary" variant="outlined" className={classes.link}>
            Login
          </Button>}
      </Toolbar>
    </AppBar>
  );
}

export default Navigation;
