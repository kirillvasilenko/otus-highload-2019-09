import React, { useEffect, useState } from "react";
import MainLayout from "../components/mainLayout/mainLayout";
import BaselineGrid from "../components/baselineGrid/baselineGrid";
import {NextPage} from "next";
import { getPublicRuntimeConfig } from "..//utils/runtimeConfig";

import apiClient from "../api/client";
import { UserDto } from "@KirillAmurskiy/socialnetwork-client";

const Index: NextPage = ({  }) => {
  const [users, setUsers] = useState<UserDto[]>([]);

  useEffect(() => {
    const fetch = async () => {
      const users = await apiClient.getUsers(null,null,null,null,null,null,0,10);
      setUsers(users);
    };
    fetch();
  }, []);

  return (
    <MainLayout>
      <h1>Count of users: {users.length}</h1>
      <BaselineGrid />
    </MainLayout>
  );
};

export default Index;
