import React from "react";
import styles from "./mainLayout.module.css"
import Navigation from "modules/navigation/navigation";


const MainLayout: React.FC = ({children}) => {
  return <div className={styles.app}>
    <header className={styles.header}>
      <Navigation/>
    </header>
    <main className={styles.main}>
      {children}
    </main>
    <footer className={styles.footer}/>
  </div>
};

export default MainLayout;
