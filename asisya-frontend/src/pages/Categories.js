import { useEffect, useState } from "react";
import api from "../api/api";
import { useNavigate } from "react-router-dom";

export default function Categories() {
  const [categories, setCategories] = useState([]);
  const navigate = useNavigate();

  const load = async () => {
    const res = await api.get("/category");
    setCategories(res.data.data);
  };

  useEffect(() => {
    load();
  }, []);

  return (
   <div style={{ padding: "20px" }}>
      <div>
        <button onClick={() => navigate("/products")}>Products</button>
        <button onClick={() => navigate("/categories")}>Categories</button>
      </div>

      <h2>Categorias</h2>

      <button onClick={() => navigate("/categories/new")}>
        Nueva Categoria
      </button>

      <table border="1">
        <thead>
          <tr>
            <th>Nombre</th>
			<th>Descripcion</th>
			<th>Imagen Categoria</th>
            <th>Acciones</th>
          </tr>
        </thead>

        <tbody>
          {categories.map((c) => (
            <tr key={c.categoryId}>
              <td>{c.categoryName}</td>
			  <td>{c.description}</td>
			    <td>
		  {c.picture ? (
			<img
			  src={c.picture}
			  width="50"
			  height="50"
			/>
		  ) : (
			"Sin imagen"
			  )}
		</td>
               <td>
				<button
				  onClick={() => navigate(`/categories/edit/${c.categoryId}`)}
				>
				  Editar
				</button>
			  </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}