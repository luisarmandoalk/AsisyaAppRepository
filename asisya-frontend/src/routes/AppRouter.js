import { Routes, Route, Navigate } from "react-router-dom";

import Login from "../pages/Login";
import Products from "../pages/Products";
import ProductForm from "../pages/ProductForm";
import Categories from "../pages/Categories"; 
import CategoryForm from "../pages/CategoryForm";

import AuthGuard from "../auth/AuthGuard";

export default function AppRouter() {

  return (
    <Routes>
      {/* Default */}
      <Route path="/" element={<Navigate to="/login" />} />

      {/* LOGIN */}
      <Route path="/login" element={<Login />} />

      {/* PRODUCTS */}
      <Route
        path="/products"
        element={
          <AuthGuard>
            <Products />
          </AuthGuard>
        }
      />

      {/* CREATE PRODUCT */}
      <Route
        path="/products/new"
        element={
          <AuthGuard>
            <ProductForm />
          </AuthGuard>
        }
      />

      {/* EDIT PRODUCT */}
      <Route
        path="/products/edit/:id"
        element={
          <AuthGuard>
            <ProductForm />
          </AuthGuard>
        }
      />

      {/* CATEGORIES ?? NUEVO */}
		<Route
	  path="/categories"
	  element={
		<AuthGuard>
		  <Categories />
		</AuthGuard>
	  }
	/>
		
		<Route
	  path="/categories/new"
	  element={
		<AuthGuard>
		  <CategoryForm />
		</AuthGuard>
	  }
	/>
	 <Route
        path="/categories/edit/:id"
        element={
          <AuthGuard>
            <CategoryForm />
          </AuthGuard>
        }
      />

      {/* fallback */}
      <Route path="*" element={<Navigate to="/login" />} />
    </Routes>
  );
}