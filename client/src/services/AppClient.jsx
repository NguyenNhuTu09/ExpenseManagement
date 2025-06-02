import axios from 'axios';
import { API_BASE_URL, TOKEN_KEY } from '../utils/constants';

const apiClient = axios.create({
  baseURL: API_BASE_URL,
  headers: {
    'Content-Type': 'application/json',
  },
});

apiClient.interceptors.request.use(
  (config) => {
    const token = localStorage.getItem(TOKEN_KEY);
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

apiClient.interceptors.response.use(
  (response) => response,
  (error) => {
    if (error.response && error.response.status === 401) {
      localStorage.removeItem(TOKEN_KEY);
      localStorage.removeItem('expense_management_user_info'); // Đảm bảo key này nhất quán
      
      console.error('Unauthorized, logging out.');
      const event = new Event('auth-error-401');
      window.dispatchEvent(event);
    }
    return Promise.reject(error);
  }
);


export default apiClient;