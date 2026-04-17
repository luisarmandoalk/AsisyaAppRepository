import { useState } from "react";
import { useNavigate } from "react-router-dom";
import api from "../api/api";

export default function Login() {
  const navigate = useNavigate();

  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");

  const login = async (e) => {
  e.preventDefault();

  try {
    const res = await api.post("/auth/login", {
      username,
      password
    });

    console.log(res.data); // ?? AQUÍ

    localStorage.setItem("token", res.data.token);

	setTimeout(() => {
	  navigate("/products", { replace: true });
	}, 50);

    alert("Login OK");
    navigate("/products");

  } catch (err) {
    alert("Error login");
    console.log(err);
  }
};

  return (
    <div>
  <h1 style={{ fontWeight: "bold" }}>ASISYA SYSTEM</h1>

  <h2>Login</h2>

  <form onSubmit={login}>
    <input
      placeholder="user"
      value={username}
      onChange={(e) => setUsername(e.target.value)}
    />

    <input
      type="password"
      placeholder="pass"
      value={password}
      onChange={(e) => setPassword(e.target.value)}
    />

    <button type="submit">Login</button>
  </form>
</div>
);
}