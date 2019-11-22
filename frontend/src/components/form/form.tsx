import React, {useCallback} from 'react';
import classNames from "classnames";
import styles from "./form.module.css";

const Form: React.FC<FormProps> = ({className = "", onSubmit, ...props}) => {
  const cn = classNames(styles.form, className);
  const handleSubmit = useCallback((event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    if (typeof onSubmit === "function") {
      onSubmit(event);
    }
  }, [onSubmit]);
  return <form className={cn} {...props} onSubmit={handleSubmit}/>
};

export default Form;

export type FormProps = React.DetailedHTMLProps<React.FormHTMLAttributes<HTMLFormElement>, HTMLFormElement>
