import axios from "axios";
import LocalStorageService from "../utils/LocalStorageService";

// let instance: AxiosInstance  = Axios.create({
//     baseURL: "http://studiosib.ru:8081/"
// });

const localStorageService = LocalStorageService.getService();

// Перехватчик запроса

axios.interceptors.request.use(
    config => {
        const token = localStorageService.getAccessToken();
        if (token) {
            config.headers['Authorization'] = 'Bearer ' + token;
        }
        config.baseURL = "http://studiosib.ru:8081/"
        // config.headers['Content-Type'] = 'application/json';
        return config;
    },
    error => {
        Promise.reject(error)
    });

//Перехватчик ответа
axios.interceptors.response.use((response) => {
    return response
}, function (error) {
    const originalRequest = error.config;

    if (error.response.status === 401 && originalRequest.url ===
        '/auth/refresh') {
        // router.push('/');
        return Promise.reject(error);
    }

    if (error.response.status === 401 && !originalRequest._retry) {
        originalRequest._retry = true;
        const refreshToken = localStorageService.getRefreshToken();
        const accessToken = localStorageService.getAccessToken();
        return axios.post('/auth/refresh',
            {
                "refreshToken": refreshToken,
                "accessToken": accessToken
            })
            .then(res => {
                if (res.status === 200) {
                    LocalStorageService.setToken(res.data);
                    axios.defaults.headers.common['Authorization'] = 'Bearer ' + localStorageService.getAccessToken();
                    return axios(originalRequest);
                }
            })
    }
    return Promise.reject(error);
});

export const sectionAPI = {
    list(idPage) {
        return axios.get(`page/` + idPage)
    },
    search(data) {
        return axios.get(`page/search?filter=${data}`)
    }
};

export const albumAPI = {
    get(id) {
        return axios.get(`album/` + id)
    }
};

export const playlistAPI = {
    get(id) {
        return axios.get(`playlist/` + id)
    }
};

export const searchAPI = {
    get(data) {
        return axios.get(`page/search?filter=${data}`)
    }
};

export const accountAPI = {
    post(data) {
        return axios.get(`Account/`, data)
    }
};

export const userAPI = {
    get(data) {
        return axios.get(`User/`, data)
    },
    create(data) {
        return axios.put(`User/`, data)
    },
    update(data) {
        return axios.post(`User/`, data)
    }
};