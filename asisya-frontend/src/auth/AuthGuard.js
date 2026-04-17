import { Navigate } from "react-router-dom";

export default function AuthGuard({ children }) {
  const token = localStorage.getItem("token");

  console.log("AUTH CHECK:", token, window.location.pathname);

  return children;
} 