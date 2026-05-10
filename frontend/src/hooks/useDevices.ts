"use client";

import { useQuery } from "@tanstack/react-query";

export function useDevices() {
  return useQuery({
    queryKey: ["devices"],
    queryFn: async () => {
      const res = await fetch(`${process.env.NEXT_PUBLIC_API_URL}/api/devices`
        
      );

      if (!res.ok) {
        throw new Error("Failed to fetch devices");
      }

      return res.json();
    },
    retry: 1, // กัน retry วน
  });
}