import axios from "axios";
export const appAxios = axios.create({
  baseURL: "http://localhost:9000/api/",
  withCredentials: false,
  headers: {
    "Content-Type": "application/json",
  },
});

export const setJwtTokenHeader = (jwtToken) => {
  appAxios.defaults.headers.common["Authorization"] = "Bearer " + jwtToken;
};

export const cleanTokenHeader = () => {
  appAxios.defaults.headers.common["Authorization"] = "";
};
