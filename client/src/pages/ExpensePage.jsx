import React, { useState, useEffect, useCallback } from 'react';
import * as expenseService from '../services/expenseService';
import * as categoryService from '../services/categoryService'; // Để lấy danh sách category cho filter
import { Link } from 'react-router-dom'; // Nếu có trang chi tiết hoặc form thêm/sửa riêng
import './TableStyles.css'; // Tạo file CSS này cho bảng

const ExpensesPage = () => {
  const [expenses, setExpenses] = useState([]);
  const [categories, setCategories] = useState([]);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState('');
  
  // Filters state
  const [filterStartDate, setFilterStartDate] = useState('');
  const [filterEndDate, setFilterEndDate] = useState('');
  const [filterCategoryId, setFilterCategoryId] = useState('');

  const fetchExpensesAndCategories = useCallback(async () => {
    setIsLoading(true);
    setError('');
    try {
      const params = {};
      if (filterStartDate) params.startDate = filterStartDate;
      if (filterEndDate) params.endDate = filterEndDate;
      if (filterCategoryId) params.categoryId = parseInt(filterCategoryId);
      // Thêm sortBy, ascending nếu cần
      
      const [expensesData, categoriesData] = await Promise.all([
        expenseService.getMyExpenses(params),
        categoryService.getMyCategories() // Lấy danh mục cho dropdown filter
      ]);
      setExpenses(expensesData || []);
      setCategories(categoriesData || []);
    } catch (err) {
      setError('Không thể tải danh sách chi tiêu hoặc danh mục.');
      console.error(err);
    } finally {
      setIsLoading(false);
    }
  }, [filterStartDate, filterEndDate, filterCategoryId]);

  useEffect(() => {
    fetchExpensesAndCategories();
  }, [fetchExpensesAndCategories]);

  const handleDeleteExpense = async (expenseId) => {
    if (window.confirm('Bạn có chắc chắn muốn xóa chi tiêu này?')) {
      try {
        await expenseService.deleteExpense(expenseId);
        setExpenses(prevExpenses => prevExpenses.filter(exp => exp.id !== expenseId));
        alert('Xóa chi tiêu thành công!');
      } catch (err) {
        alert('Lỗi khi xóa chi tiêu.');
        console.error(err);
      }
    }
  };
  
  const handleFilterSubmit = (e) => {
    e.preventDefault();
    fetchExpensesAndCategories(); // Gọi lại hàm fetch với filter mới
  };

  if (isLoading) return <p>Đang tải chi tiêu...</p>;
  if (error) return <p style={{ color: 'red' }}>{error}</p>;

  return (
    <div>
      <h2>Quản Lý Chi Tiêu</h2>
      {/* Nút để mở form thêm chi tiêu (có thể là modal hoặc trang riêng) */}
      {/* <Link to="/expenses/new">Thêm Chi Tiêu Mới</Link> */}
      <button onClick={() => alert("Chức năng Thêm mới đang được phát triển")}>Thêm Chi Tiêu Mới</button>

      <form onSubmit={handleFilterSubmit} className="filter-form">
        <h3>Bộ lọc</h3>
        <div className="filter-group">
          <label htmlFor="startDate">Từ ngày:</label>
          <input type="date" id="startDate" value={filterStartDate} onChange={e => setFilterStartDate(e.target.value)} />
        </div>
        <div className="filter-group">
          <label htmlFor="endDate">Đến ngày:</label>
          <input type="date" id="endDate" value={filterEndDate} onChange={e => setFilterEndDate(e.target.value)} />
        </div>
        <div className="filter-group">
          <label htmlFor="category">Danh mục:</label>
          <select id="category" value={filterCategoryId} onChange={e => setFilterCategoryId(e.target.value)}>
            <option value="">Tất cả</option>
            {categories.map(cat => (
              <option key={cat.id} value={cat.id}>{cat.name}</option>
            ))}
          </select>
        </div>
        <button type="submit">Lọc</button>
      </form>

      {expenses.length === 0 ? (
        <p>Không có chi tiêu nào để hiển thị.</p>
      ) : (
        <table className="data-table">
          <thead>
            <tr>
              <th>Mô tả</th>
              <th>Số tiền</th>
              <th>Ngày</th>
              <th>Danh mục</th>
              <th>Ghi chú</th>
              <th>Hành động</th>
            </tr>
          </thead>
          <tbody>
            {expenses.map(expense => (
              <tr key={expense.id}>
                <td>{expense.description}</td>
                <td>{expense.amount.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' })}</td>
                <td>{new Date(expense.date).toLocaleDateString('vi-VN')}</td>
                <td>{expense.categoryName}</td>
                <td>{expense.notes || '-'}</td>
                <td>
                  {/* <Link to={`/expenses/edit/${expense.id}`}>Sửa</Link> */}
                  <button onClick={() => alert(`Sửa expense ${expense.id} (đang phát triển)`)} className="action-button edit">Sửa</button>
                  <button onClick={() => handleDeleteExpense(expense.id)} className="action-button delete">Xóa</button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      )}
    </div>
  );
};
export default ExpensesPage;