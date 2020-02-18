import * as React from "react";
import Navigation from "../../modules/navigation/navigation";

function MainLayout(props: React.PropsWithChildren<any>) {
  return (
    <>
      <Navigation />
      <main>{props.children}</main>
      <footer></footer>
    </>
  );
}

export default MainLayout;
