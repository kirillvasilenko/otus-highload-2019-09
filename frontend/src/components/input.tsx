import React from "react";
import classNames from "classnames";
import styles from "./form.module.css";

const Input: React.FC<InputProps> = ({className, ...props}) => {
  const cn = classNames(styles.input, className);
  return <input className={cn} {...props}/>
};

export default Input;

export type InputProps = React.DetailedHTMLProps<React.InputHTMLAttributes<HTMLInputElement>, HTMLInputElement>;
