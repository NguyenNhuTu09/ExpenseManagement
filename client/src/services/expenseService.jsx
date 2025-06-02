import apiClient from './apiClient';
import { format } from 'date-fns'; // Ví dụ sử dụng date-fns để format ngày

export const getMyExpenses = async (params) => {
    const queryParams = { ...params };
    if (queryParams.startDate) {
        queryParams.startDate = format(new Date(queryParams.startDate), 'yyyy-MM-dd');
    }
    if (queryParams.endDate) {
        queryParams.endDate = format(new Date(queryParams.endDate), 'yyyy-MM-dd');
    }
    const response = await apiClient.get('/expenses', { params: queryParams });
    return response.data;
};

export const createExpense = async (expenseData) => {
    const dataToSubmit = {
        ...expenseData,
        date: format(new Date(expenseData.date), 'yyyy-MM-dd HH:mm:ss') // Backend có thể cần ISO 8601
    };
    const response = await apiClient.post('/expenses', dataToSubmit);
    return response.data;
};

export const getExpenseById = async (expenseId) => {
    const response = await apiClient.get(`/expenses/${expenseId}`);
    return response.data;
};

export const updateExpense = async (expenseId, expenseData) => {
     const dataToSubmit = {
        ...expenseData,
        date: format(new Date(expenseData.date), 'yyyy-MM-dd HH:mm:ss')
    };
    await apiClient.put(`/expenses/${expenseId}`, dataToSubmit);
};

export const deleteExpense = async (expenseId) => {
    await apiClient.delete(`/expenses/${expenseId}`);
};

export const getTotalExpenses = async (startDate, endDate, categoryId) => {
    const params = {
        startDate: format(new Date(startDate), 'yyyy-MM-dd'),
        endDate: format(new Date(endDate), 'yyyy-MM-dd'),
        categoryId: categoryId || undefined // Gửi undefined nếu không có categoryId
    };
    const response = await apiClient.get('/expenses/total', { params });
    return response.data; // { totalAmount: number }
};
