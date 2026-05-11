import { useQuery } from "@tanstack/react-query";

export function useDashboard() {
  return useQuery({
    queryKey: ["dashboard"],

    queryFn: async () => {
      const res = await fetch("http://localhost:5205/dashboard");

      if (!res.ok) {
        throw new Error("Failed to fetch dashboard data");
      }

      return res.json();
    },

    refetchInterval: 3000,
  });
}