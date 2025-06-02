import React from 'react';
import { Link } from 'react-router-dom';
import './NotFoundPage.css'; // Tạo file CSS này

const NotFoundPage = () => {
  return (
    <div className="not-found-container">
      <h1>404</h1>
      <h2>Trang không tồn tại</h2>
      <p>Xin lỗi, chúng tôi không thể tìm thấy trang bạn yêu cầu.</p>
      <Link to="/" className="go-home-button">Quay về Trang Chủ</Link>
    </div>
  );
};

export default NotFoundPage;