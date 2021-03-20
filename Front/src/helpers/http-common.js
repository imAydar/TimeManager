import axios from 'axios';

export const http = axios.create({
    baseURL: 'https://timemanager.azurewebsites.net/',
    headers: {
        'Content-Type': 'application/json'
    }
})