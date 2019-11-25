import React from "react";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";

import {
  AUTH_ROUTE,
  INDEX_ROUTE,
  REGISTRATION_ROUTE
} from "pages/routes.constants";
import IndexPage from "pages/indexPage/indexPage";

import BaselineGrid from "components/baselineGrid/baselineGrid";
import MainLayout from "components/mainLayout/mainLayout";
import AuthPage from "pages/authPage/authPage";
import RegistrationPage from "pages/registrationPage/registrationPage";

const App: React.FC = () => {
  return (
    <Router>
      <MainLayout>
        <Switch>
          <Route path={REGISTRATION_ROUTE}>
            <RegistrationPage />
          </Route>
          <Route path={AUTH_ROUTE}>
            <AuthPage />
          </Route>
          <Route path={INDEX_ROUTE}>
            <IndexPage />
          </Route>
        </Switch>

        <BaselineGrid />
      </MainLayout>
    </Router>
  );
};

export default App;
