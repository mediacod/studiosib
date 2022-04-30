import Axios, {AxiosInstance, AxiosResponse} from "axios";
import {ReactText} from "react";
import {LocalStorageService} from "../utils/LocalStorageService";

const localStorageService = LocalStorageService.getService();
const baseURL = "http://studiosib.ru:8081/"

let host: AxiosInstance  = Axios.create({
    baseURL
});

let authHost: AxiosInstance  = Axios.create({
    baseURL
});

const authInterceptor = (config: any) => {
    const token = localStorageService.getAccessToken();
    if (token) {
        config.headers['Authorization'] = 'Bearer ' + token;
    }
    return config
}

authHost.interceptors.request.use(authInterceptor, error => {
    Promise.reject(error)
})

//Перехватчик ответа
authHost.interceptors.response.use((response: AxiosResponse) => {
    return response
}, function (error: any) {
    const originalRequest = error.config;

    if (error.response.status === 401 && originalRequest.url ===
        '/auth/refresh') {
        // router.push('/');
        return Promise.reject(error);
    }

    if (error.response.status === 404 && !originalRequest._retry) {
        originalRequest._retry = true;

    }
    return Promise.reject(error);
});

export const refreshAPI = async (originalRequest?: any) => {
    const refreshToken = localStorageService.getRefreshToken();
    const accessToken = localStorageService.getAccessToken();
    return authHost.post('/auth/refresh',
        {
            "refreshToken": refreshToken,
            "accessToken": accessToken
        })
        .then(res => {
            if (res.status === 200) {
                LocalStorageService.setToken(res.data);
                authHost.defaults.headers.common['Authorization'] = 'Bearer ' + localStorageService.getAccessToken();
                return originalRequest ? authHost(originalRequest) : res.data;
            }
        })
}

export const getSectionAPI = async (idPage: ReactText) => {
    const {data} = await host.get('page/' + idPage, {})
    return data
}

export const createAccount = async (data: any) => {
    const response = await host.post('Account/', data)
    return response
}

export const Login = async (data: any) => {
    const response = await host.post('Auth/SignIn/', data)
    return response.data
}

export const AlbumAPI = async (id: any, type: string) => {
    const response = await host.get(`${type}/` + id)
    return response.data
}


export const ColorAPI = async () => {
    const response = await host.get(`color`)
    return response.data
}

export const sectionAPI = {
    list(idPage: ReactText) {
        return host.get(`page/` + idPage)
    },
    search(data: Object) {
        return host.get(`page/search?filter=${data}`)
    }
};

export const USER_API = {
    GET: () => {
        return authHost.get(`/User`)
    },
    CREATE: (data: any) => {
        return authHost.post(`/User`, data)
    },
    UPDATE: (data: any) => {
        return authHost.put(`/User`, data)
    }
};

export const USER_HISTORY = {
    GET: () => {
        return authHost.get(`/UserHistoryTrack`)
    },
    ADD: (data: any) => {
        return authHost.post(`/UserHistoryTrack?idTrack=${data}`)
    }
};

export const USER_FAVOURITES_TRACK = {
    GET: () => {
        return authHost.get(`/UserFavouritesTrack`)
    },
    ADD: (id: any)=> authHost.post(`/UserFavouritesTrack?idTrack=${id}`),
    REMOVE: (data: any) => {
        return authHost.delete(`/UserFavouritesTrack?idTrack=${data}`)
    }
};

export const USER_FAVOURITES_PLAYLIST = {
    GET: () => {
        return authHost.get(`/UserFavouritesPlaylist`)
    },
    ADD: (data: any) => {
        return authHost.post(`/UserFavouritesPlaylist?idPlaylist=${data}`)
    }
};

export const playlistAPI = {
    get(id: ReactText) {
        return host.get(`playlist/` + id)
    }
};

export const searchAPI = async (query: Object) => {
    const {data} = await host.get(`page/search?filter=${query}`)
    return data
};

export const USER_PLAYLIST_API = {
    GET: () => {
        return authHost.get(`playlist/`)
    },
    GET_ONE: (id: any) => {
        return authHost.get(`playlist/${id}`)
    },
    CREATE: (data: any) => {
        return authHost.post(`playlist/`, data)
    },
    UPDATE: () => {
        return authHost.put(`playlist/`)
    },
    REMOVE: () => {

    },
    ADD_TRACK: (data: any) => {
        return authHost.put(`playlist/AddTrack`, data)
    },
    REMOVE_TRACK: ({idTrackToPlaylist}:  any) => {
        return authHost.delete(`playlist/DeleteTrack?idTrackToPlaylist=${idTrackToPlaylist}`)
    }
};