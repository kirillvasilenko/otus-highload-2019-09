import React, { FormEvent, useCallback, useState } from "react";
import { makeStyles } from "@material-ui/core/styles";
import Paper from "@material-ui/core/Paper";
import InputBase from "@material-ui/core/InputBase";
import IconButton from "@material-ui/core/IconButton";
import SearchIcon from "@material-ui/icons/Search";
import MainLayout from "../components/mainLayout/mainLayout";
import { NextPage } from "next";
import clientFactory from "../api/clientFactory";
import { UserDto } from "@kirillamurskiy/socialnetwork-client";
import Link from "next/link";
import { createUserAsRoute, USERS_ROUTE } from "../routes.constants";

const useStyles = makeStyles(theme => ({
  root: {
    padding: "2px 4px",
    display: "flex",
    alignItems: "center",
    width: 400
  },
  input: {
    marginLeft: theme.spacing(1),
    flex: 1
  },
  iconButton: {
    padding: 10
  },
  divider: {
    height: 28,
    margin: 4
  }
}));

const Index: NextPage = () => {
  const classes = useStyles();
  const [value, setValue] = useState<string>("");
  const [users, setUsers] = useState<UserDto[]>([]);
  const [pending, setPending] = useState<boolean>(false);

  const handleChange = useCallback((event: React.ChangeEvent<HTMLInputElement>) => {
    setValue(event.target.value);
  }, []);

  const handleSearch = useCallback((e) => {
    e.preventDefault();
    const fetch = async () => {
      try {
        setPending(true);
        const users = await clientFactory.users().getUsers(value);
        setUsers(users);
        setPending(false);
      } catch (e) {
        setPending(false);
        setUsers([]);
      }

    };

    fetch();
  }, [value]);

  return (
    <MainLayout>
      <Paper component="form" className={classes.root}>
        <InputBase
          onChange={handleChange}
          className={classes.input}
          placeholder="Search users"
          inputProps={{ "aria-label": "search users" }}
        />
        <IconButton type="submit" className={classes.iconButton} aria-label="search" onClick={handleSearch}
                    disabled={pending}>
          <SearchIcon/>
        </IconButton>
      </Paper>
      {users.map(user => {
        return <Link key={user.id} href={USERS_ROUTE}
                     as={createUserAsRoute(user.id!.toString())}><a>{user.givenName} {user.familyName}</a></Link>;
      })}
    </MainLayout>
  );
};

export default Index;
