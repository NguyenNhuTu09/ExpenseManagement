import React from 'react';
import { useAuth } from '../contexts/AuthContext';

const DashboardPage = () => {
  const { currentUser } = useAuth();

  return (
    <div>
      <h1>Chào mừng đến Bảng Điều Khiển, {currentUser?.fullName || currentUser?.username}!</h1>
      <p>Đây là nơi bạn có thể xem tổng quan về chi tiêu của mình.</p>
    </div>
  );
};

export default DashboardPage;