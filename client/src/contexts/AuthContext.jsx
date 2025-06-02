import React, { createContext, useState, useEffect, useContext, useCallback } from 'react';
import { TOKEN_KEY, USER_INFO_KEY } from '../utils/constants';
import apiClient from '../services/apiClient'; // Sử dụng apiClient đã cấu hình

const AuthContext = createContext(null);

export const useAuth = () => useContext(AuthContext);

export const AuthProvider = ({ children }) => {
  const [currentUser, setCurrentUser] = useState(null);
  const [token, setToken] = useState(localStorage.getItem(TOKEN_KEY));
  const [isLoading, setIsLoading] = useState(true); // Để kiểm tra trạng thái auth ban đầu

  const login = async (loginIdentifier, password) => {
    try {
      const response = await apiClient.post('/auth/login', { loginIdentifier, password });
      const { token: apiToken, user: userInfo } = response.data;
      localStorage.setItem(TOKEN_KEY, apiToken);
      localStorage.setItem(USER_INFO_KEY, JSON.stringify(userInfo));
      setToken(apiToken);
      setCurrentUser(userInfo);
      return userInfo;
    } catch (error) {
      console.error("Login failed:", error.response?.data || error.message);
      throw error;
    }
  };

  const register = async (username, email, password, fullName) => {
    try {
      const response = await apiClient.post('/auth/register', { username, email, password, fullName });
      return response.data;
    } catch (error) {
      console.error("Registration failed:", error.response?.data || error.message);
      throw error;
    }
  };

  const logout = useCallback(() => {
    localStorage.removeItem(TOKEN_KEY);
    localStorage.removeItem(USER_INFO_KEY);
    setToken(null);
    setCurrentUser(null);
   
  }, []);

  useEffect(() => {
    const initializeAuth = () => {
      const storedToken = localStorage.getItem(TOKEN_KEY);
      const storedUser = localStorage.getItem(USER_INFO_KEY);
      if (storedToken && storedUser) {
        setToken(storedToken);
        try {
          setCurrentUser(JSON.parse(storedUser));
        } catch (e) {
          console.error("Failed to parse stored user info", e);
          logout(); // Nếu thông tin user bị lỗi, logout
        }
      }
      setIsLoading(false);
    };
    initializeAuth();

    // Lắng nghe sự kiện 401 từ apiClient interceptor
    const handleAuthError = () => {
        logout();
    };
    window.addEventListener('auth-error-401', handleAuthError);
    return () => {
        window.removeEventListener('auth-error-401', handleAuthError);
    };

  }, [logout]);

  const value = {
    currentUser,
    setCurrentUser, 
    token,
    login,
    register,
    logout,
    isAuthenticated: !!token,
    isLoadingAuth: isLoading,
  };

  return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
};