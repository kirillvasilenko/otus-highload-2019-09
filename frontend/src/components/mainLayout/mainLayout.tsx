import * as React from "react";
import Navigation from "../../modules/navigation/navigation";
import Container from "@material-ui/core/Container";

function MainLayout(props: React.PropsWithChildren<any>) {
  return (
    <>
      <Navigation />
      <main>{props.children}</main>
      <footer className={""}></footer>
    </>
  );
}

export default MainLayout;
