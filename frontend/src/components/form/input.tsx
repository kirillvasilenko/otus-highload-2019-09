import React from "react";


function Input(props: InputProps, ref: React.Ref<any>) {
  return <div className={"input"}>
    <input {...props} ref={ref}/>
    <style jsx>{`
      .input {
        display: inline-block;
        overflow: visible;
      }
      input {
        margin: 0;
        border: none transparent;
        border-bottom: 1px solid black;
        padding: 10px;
        width: 200px;
      }
      .input:not(:last-child) {
        margin-bottom: 20px;
      }
    `}</style>
  </div>
}

export default React.forwardRef(Input);

type InputProps = React.DetailedHTMLProps<React.InputHTMLAttributes<HTMLInputElement>, HTMLInputElement>;