import { useState } from "react";
import api from "../api/api";

export default function Login() {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");

  const login = async (e) => {
    e.preventDefault();

    try {
      const res = await api.post("/auth/login", {
        username,
        password
      });

      localStorage.setItem("token", res.data.token);

      alert("Login OK");
    } catch (err) {
      alert("Error login");
    }
  };

  return (
    <form onSubmit={login}>
      <input
        placeholder="user"
        onChange={(e) => setUsername(e.target.value)}
      />

      <input
        type="password"
        placeholder="pass"
        onChange={(e) => setPassword(e.target.value)}
      />

      <button>Login</button>
    </form>
  );
}