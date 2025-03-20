import axios from "axios";

export const login = async (credentials) => {
  const response = await axios.post("/api/auth/login", credentials);
  return response.data;
};

export const logout = async () => {
  const response = await axios.post("/api/auth/logout");
  return response.data;
};
