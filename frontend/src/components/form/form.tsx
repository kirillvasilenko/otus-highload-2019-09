import React from "react";


function Form({children, ...props}: React.PropsWithChildren<FormProps>) {
  return <form {...props}>
    {children}
    <style jsx>{`
      form {
        border-radius: 8px;
        box-shadow: 0 1px 7px 0 rgba(0,0,0,0.3);
        display: inline-flex;
        flex-direction: column;
        padding: 20px;
        margin: 0 auto;
        align-items: center;
      }
    `}</style>
  </form>
}

export default Form;

type FormProps = React.DetailedHTMLProps<React.FormHTMLAttributes<HTMLFormElement>, HTMLFormElement>;