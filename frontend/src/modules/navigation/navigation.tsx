import React from "react";
import {
  AUTH_ROUTE,
  INDEX_ROUTE,
  REGISTRATION_ROUTE
} from "../../routes.constants";
import Link from "next/link";

function Navigation(props: React.PropsWithChildren<any>) {
  return (
    <nav>
      <ul>
        <li>
          <Link href={INDEX_ROUTE}><a>Home</a></Link>
        </li>
        <li>
          <Link href={AUTH_ROUTE}><a>Auth</a></Link>
        </li>
        <li>
          <Link href={REGISTRATION_ROUTE}><a>Registration</a></Link>
        </li>
      </ul>
    </nav>
  );
}

export default Navigation;
