import Axios, {AxiosInstance} from "axios";

let instance: AxiosInstance  = Axios.create({
    baseURL: "http://msapi.ru:8081/"
});

export const sectionAPI = {
    list(idPage) {
        return instance.get(`page/` + idPage)
    },
    search(data) {
        return instance.get(`page/search?filter=${data}`)
    }
};

export const albumAPI = {
    get(id) {
        return instance.get(`album/` + id)
    }
};

export const playlistAPI = {
    get(id) {
        return instance.get(`playlist/` + id)
    }
};
