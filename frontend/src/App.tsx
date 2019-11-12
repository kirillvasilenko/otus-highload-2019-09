import React from 'react';
import {
  BrowserRouter as Router,
  Switch,
  Route
} from "react-router-dom";
import './App.css';
import AuthPage from "./pages/authPage";
import MainLayout from "./modules/mainLayout";
import {AUTH, HOME} from "./pages/pagesRoutes";
import HomePage from "./pages/homePage";
import PrivateRoute from "./pages/privateRoute";

const App: React.FC = () => {
  return (
    <Router>
      <MainLayout>
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
