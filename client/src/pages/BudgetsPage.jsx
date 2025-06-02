import React, { useState, useEffect, useCallback } from 'react';
import * as budgetService from '../services/budgetService';
import * as categoryService from '../services/categoryService'; // Để lấy danh mục cho form
import Modal from '../components/common/Modal';
// import BudgetForm from '../components/budgets/BudgetForm'; // Bạn sẽ tạo component này
import { FaEdit, FaTrash, FaPlusCircle, FaChartBar } from 'react-icons/fa';
import './TableStyles.css';

// Placeholder BudgetForm
const BudgetForm = ({ onSubmit, onCancel, initialData, categories, isLoading }) => {
    const [name, setName] = useState('');
    const [amount, setAmount] = useState('');
    const [startDate, setStartDate] = useState('');
    const [endDate, setEndDate] = useState('');
    const [selectedCategoryId, setSelectedCategoryId] = useState('');

    useEffect(() => {
        if (initialData) {
            setName(initialData.name || '');
            setAmount(initialData.amount?.toString() || '');
            setStartDate(initialData.startDate ? format(new Date(initialData.startDate), 'yyyy-MM-dd') : '');
            setEndDate(initialData.endDate ? format(new Date(initialData.endDate), 'yyyy-MM-dd') : '');
            setSelectedCategoryId(initialData.categoryId?.toString() || '');
        } else {
            setName(''); setAmount(''); setStartDate(''); setEndDate(''); setSelectedCategoryId('');
        }
    }, [initialData]);

    const handleSubmit = (e) => {
        e.preventDefault();
        if (!name.trim() || !amount.trim() || !startDate || !endDate) {
            alert('Vui lòng điền đầy đủ các trường bắt buộc (Tên, Số tiền, Ngày bắt đầu, Ngày kết thúc).');
            return;
        }
        if (new Date(endDate) < new Date(startDate)) {
            alert('Ngày kết thúc không thể trước ngày bắt đầu.');
            return;
        }
        onSubmit({ 
            name, 
            amount: parseFloat(amount), 
            startDate, 
            endDate, 
            categoryId: selectedCategoryId ? parseInt(selectedCategoryId) : null 
        });
    };
    return (
        <form onSubmit={handleSubmit} className="budget-form"> {/* Thêm class budget-form */}
            <div className="form-group"><label htmlFor="budgetName">Tên Ngân Sách:</label><input type="text" id="budgetName" value={name} onChange={e => setName(e.target.value)} required /></div>
            <div className="form-group"><label htmlFor="budgetAmount">Số Tiền:</label><input type="number" id="budgetAmount" value={amount} onChange={e => setAmount(e.target.value)} step="0.01" required /></div>
            <div className="form-group"><label htmlFor="budgetStartDate">Ngày Bắt Đầu:</label><input type="date" id="budgetStartDate" value={startDate} onChange={e => setStartDate(e.target.value)} required /></div>
            <div className="form-group"><label htmlFor="budgetEndDate">Ngày Kết Thúc:</label><input type="date" id="budgetEndDate" value={endDate} onChange={e => setEndDate(e.target.value)} required /></div>
            <div className="form-group"><label htmlFor="budgetCategory">Danh Mục (tùy chọn):</label>
                <select id="budgetCategory" value={selectedCategoryId} onChange={e => setSelectedCategoryId(e.target.value)}>
                    <option value="">Tổng thể (Không chọn)</option>
                    {categories.map(cat => <option key={cat.id} value={cat.id}>{cat.name}</option>)}
                </select>
            </div>
            <div className="form-actions">
                <button type="submit" disabled={isLoading} className="button-primary"><FaSave /> {isLoading ? 'Đang lưu...' : (initialData ? 'Cập nhật' : 'Thêm mới')}</button>
                <button type="button" onClick={onCancel} disabled={isLoading} className="button-secondary"><FaTimes /> Hủy</button>
            </div>
        </form>
    );
};

const BudgetsPage = () => {
    const [budgets, setBudgets] = useState([]);
    const [categories, setCategories] = useState([]); // Để chọn category cho budget
    const [isLoading, setIsLoading] = useState(true);
    const [error, setError] = useState('');
    
    const [isModalOpen, setIsModalOpen] = useState(false);
    const [editingBudget, setEditingBudget] = useState(null);
    const [isSubmitting, setIsSubmitting] = useState(false);
  
    const fetchData = useCallback(async () => {
      setIsLoading(true);
      setError('');
      try {
        const [budgetsData, categoriesData] = await Promise.all([
          budgetService.getMyBudgets(),
          categoryService.getMyCategories()
        ]);
        setBudgets(budgetsData || []);
        setCategories(categoriesData || []);
      } catch (err) {
        setError('Không thể tải danh sách ngân sách hoặc danh mục.');
        console.error(err);
      } finally {
        setIsLoading(false);
      }
    }, []);
  
    useEffect(() => {
      fetchData();
    }, [fetchData]);
  
    const handleOpenModal = (budget = null) => {
      setEditingBudget(budget);
      setIsModalOpen(true);
    };
    const handleCloseModal = () => {
      setIsModalOpen(false);
      setEditingBudget(null);
    };
  
    const handleSubmitBudget = async (formData) => {
      setIsSubmitting(true);
      setError('');
      try {
        if (editingBudget) {
          await budgetService.updateBudget(editingBudget.id, formData);
          alert('Cập nhật ngân sách thành công!');
        } else {
          await budgetService.createBudget(formData);
          alert('Thêm ngân sách mới thành công!');
        }
        fetchData();
        handleCloseModal();
      } catch (err) {
        const errMessage = err.response?.data?.message || (editingBudget ? 'Lỗi khi cập nhật ngân sách.' : 'Lỗi khi thêm ngân sách mới.');
        setError(errMessage);
        alert(errMessage);
        console.error(err);
      } finally {
        setIsSubmitting(false);
      }
    };
  
    const handleDeleteBudget = async (budgetId) => {
      if (window.confirm('Bạn có chắc chắn muốn xóa ngân sách này?')) {
        try {
          await budgetService.deleteBudget(budgetId);
          setBudgets(prev => prev.filter(b => b.id !== budgetId));
          alert('Xóa ngân sách thành công!');
        } catch (err) {
          alert('Lỗi khi xóa ngân sách.');
          console.error(err);
        }
      }
    };
    
    const ProgressBar = ({ progress, spent, total }) => {
      const percentage = Math.min(Math.max(progress * 100, 0), 100); // Đảm bảo từ 0-100
      let barColor = '#28a745'; // Green
      if (percentage > 75 && percentage <= 90) barColor = '#ffc107'; // Yellow
      else if (percentage > 90) barColor = '#dc3545'; // Red
  
      return (
          <div className="progress-bar-container">
              <div className="progress-bar-labels">
                  <span>{spent.toLocaleString('vi-VN')}đ</span>
                  <span>{total.toLocaleString('vi-VN')}đ</span>
              </div>
              <div className="progress-bar-background">
                  <div className="progress-bar-fill" style={{ width: `${percentage}%`, backgroundColor: barColor }}></div>
              </div>
              <div className="progress-bar-percentage">{percentage.toFixed(1)}%</div>
          </div>
      );
    };
  
  
    if (isLoading) return <p>Đang tải ngân sách...</p>;
  
    return (
      <div>
        <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: '20px' }}>
          <h2>Quản Lý Ngân Sách</h2>
          <button onClick={() => handleOpenModal()} className="action-button add-new">
            <FaPlusCircle /> Thêm Ngân Sách
          </button>
        </div>
        {error && budgets.length === 0 && <p style={{ color: 'red' }}>{error}</p>}
  
        {budgets.length === 0 && !isLoading ? (
          <p>Bạn chưa có ngân sách nào. Hãy thêm mới!</p>
        ) : (
          <div className="budgets-grid">
              {budgets.map(budget => (
                  <div key={budget.id} className="budget-card">
                      <h3>{budget.name}</h3>
                      <p>Danh mục: {budget.categoryName || 'Tất cả'}</p>
                      <p>Thời gian: {new Date(budget.startDate).toLocaleDateString('vi-VN')} - {new Date(budget.endDate).toLocaleDateString('vi-VN')}</p>
                      <ProgressBar progress={budget.progress} spent={budget.spentAmount} total={budget.amount} />
                      <div className="budget-card-actions">
                          <button onClick={() => handleOpenModal(budget)} className="action-button edit small"><FaEdit /> Sửa</button>
                          <button onClick={() => handleDeleteBudget(budget.id)} className="action-button delete small"><FaTrash /> Xóa</button>
                      </div>
                  </div>
              ))}
          </div>
        )}
  
        <Modal isOpen={isModalOpen} onClose={handleCloseModal} title={editingBudget ? 'Chỉnh Sửa Ngân Sách' : 'Thêm Ngân Sách Mới'}>
          <BudgetForm 
            onSubmit={handleSubmitBudget} 
            onCancel={handleCloseModal}
            initialData={editingBudget}
            categories={categories}
            isLoading={isSubmitting}
          />
          {error && isModalOpen && <p style={{ color: 'red', marginTop: '10px' }}>{error}</p>}
        </Modal>
      </div>
    );
  };

  export default BudgetsPage;