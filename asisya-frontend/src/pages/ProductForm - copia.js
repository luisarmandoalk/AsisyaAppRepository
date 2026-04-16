import { useState } from "react";
import api from "../api/api";
import { useNavigate } from "react-router-dom";

export default function ProductForm() {
  const navigate = useNavigate();

  const [form, setForm] = useState({
    productName: "",
    unitPrice: 0,
    categoryId: "",
    supplierId: ""
  });

  const handleSubmit = async (e) => {
    e.preventDefault();

    if (!form.productName || form.unitPrice <= 0) {
      alert("Invalid data");
      return;
    }

    await api.post("/product", form);
    navigate("/products");
  };

  return (
    <form onSubmit={handleSubmit}>
      <input
        placeholder="Name"
        onChange={(e) => setForm({ ...form, productName: e.target.value })}
      />

      <input
        type="number"
        placeholder="Price"
        onChange={(e) => setForm({ ...form, unitPrice: e.target.value })}
      />

      <input
        placeholder="CategoryId"
        onChange={(e) => setForm({ ...form, categoryId: e.target.value })}
      />

      <input
        placeholder="SupplierId"
        onChange={(e) => setForm({ ...form, supplierId: e.target.value })}
      />

      <button>Create</button>
    </form>
  );
}