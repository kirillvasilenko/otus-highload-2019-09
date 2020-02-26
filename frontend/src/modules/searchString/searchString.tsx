import React, { useCallback } from "react";

import Router from "next/router";
import { SEARCH_ROUTE } from "../../routes.constants";


const SearchString: React.FC<SearchStringProps> = ({clean, onSubmit, className, children , value = ""}) => {
  const [searchString, setValue] = React.useState<string>(value);

  React.useEffect(() => {
    setValue(value);
  }, [value]);

  const onChange = useCallback((event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
    setValue(event.target.value);
  }, []);

  const handleSubmit = useCallback((event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    const form = new FormData(event.currentTarget);
    const string = new URLSearchParams(form as any);
    clean && setValue("");
    Router.push(SEARCH_ROUTE + "?" + string.toString(), undefined, {
      shallow: true,
    })
  }, []);

  return <form action={"/search"} onSubmit={handleSubmit}>
      {children({ onChange, value: searchString, name: "name" })}
  </form>;

};

type SearchStringProps = {
  className?: string;
  children: (props: InjectedProps) => React.ReactElement;
  value?: string;
  onSubmit?: (event: React.FormEvent<HTMLFormElement>) => void;
  clean?: boolean;
}

type InjectedProps = {
  onChange: (event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => void;
  value: string,
  name: "name"
}

export default SearchString;