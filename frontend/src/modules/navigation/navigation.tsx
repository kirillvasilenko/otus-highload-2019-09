import React from "react";
import { Link } from "react-router-dom";
import {
  AUTH_ROUTE,
  INDEX_ROUTE,
  REGISTRATION_ROUTE
} from "pages/routes.constants";

function Navigation(props: React.PropsWithChildren<any>) {
  return (
    <nav>
      <ul>
        <li>
          <Link to={INDEX_ROUTE}>Home</Link>
        </li>
        <li>
          <Link to={AUTH_ROUTE}>Auth</Link>
        </li>
        <li>
          <Link to={REGISTRATION_ROUTE}>Registration</Link>
        </li>
      </ul>
    </nav>
  );
}

export default Navigation;
