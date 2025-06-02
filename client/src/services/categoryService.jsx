import apiClient from './apiClient';

export const getMyCategories = async () => {
    const response = await apiClient.get('/categories');
    return response.data;
};

export const createCategory = async (categoryData) => {
    // categoryData: { name: string, icon?: string }
    const response = await apiClient.post('/categories', categoryData);
    return response.data;
};

export const updateCategory = async (categoryId, categoryData) => {
    const response = await apiClient.put(`/categories/${categoryId}`, categoryData);
    return response.data; // Backend trả về NoContent (204), nên có thể không có data
};

export const deleteCategory = async (categoryId) => {
    await apiClient.delete(`/categories/${categoryId}`);
    // Backend trả về NoContent (204)
};

export const getCategoryById = async (categoryId) => {
    const response = await apiClient.get(`/categories/${categoryId}`);
    return response.data;
};