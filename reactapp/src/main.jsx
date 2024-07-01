import React from 'react';
import ReactDOM from 'react-dom/client';
import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';
import './index.css';
import StackedLayout from './ui/components/stackedlayout/StackedLayout.jsx';
import Login from './pages/login/Login.jsx';
import Home from './pages/home/Home.jsx';
import Hardware from './pages/hardware/Hardware.jsx';
import Periferico from './pages/periferico/Periferico.jsx';
import Computadores from './pages/computadores/Computadores.jsx';
import Games from './pages/games/Games.jsx';
import Collections from './ui/components/collections/Collections.jsx';
import PageNotFound from './pages/pagenotfound/PageNotFound.jsx';
import Pedidos from './pages/pedidos/Pedidos';
import Produtos from './pages/produtos/Produtos.jsx';
import OfertasDoDia from './pages/ofertasdodia/OfertasDoDia';
import Contact from './ui/components/contact/Contact';
import FormConfig from './ui/components/formconfig/FormConfig';
import ProductOverview from './ui/components/productoverview/ProductOverview.jsx';

function PrivateRoute({ element: Element, ...rest }) {
    return localStorage.getItem('tokenJWT') ? (
        <Element {...rest} />
    ) : (
        <Navigate to="/" />
    );
}

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
            <Route path="/home" element={<PrivateRoute element={Home} />} />
            <Route path="/categorias" element={<PrivateRoute element={Collections} />} />
            <Route path="/hardware" element={<PrivateRoute element={Hardware} />} />
            <Route path="/periferico" element={<PrivateRoute element={Periferico} />} />
            <Route path="/computadores" element={<PrivateRoute element={Computadores} />} />
            <Route path="/games" element={<PrivateRoute element={Games} />} />
            <Route path="/produtos" element={<PrivateRoute element={Produtos} />} />
            <Route path="/pedidos" element={<PrivateRoute element={Pedidos} />} />
            <Route path="/ofertasdodia" element={<PrivateRoute element={OfertasDoDia} />} />
            <Route path="/contato" element={<PrivateRoute element={Contact} />} />
            <Route path="/configuracoes" element={<PrivateRoute element={FormConfig} />} />
            <Route path="/productoverview" element={<PrivateRoute element={ProductOverview} />} />
            <Route path="/404" element={<PageNotFound />} />
            <Route path="*" element={<Navigate to="/404" />} />
        </Routes>
    );
}

ReactDOM.createRoot(document.getElementById('root')).render(
    <React.StrictMode>
        <Main />
    </React.StrictMode>
);