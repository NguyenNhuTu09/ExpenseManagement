import React, { useState, useEffect } from 'react';
import { FaPalette, FaSave, FaTimes } from 'react-icons/fa'; // Ví dụ dùng react-icons

const CategoryForm = ({ onSubmit, onCancel, initialData, isLoading }) => {
  const [name, setName] = useState('');
  const [icon, setIcon] = useState(''); // Bạn có thể dùng một icon picker component ở đây

  useEffect(() => {
    if (initialData) {
      setName(initialData.name || '');
      setIcon(initialData.icon || '');
    } else {
      setName('');
      setIcon('');
    }
  }, [initialData]);

  const handleSubmit = (e) => {
    e.preventDefault();
    if (!name.trim()) {
      alert('Tên danh mục không được để trống.');
      return;
    }
    onSubmit({ name, icon });
  };

  return (
    <form onSubmit={handleSubmit} className="category-form">
      <div className="form-group">
        <label htmlFor="categoryName">Tên Danh Mục:</label>
        <input
          type="text"
          id="categoryName"
          value={name}
          onChange={(e) => setName(e.target.value)}
          placeholder="Ví dụ: Ăn uống, Du lịch"
          required
        />
      </div>
      <div className="form-group">
        <label htmlFor="categoryIcon">Icon (tùy chọn):</label>
        <div style={{display: 'flex', alignItems: 'center'}}>
          {/* <FaPalette size={20} style={{marginRight: '10px'}}/>  Ví dụ icon */}
          <input
            type="text"
            id="categoryIcon"
            value={icon}
            onChange={(e) => setIcon(e.target.value)}
            placeholder="Tên icon (ví dụ: coffee, car)"
          />
        </div>
      </div>
      <div className="form-actions">
        <button type="submit" disabled={isLoading} className="button-primary">
          <FaSave /> {isLoading ? 'Đang lưu...' : (initialData ? 'Cập nhật' : 'Thêm mới')}
        </button>
        <button type="button" onClick={onCancel} disabled={isLoading} className="button-secondary">
          <FaTimes /> Hủy
        </button>
      </div>
    </form>
  );
};

export default CategoryForm;