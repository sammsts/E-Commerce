import axios from 'axios';

const api = axios.create({
    baseURL: 'https://webapiecommerceengsoft2.azurewebsites.net/api',
    headers: {
        Authorization: `Bearer ${localStorage.getItem('tokenJWT')}`,
    },
});

export default api;
