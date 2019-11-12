import React from "react";
import {Link} from "react-router-dom";
import {AUTH, HOME} from "../pages/pagesRoutes";

const MainLayout: React.FC = ({children}) => {
  return <div className={"App"}>
    <header>
      {/*<nav>*/}
      {/*  <ul>*/}
      {/*    <li>*/}
      {/*      <Link to={HOME}>Home</Link>*/}
      {/*    </li>*/}
      {/*    <li>*/}
      {/*      <Link to={AUTH}>Auth</Link>*/}
      {/*    </li>*/}
      {/*  </ul>*/}
      {/*</nav>*/}
    </header>
    <main>
      {children}
    </main>
    <footer/>
  </div>
};

export default MainLayout;
