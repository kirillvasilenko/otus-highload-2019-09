import React from "react";
import { NextPage, NextPageContext } from "next";
import { UserDto } from "@kirillamurskiy/socialnetwork-client";
import clientFactory from "../../api/clientFactory";
import MainLayout from "../../components/mainLayout/mainLayout";
import {
  Container,
  createStyles, InputBase,
  Theme,
} from "@material-ui/core";
import { makeStyles } from "@material-ui/styles";
import SearchString from "../../modules/searchString/searchString";
import Router from "next/router";
import UserList from "../../modules/userList/userList";

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

const Search: React.FC<{ name: string }> = React.memo(({ name }) => {
  const ref = React.useRef<HTMLInputElement>();
  const [searchString, setSearchString] = React.useState<string>(name);

  React.useEffect(() => {
    if (ref.current !== undefined) {
      ref.current.focus();
      ref.current.selectionStart = ref.current.selectionEnd = name.length;
    }
  }, [searchString]);

  const handleRouterChange = React.useCallback((route: string) => {
    if (Router.query.name) {
      setSearchString(Router.query.name as string);
    }
  }, []);

  React.useEffect(() => {
    Router.events.on("routeChangeComplete", handleRouterChange);
    return () => Router.events.off("routeChangeComplete", handleRouterChange);
  }, []);

  return <SearchString value={searchString} className={"page: "}>
    {(props) => {
      return <InputBase {...props} inputRef={ref}/>;
    }}
  </SearchString>;
});

const SearchPage: NextPage<{ users: UserDto[], name: string }> = ({ users, name = "" }) => {
  const classes = useStyles();

  return (
    <MainLayout>
      <Container maxWidth="sm" className={classes.container}>
        <Search name={name} />
        <UserList users={users} />
      </Container>
    </MainLayout>
  );
};

interface SearchPageContext extends NextPageContext {
  query: { name: string }
}

SearchPage.getInitialProps = async (ctx: SearchPageContext) => {
  const { name } = ctx.query;
  const users = await clientFactory.users().getUsers(name, undefined, undefined, undefined, 0, 3);
  return { users, name };
};

export default SearchPage;
