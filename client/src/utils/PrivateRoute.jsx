import React from 'react';
import { Navigate, Outlet } from 'react-router-dom';
import { useAuth } from '../contexts/AuthContext';

const PrivateRoute = ({ children }) => {
  const { isAuthenticated, isLoadingAuth } = useAuth();

  if (isLoadingAuth) {
    return <div>Đang tải trang...</div>; // Hoặc một component Spinner đẹp hơn
  }

  return isAuthenticated ? (children || <Outlet />) : <Navigate to="/login" replace />;
};

export default PrivateRoute;