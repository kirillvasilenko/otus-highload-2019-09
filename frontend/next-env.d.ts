/// <reference types="next" />
/// <reference types="next/types/global" />

declare global {
  interface NodeJ {
    Process: {
      apiUrl?: string;
    }
  }
}
