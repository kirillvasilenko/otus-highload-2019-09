import React from "react";
import {Link} from "react-router-dom";
import {AUTH, HOME} from "pages/routes.constants";
import styles from "./navigation.module.css";

const Item: React.FC = ({children}) => {
  return <li className={styles.item}>
    {children}
  </li>
};

const Navigation: React.FC = () => {
  return <nav className={styles.navigation}>
    <ul className={styles.list}>
      <Item><Link className={styles.link} to={HOME}>Home</Link></Item>
      <Item><Link className={styles.link} to={AUTH}>Auth</Link></Item>
    </ul>
  </nav>
};

export default Navigation;
