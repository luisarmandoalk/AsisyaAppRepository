import { useState, useEffect } from "react";
import api from "../api/api";
import { useNavigate, useParams } from "react-router-dom";

export default function CategoryForm() {
  const [form, setForm] = useState({
    categoryName: "",
	 description: "",
    picture: ""
  });

  const navigate = useNavigate();
  const { id } = useParams();

  useEffect(() => {
    if (id) load();
  }, [id]);

  const load = async () => {
    const res = await api.get(`/category/${id}`);
    setForm(res.data);
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    if (!form.categoryName) {
      alert("Nombre requerido");
      return;
    }

    if (id) {
      await api.put(`/category/${id}`, form);
    } else {
      await api.post("/category", form);
    }

    navigate("/categories");
  };

  return (
    <form onSubmit={handleSubmit}>
      <h2>{id ? "Editar" : "Nueva"} Categoría</h2>

      <input
        placeholder="Nombre"
        value={form.categoryName}
        onChange={(e) =>
          setForm({ ...form, categoryName: e.target.value })
        }
      />
		<input
		  placeholder="Description"
		  value={form.description}
		  onChange={(e) =>
			setForm({ ...form, description: e.target.value })
		  }
		/>

      <input
        placeholder="Imagen"
        value={form.picture}
        onChange={(e) =>
          setForm({ ...form, picture: e.target.value })
        }
      />

      <button>Guardar</button>
    </form>
  );
}