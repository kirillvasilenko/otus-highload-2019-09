import React from "react";
import {
  AUTH_ROUTE,
  INDEX_ROUTE,
  REGISTRATION_ROUTE
} from "../../pages/routes.constants";
import Link from "next/link";

function Navigation(props: React.PropsWithChildren<any>) {
  return (
    <nav>
      <ul>
        <li>
          <Link href={INDEX_ROUTE}>Home</Link>
        </li>
        <li>
          <Link href={AUTH_ROUTE}>Auth</Link>
        </li>
        <li>
          <Link href={REGISTRATION_ROUTE}>Registration</Link>
        </li>
      </ul>
    </nav>
  );
}

export default Navigation;
