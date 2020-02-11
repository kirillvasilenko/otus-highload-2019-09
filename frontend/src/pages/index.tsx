import React, { useCallback } from "react";
import MainLayout from "../components/mainLayout/mainLayout";
import BaselineGrid from "../components/baselineGrid/baselineGrid";
import {NextPage} from "next";

import apiClient from "../api/client";
import useRequest from "../hooks/useRequest";

const Index: NextPage = ({  }) => {
  const request = useCallback(( ) => {
    return apiClient.getUsers(undefined,undefined,undefined,undefined,undefined,undefined,0,10)
  }, []);

  const {data: users, isLoading, error} = useRequest(request, []);

  return (
    <MainLayout>
      <p>{isLoading}</p>
      <h1>Count of users: {users.length}</h1>
      {error ? <p>error</p> : null}
      <BaselineGrid />
    </MainLayout>
  );
};

export default Index;
