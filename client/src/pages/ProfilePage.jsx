import React from 'react';
import { useAuth } from '../contexts/AuthContext';
import './ProfilePage.css'; // Tạo file CSS này

const ProfilePage = () => {
  const { currentUser, logout } = useAuth();
  // const navigate = useNavigate(); // Nếu cần điều hướng sau khi cập nhật

  if (!currentUser) {
    return <p>Đang tải thông tin người dùng...</p>;
  }

  const handleUpdateProfile = () => {
    // Logic cập nhật profile (cần API backend tương ứng)
    alert('Chức năng cập nhật thông tin đang được phát triển. Backend cần API /api/users/me (PUT).');
  };

  return (
    <div className="profile-container">
      <h2>Thông Tin Tài Khoản</h2>
      <div className="profile-info-card">
        <div className="info-row">
          <span className="info-label">Tên đăng nhập:</span>
          <span className="info-value">{currentUser.username}</span>
        </div>
        <div className="info-row">
          <span className="info-label">Email:</span>
          <span className="info-value">{currentUser.email}</span>
        </div>
        <div className="info-row">
          <span className="info-label">Họ và tên:</span>
          <span className="info-value">{currentUser.fullName || 'Chưa cập nhật'}</span>
        </div>
        <div className="info-row">
          <span className="info-label">Vai trò:</span>
          <span className="info-value">{currentUser.roles?.join(', ') || 'User'}</span>
        </div>
      </div>
      <button onClick={handleUpdateProfile} className="profile-button edit-button">
        Chỉnh Sửa Thông Tin
      </button>
      {/* <button onClick={handleChangePassword} className="profile-button change-password-button">
        Đổi Mật Khẩu
      </button> */}
    </div>
  );
};

export default ProfilePage;