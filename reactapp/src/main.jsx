import React from 'react';
import ReactDOM from 'react-dom';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
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
import OfertasDoDia from './pages/ofertasdodia/OfertasDoDia';
import Contact from './ui/components/contact/Contact';
import FormConfig from './ui/components/formconfig/FormConfig';

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
            <Route path="/hardware" element={<Hardware />} />
            <Route path="/periferico" element={<Periferico />} />
            <Route path="/computadores" element={<Computadores />} />
            <Route path="/games" element={<Games />} />
            <Route path="/pedidos" element={<Pedidos />} />
            <Route path="/ofertasdodia" element={<OfertasDoDia />} />
            <Route path="/contato" element={<Contact />} />
            <Route path="/configuracoes" element={<FormConfig />} />
            <Route path="*" element={<PageNotFound />} />
        </Routes>
    );
}

ReactDOM.createRoot(document.getElementById('root')).render(
    <React.StrictMode>
        <Main />
    </React.StrictMode>
);
