import { useEffect, useState } from "react";
import { Unpromisify } from "../utils/types";

type UseRequestReturn<T> = {
  data: T,
  error?: string;
  isLoading: boolean;
}

export default function useRequest<T extends (...args: any[]) => any>(callback: T, defaultValue: Unpromisify<ReturnType<T>> ): UseRequestReturn<Unpromisify<ReturnType<T>>> {
  const [data, setData] = useState(defaultValue);
  const [error, setError] = useState<string>();
  const [isLoading, setIsLoading] = useState<boolean>(false);
  useEffect(() => {
    const fetch = async () => {
      try {
        setIsLoading(true);
        const data = await callback();
        setIsLoading(false);
        setData(data);
      } catch (e) {
        setIsLoading(false);
        setError(e.message);
      }
    };
    fetch();
  }, [callback]);

  return { data, error, isLoading };
}