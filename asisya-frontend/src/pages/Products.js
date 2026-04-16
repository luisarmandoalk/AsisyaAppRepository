import { useEffect, useState } from "react";
import api from "../api/api";
import { useNavigate } from "react-router-dom";

export default function Products() {
  const [products, setProducts] = useState([]);
  const [page, setPage] = useState(1);
  const [total, setTotal] = useState(0);
  const pageSize = 20;

  const navigate = useNavigate();

  const load = async (currentPage = 1) => {
    try {
      const res = await api.get(
        `/product?page=${currentPage}&pageSize=${pageSize}`
      );

      setProducts(res.data.data); // ?? importante
      setTotal(res.data.total);

    } catch (error) {
      console.log("Error cargando productos", error);
    }
  };

  useEffect(() => {
    load(page);
  }, [page]);

  const totalPages = Math.ceil(total / pageSize);

  return (
    <div style={{ padding: "20px" }}>
      <h2>Listado de Productos</h2>

      <button onClick={() => navigate("/products/new")}>
         Nuevo Producto
      </button>

      <table border="1" cellPadding="10" style={{ marginTop: "20px" }}>
        <thead>
          <tr>
            <th>Nombre</th>
            <th>Precio</th>
            <th>Acciones</th>
          </tr>
        </thead>

        <tbody>
          {Array.isArray(products) &&
            products.map((p) => (
              <tr key={p.id}>
                <td>{p.productName}</td>
                <td>{p.unitPrice}</td>

                <td>
                  <button onClick={() => navigate(`/products/edit/${p.id}`)}>
                    Editar
                  </button>
                </td>
              </tr>
            ))}
        </tbody>
      </table>

      {/* PAGINACIÓN */}
      <div style={{ marginTop: "20px" }}>
        <button
          onClick={() => setPage(page - 1)}
          disabled={page === 1}
        >
           Anterior
        </button>

        <span style={{ margin: "0 10px" }}>
          Página {page} de {totalPages || 1}
        </span>

        <button
          onClick={() => setPage(page + 1)}
          disabled={page === totalPages}
        >
          Siguiente 
        </button>
      </div>
    </div>
  );
}