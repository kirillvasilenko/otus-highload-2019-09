import React from "react";
import NextLink from "next/link";
import MuiLink from "@material-ui/core/Link";
import { LinkProps as NextLinkProps } from "next/dist/client/link";
import { LinkBaseProps } from "@material-ui/core/Link/Link";

type NextComposedLink = NextLinkProps & {
  className?: string;
}

export const NextComposed = React.forwardRef<HTMLAnchorElement, NextComposedLink>((props, ref) => {
  const { as, href, prefetch, children, className = "" } = props;

  return (
    <NextLink href={href} prefetch={prefetch} as={as}>
      <a className={className}>{children}</a>
    </NextLink>
  );
});

// const Link: React.FC<LinkBaseProps> = ({children, ...other}: React.PropsWithChildren<LinkBaseProps>) => {
//   return <MuiLink {...other} component={NextComposed}>{children}</MuiLink>;
// };

// export default Link;