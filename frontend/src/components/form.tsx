import React from 'react';
import classNames from "classnames";
import styles from "./form.module.css";

const Form: React.FC<FormProps> = ({className = "", ...props}) => {
  const cn = classNames(styles.form, className);
  return <form className={cn} {...props}/>
};

export default Form;

export type FormProps = React.DetailedHTMLProps<React.FormHTMLAttributes<HTMLFormElement>, HTMLFormElement>
