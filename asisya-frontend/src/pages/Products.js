import { useEffect, useState } from "react";
import api from "../api/api";
import { useNavigate } from "react-router-dom";

export default function Products() {
  const [products, setProducts] = useState([]);
  const [page, setPage] = useState(1);
  const [total, setTotal] = useState(0);

  // filtros
  const [search, setSearch] = useState("");
  const [minPrice, setMinPrice] = useState("");
  const [maxPrice, setMaxPrice] = useState("");

  const pageSize = 20;
  const navigate = useNavigate();

  const load = async () => {
    try {
      const res = await api.get("/product", {
        params: {
          page,
          pageSize,
          search,
          minPrice,
          maxPrice
        }
      });

      setProducts(res.data.data);
      setTotal(res.data.total);

    } catch (error) {
      console.log("Error cargando productos", error);
    }
  };

  useEffect(() => {
    load();
  }, [page]);

  const totalPages = Math.ceil(total / pageSize);

  return (
    <div style={{ padding: "20px" }}>
	<div>
	  <button onClick={() => navigate("/products")}>Products</button>
	  <button onClick={() => navigate("/categories")}>Categories</button>
	</div>
      <h2>Listado de Productos</h2>

      <div>
        <input
          placeholder="Buscar por nombre"
          value={search}
          onChange={(e) => setSearch(e.target.value)}
        />

        <input
          type="number"
          placeholder="Precio MIN"
          value={minPrice}
          onChange={(e) => setMinPrice(e.target.value)}
        />

        <input
          type="number"
          placeholder="Precio MAX"
          value={maxPrice}
          onChange={(e) => setMaxPrice(e.target.value)}
        />

        <button onClick={() => { setPage(1); load(); }}>
          Buscar
        </button>
      </div>

      <button onClick={() => navigate("/products/new")}>
        Nuevo Producto
      </button>

      <table border="1" cellPadding="10" style={{ marginTop: "20px" }}>
        <thead>
          <tr>
            <th>Nombre</th>
            <th>Precio</th>
			<th>Categoria</th>			
			<th>Imagen Categoria</th>
			<th>Acciones</th> 
          </tr>
        </thead>

       <tbody>
	  {Array.isArray(products) && products.map((p) => (
		<tr key={p.id}>
		  <td>{p.productName}</td>
		  <td>{p.unitPrice}</td>
		  <td>{p.categoryName}</td> 		  
		  <td>
		  {p.categoryImageUrl ? (
			<img
			  src={p.categoryImageUrl}
			  width="50"
			  height="50"
			/>
		  ) : (
			"Sin imagen"
			  )}
		</td>
		<td>
			<button
				onClick={() => navigate(`/products/edit/${p.id}`)}
			>
				  Editar
				</button>
			  </td>
			</tr>
	  ))}
	</tbody>
      </table>

      
      <div style={{ marginTop: "20px" }}>
        <button onClick={() => setPage(page - 1)} disabled={page === 1}>
          Anterior
        </button>

        <span style={{ margin: "0 10px" }}>
          Pagina {page} de {totalPages}
        </span>

        <button onClick={() => setPage(page + 1)} disabled={page === totalPages}>
          Siguiente
        </button>
      </div>
    </div>
  );
}