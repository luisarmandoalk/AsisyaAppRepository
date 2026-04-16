import { Routes, Route, Navigate } from "react-router-dom";
import Login from "../pages/Login";
import Products from "../pages/Products";       
import ProductForm from "../pages/ProductForm"; 
import AuthGuard from "../auth/AuthGuard";

export default function AppRouter() {
  return (
    <Routes>
      {/* Default */}
      <Route path="/" element={<Navigate to="/login" />} />

      {/* Login */}
      <Route path="/login" element={<Login />} />

      {/* LISTADO */}
      <Route
        path="/products"
        element={
          <AuthGuard>
            <Products />
          </AuthGuard>
        }
      />

      {/* CREATE */}
      <Route
        path="/products/new"
        element={
          <AuthGuard>
            <ProductForm />
          </AuthGuard>
        }
      />

      {/* EDIT */}
      <Route
        path="/products/edit/:id"
        element={
          <AuthGuard>
            <ProductForm />
          </AuthGuard>
        }
      />

      {/* fallback */}
      <Route path="*" element={<Navigate to="/login" />} />
    </Routes>
  );
}