import {
  Avatar,
  Card, createStyles,
  Link,
  List,
  ListItem,
  ListItemAvatar,
  ListItemText, Theme,
  Typography
} from "@material-ui/core";
import React from "react";
import { NextComposed } from "../../components/link/links";
import { createUserAsRoute, USERS_ROUTE } from "../../routes.constants";
import { makeStyles } from "@material-ui/styles";
import { UserDto } from "@kirillamurskiy/socialnetwork-client";
import Router from "next/router";
import clientFactory from "../../api/clientFactory";

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    root: {
      width: "100%",
      maxWidth: 360,
      backgroundColor: theme.palette.background.paper
    },
    inline: {
      display: "inline"
    },
    card: {
      marginBottom: theme.spacing(1)
    },
    container: {
      marginTop: theme.spacing(2)
    }
  })
);

const UserList: React.FC<UserList> = ({ users }) => {
  const [userList, setUserList] = React.useState<UserDto[]>(users);
  const classes = useStyles();

  const handleRouterChange = React.useCallback((route: string) => {
    if (Router.query.name) {
      const fetch = async () => {
        const users = await clientFactory.users().getUsers(Router.query.name as string, undefined, undefined, undefined, 0, 3);
        setUserList(users);
      };
      fetch();
    }
  }, []);

  React.useEffect(() => {
    Router.events.on("routeChangeComplete", handleRouterChange);
    return () => Router.events.off("routeChangeComplete", handleRouterChange);
  }, []);

  return <List className={classes.root}>
    {userList.map((user) => {
      return <ListItem key={user.id} alignItems="flex-start" component={Card} className={classes.card}
                       variant="outlined">
        <ListItemAvatar>
          <Avatar alt="Remy Sharp" src="/static/images/avatar/1.jpg"/>
        </ListItemAvatar>
        <ListItemText
          primary={
            <Link component={NextComposed} href={USERS_ROUTE} as={createUserAsRoute(user.id!)}>
              {`${user.givenName} ${user.familyName}`}
            </Link>
          }
          secondary={
            <React.Fragment>
              <Typography
                component="span"
                variant="body2"
                className={classes.inline}
                color="textPrimary"
              >
                {user.city}
              </Typography>
              {user.interests}
            </React.Fragment>
          }
        />
      </ListItem>;
    })}
  </List>;
};

type UserList = {
  users: UserDto[]
};


export default React.memo(UserList);