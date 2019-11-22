import React from 'react';
import {
  BrowserRouter as Router,
  Switch,
  Route
} from "react-router-dom";
import './App.css';
import AuthPage from "pages/authPage/authPage";
import MainLayout from "modules/mainLayout/mainLayout";
import {AUTH, HOME} from "pages/routes.constants";
import HomePage from "pages/homePage/homePage";
import PrivateRoute from "pages/privateRoute/privateRoute";
// import BaselineGrid from "components/baselineGrid/baselineGrid";

type X = {
  hello?: {
    world: string
  }
}

const App: React.FC = () => {
  const x: X = {};
  return (
    <Router>
      {/*<BaselineGrid/>*/}
      <MainLayout>
        <h1>{x.hello?.world}</h1>
        <Switch>
          <Route path={AUTH}>
            <AuthPage/>
          </Route>
          <PrivateRoute path={HOME}>
            <HomePage/>
          </PrivateRoute>
        </Switch>
      </MainLayout>
    </Router>
  );
};

export default App;
