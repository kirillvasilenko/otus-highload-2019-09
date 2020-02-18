import React from "react";
import NextLink from "next/link";
import { LinkProps as NextLinkProps } from "next/dist/client/link";
type NextComposedLink = NextLinkProps & {
  className?: string;
}

export const NextComposed = React.forwardRef<HTMLAnchorElement, NextComposedLink>((props, ref) => {
  const { as, href, prefetch, children, className = "" } = props;

  return (
    <NextLink href={href} prefetch={prefetch} as={as}>
      <a className={className} ref={ref}>{children}</a>
    </NextLink>
  );
});
