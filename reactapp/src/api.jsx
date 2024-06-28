import axios from 'axios';

const api = axios.create({
    baseURL: 'https://localhost:7063/api',
    headers: {
        Authorization: `Bearer ${localStorage.getItem('tokenJWT')}`,
    },
});

export default api;
