import React from 'react';
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import Login from './pages/login/Login.jsx';
import Home from './pages/home/Home.jsx';

const RoutesApp = () => {
    return (
        <BrowserRouter>
            <Routes>
                <Route path="/" element={<Login />} index />
                <Route path="/home" element={<Home />} />
            </Routes>
        </BrowserRouter>
    );
};

export default RoutesApp;