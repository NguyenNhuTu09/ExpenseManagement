import React, { useState, useEffect, useCallback } from 'react';
import * as categoryService from '../services/categoryService';
import Modal from '../components/common/Modal';
import CategoryForm from '../components/categories/CategoryForm';
import { FaEdit, FaTrash, FaPlusCircle } from 'react-icons/fa';
import './TableStyles.css'; // Sử dụng chung CSS

const CategoriesPage = () => {
  const [categories, setCategories] = useState([]);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState('');
  
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [editingCategory, setEditingCategory] = useState(null); // null: add new, object: edit
  const [isSubmitting, setIsSubmitting] = useState(false);

  const fetchCategories = useCallback(async () => {
    setIsLoading(true);
    setError('');
    try {
      const data = await categoryService.getMyCategories();
      setCategories(data || []);
    } catch (err) {
      setError('Không thể tải danh sách danh mục.');
      console.error(err);
    } finally {
      setIsLoading(false);
    }
  }, []);

  useEffect(() => {
    fetchCategories();
  }, [fetchCategories]);

  const handleOpenModal = (category = null) => {
    setEditingCategory(category);
    setIsModalOpen(true);
  };

  const handleCloseModal = () => {
    setIsModalOpen(false);
    setEditingCategory(null);
  };

  const handleSubmitCategory = async (formData) => {
    setIsSubmitting(true);
    setError('');
    try {
      if (editingCategory) {
        await categoryService.updateCategory(editingCategory.id, formData);
        alert('Cập nhật danh mục thành công!');
      } else {
        await categoryService.createCategory(formData);
        alert('Thêm danh mục mới thành công!');
      }
      fetchCategories(); // Tải lại danh sách
      handleCloseModal();
    } catch (err) {
      const errMessage = err.response?.data?.message || err.response?.data?.Message || (editingCategory ? 'Lỗi khi cập nhật danh mục.' : 'Lỗi khi thêm danh mục mới.');
      setError(errMessage); // Hiển thị lỗi trong modal hoặc alert
      alert(errMessage); 
      console.error(err);
    } finally {
      setIsSubmitting(false);
    }
  };

  const handleDeleteCategory = async (categoryId) => {
    if (window.confirm('Bạn có chắc chắn muốn xóa danh mục này? Các chi tiêu thuộc danh mục này có thể bị ảnh hưởng (nếu backend không cho xóa).')) {
      try {
        await categoryService.deleteCategory(categoryId);
        setCategories(prev => prev.filter(cat => cat.id !== categoryId));
        alert('Xóa danh mục thành công!');
      } catch (err) {
        const errMessage = err.response?.data?.message || err.response?.data?.Message || 'Lỗi khi xóa danh mục.';
        alert(errMessage);
        console.error(err);
      }
    }
  };

  if (isLoading) return <p>Đang tải danh mục...</p>;
  // if (error && categories.length === 0) return <p style={{ color: 'red' }}>{error}</p>; // Chỉ hiển thị lỗi nếu không có dữ liệu

  return (
    <div>
      <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: '20px' }}>
        <h2>Quản Lý Danh Mục Chi Tiêu</h2>
        <button onClick={() => handleOpenModal()} className="action-button add-new">
          <FaPlusCircle /> Thêm Danh Mục
        </button>
      </div>
      {error && categories.length === 0 && <p style={{ color: 'red' }}>{error}</p>}

      {categories.length === 0 && !isLoading ? (
        <p>Bạn chưa có danh mục nào. Hãy thêm mới!</p>
      ) : (
        <table className="data-table">
          <thead>
            <tr>
              <th>Tên Danh Mục</th>
              <th>Icon</th>
              <th>Hành động</th>
            </tr>
          </thead>
          <tbody>
            {categories.map(category => (
              <tr key={category.id}>
                <td>{category.name}</td>
                <td>{category.icon || '-'}</td>
                <td>
                  <button onClick={() => handleOpenModal(category)} className="action-button edit">
                    <FaEdit /> Sửa
                  </button>
                  <button onClick={() => handleDeleteCategory(category.id)} className="action-button delete">
                    <FaTrash /> Xóa
                  </button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      )}

      <Modal isOpen={isModalOpen} onClose={handleCloseModal} title={editingCategory ? 'Chỉnh Sửa Danh Mục' : 'Thêm Danh Mục Mới'}>
        <CategoryForm 
          onSubmit={handleSubmitCategory} 
          onCancel={handleCloseModal}
          initialData={editingCategory}
          isLoading={isSubmitting}
        />
        {error && isModalOpen && <p style={{ color: 'red', marginTop: '10px' }}>{error}</p>}
      </Modal>
    </div>
  );
};

export default CategoriesPage;