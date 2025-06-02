import apiClient from './apiClient';
// import { format } from 'date-fns'; // Không cần format ở đây vì backend nhận DateTime

export const getMyBudgets = async () => {
    const response = await apiClient.get('/budgets');
    return response.data;
};

export const createBudget = async (budgetData) => {
    // budgetData: { name: string, amount: number, startDate: string (YYYY-MM-DD), endDate: string (YYYY-MM-DD), categoryId?: number }
    const response = await apiClient.post('/budgets', budgetData);
    return response.data;
};

export const getBudgetById = async (budgetId) => {
    const response = await apiClient.get(`/budgets/${budgetId}`);
    return response.data;
};

export const updateBudget = async (budgetId, budgetData) => {
    await apiClient.put(`/budgets/${budgetId}`, budgetData);
    // Backend trả về NoContent (204)
};

export const deleteBudget = async (budgetId) => {
    await apiClient.delete(`/budgets/${budgetId}`);
    // Backend trả về NoContent (204)
};