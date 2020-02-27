import React from "react";

import AppBar from "@material-ui/core/AppBar";
import Toolbar from "@material-ui/core/Toolbar";
import { fade, makeStyles } from "@material-ui/core/styles";
import Typography from "@material-ui/core/Typography";
import Menu from "@material-ui/core/Menu";
import { IconButton, Link, MenuItem } from "@material-ui/core";
import { NextComposed } from "../../components/link/links";
import {
  createUserAsRoute,
  INDEX_ROUTE,
  LOGIN_ROUTE,
  LOGOUT_ROUTE,
  USERS_ROUTE
} from "../../routes.constants";
import AccountCircle from "@material-ui/icons/AccountCircle";
import Button from "@material-ui/core/Button";
import { useUser } from "../../pages/_app";
import SearchString from "../searchString/searchString";
import InputBase from "@material-ui/core/InputBase";
import clsx from "clsx";
import SearchIcon from "@material-ui/icons/Search";

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
  search: {
    position: "relative",
    borderRadius: theme.shape.borderRadius,
    backgroundColor: fade(theme.palette.common.white, 0.15),
    "&:hover": {
      backgroundColor: fade(theme.palette.common.white, 0.25)
    },
    marginLeft: 0,
    width: "100%",
    [theme.breakpoints.up("sm")]: {
      marginLeft: theme.spacing(1),
      width: "auto"
    }
  },
  searchIcon: {
    width: theme.spacing(7),
    height: "100%",
    position: "absolute",
    pointerEvents: "none",
    display: "flex",
    alignItems: "center",
    justifyContent: "center"
  },
  inputRoot: {
    color: "inherit",
    width: "100%"
  },
  inputInput: {
    padding: theme.spacing(1, 1, 1, 7),
    transition: theme.transitions.create("width"),
    [theme.breakpoints.up("sm")]: {
      width: 120,
      "&:focus": {
        width: 200
      }
    }
  }
}));

function Navigation() {
  const classes = useStyles();
  const [anchorEl, setAnchorEl] = React.useState<Element | null>(null);
  const [value] = React.useState<string>("");

  const handleClick = (event: React.MouseEvent<HTMLButtonElement, MouseEvent>) => {
    setAnchorEl(event.currentTarget);
  };

  const handleClose = () => {
    setAnchorEl(null);
  };

  const user = useUser();

  return (
    <AppBar position="static">
      <Toolbar>
        <Typography variant="h6" className={classes.title}>
          <Link component={NextComposed} href={INDEX_ROUTE} color={"inherit"}>
            Home
          </Link>
        </Typography>
        <SearchString className={"navigation: "} value={value} clean>
          {(props) => {
            return <div className={clsx(classes.search)}>
              <div className={classes.searchIcon}>
                <SearchIcon/>
              </div>
              <InputBase
                {...props}
                placeholder="Search…"
                classes={{
                  root: classes.inputRoot,
                  input: classes.inputInput
                }}
                inputProps={{ "aria-label": "search" }}
              />
            </div>;
          }}
        </SearchString>
        {user ?
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
              <Link component={NextComposed} href={USERS_ROUTE} as={createUserAsRoute(user.id!)}>
                <MenuItem onClick={handleClose}>My Page</MenuItem>
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
