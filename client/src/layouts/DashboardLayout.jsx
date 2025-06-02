import React from 'react';
import { Outlet, Link, useNavigate } from 'react-router-dom';
import { useAuth } from '../contexts/AuthContext';
import './DashboardLayout.css'; // Tạo file CSS này

const Navbar = () => {
    const { currentUser, logout } = useAuth();
    const navigate = useNavigate();

    const handleLogout = () => {
        logout();
        navigate('/login');
    };

    return (
        <nav className="navbar">
            <Link to="/" className="navbar-brand">Expense Manager</Link>
            {currentUser && (
                <div className="navbar-user">
                    <span>Chào, {currentUser.fullName || currentUser.username}!</span>
                    <button onClick={handleLogout} className="logout-button">Đăng xuất</button>
                </div>
            )}
        </nav>
    );
};

const Sidebar = () => {
    return (
        <aside className="sidebar">
            <nav>
                <ul>
                    <li><Link to="/">Tổng quan</Link></li>
                    <li><Link to="/expenses">Chi tiêu</Link></li>
                    <li><Link to="/categories">Danh mục</Link></li>
                    <li><Link to="/budgets">Ngân sách</Link></li>
                    <li><Link to="/reports">Báo cáo</Link></li>
                    <li><Link to="/profile">Tài khoản</Link></li>
                </ul>
            </nav>
        </aside>
    );
};


const DashboardLayout = () => {
  return (
    <div className="dashboard-layout">
      <Navbar />
      <div className="dashboard-main-content">
        <Sidebar />
        <main className="dashboard-page-content">
          <Outlet /> 
        </main>
      </div>
    </div>
  );
};



export default DashboardLayout;

