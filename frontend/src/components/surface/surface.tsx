import React, {ComponentProps} from "react";
import classNames from "classnames";
import styles from "./surface.module.css";

function Surface(props: ComponentProps<"div">) {
  return <div className={classNames(styles.surface)}>
    {props.children}
  </div>
}

export default Surface;
