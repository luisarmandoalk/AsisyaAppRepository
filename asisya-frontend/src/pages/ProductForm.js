import { useEffect, useState } from "react";
import api from "../api/api";
import { useNavigate, useParams } from "react-router-dom";

export default function ProductForm() {
  const navigate = useNavigate();
  const { id } = useParams(); // ?? detecta edición

  const [form, setForm] = useState({
    productName: "",
    unitPrice: "",
    categoryId: "",
    supplierId: ""
  });

  const [errors, setErrors] = useState({});

  // ?? CARGAR PRODUCTO (EDIT)
  useEffect(() => {
    if (id) {
      api.get(`/product/${id}`).then(res => {
        setForm(res.data);
      });
    }
  }, [id]);

  // ?? CAMBIOS
  const handleChange = (e) => {
    setForm({
      ...form,
      [e.target.name]: e.target.value
    });
  };

  // ? VALIDACIÓN
  const validate = () => {
    let err = {};

    if (!form.productName)
      err.productName = "Nombre requerido";

    if (!form.unitPrice || Number(form.unitPrice) <= 0)
      err.unitPrice = "Precio inválido";

    if (!form.categoryId)
      err.categoryId = "Category requerida";

    if (!form.supplierId)
      err.supplierId = "Supplier requerido";

    setErrors(err);
    return Object.keys(err).length === 0;
  };

  // ?? GUARDAR
  const handleSubmit = async (e) => {
    e.preventDefault();

    if (!validate()) return;

    try {
      const payload = {
        ...form,
        unitPrice: Number(form.unitPrice) // ?? FIX importante
      };

      if (id) {
        await api.put(`/product/${id}`, payload);
      } else {
        await api.post("/product", payload);
      }

      navigate("/products");

    } catch (err) {
      console.log(err);
      alert("Error guardando");
    }
  };

  return (
    <div>
      <h2>{id ? "Editar Producto" : "Nuevo Producto"}</h2>

      <form onSubmit={handleSubmit}>
        <input
          name="productName"
          placeholder="Name"
          value={form.productName}
          onChange={handleChange}
        />
        <p>{errors.productName}</p>

        <input
          name="unitPrice"
          type="number"
          placeholder="Price"
          value={form.unitPrice}
          onChange={handleChange}
        />
        <p>{errors.unitPrice}</p>

        <input
          name="categoryId"
          placeholder="CategoryId"
          value={form.categoryId}
          onChange={handleChange}
        />
        <p>{errors.categoryId}</p>

        <input
          name="supplierId"
          placeholder="SupplierId"
          value={form.supplierId}
          onChange={handleChange}
        />
        <p>{errors.supplierId}</p>

        <button type="submit">
          {id ? "Actualizar" : "Crear"}
        </button>
      </form>
    </div>
  );
}