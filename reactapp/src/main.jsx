import React from 'react';
import ReactDOM from 'react-dom';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import './index.css';
import StackedLayout from './ui/components/stackedlayout/StackedLayout.jsx';
import Login from './pages/login/Login.jsx';
import Home from './pages/home/Home.jsx';
import Collections from './ui/components/collections/Collections.jsx';
import PageNotFound from './pages/pagenotfound/PageNotFound.jsx';

function Main() {
    return (
        <Router>
            <Routes>
                <Route path="/" element={<Login />} />
                <Route path="*" element={<MainLayout />} />
            </Routes>
        </Router>
    );
}

function MainLayout() {
    return (
        <>
            <StackedLayout />
            <AppRoutes />
        </>
    );
}

function AppRoutes() {
    return (
        <Routes>
            <Route path="/home" element={<Home />} />
            <Route path="/categorias" element={<Collections />} />
            <Route path="*" element={<PageNotFound />} />
        </Routes>
    );
}

ReactDOM.createRoot(document.getElementById('root')).render(
    <React.StrictMode>
        <Main />
    </React.StrictMode>
);
