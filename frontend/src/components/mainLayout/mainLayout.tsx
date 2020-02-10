import * as React from "react";
import Navigation from "../../modules/navigation/navigation";

function MainLayout(props: React.PropsWithChildren<any>) {
  return (
    <>
      <header className={""}>
        <Navigation />
      </header>
      <main className={""}>{props.children}</main>
      <footer className={""}></footer>
    </>
  );
}

export default MainLayout;
