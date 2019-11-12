import React from "react";
import {Route, Redirect, RouteProps} from "react-router-dom";
import {AUTH} from "./pagesRoutes";

const PrivateRoute: React.FC<RouteProps> = ({children, ...props}) => {
  const isAuthenticated = false;
  return <Route {...props}>
    {isAuthenticated ? children : <Redirect to={AUTH}/>}
  </Route>
};

export default PrivateRoute;
