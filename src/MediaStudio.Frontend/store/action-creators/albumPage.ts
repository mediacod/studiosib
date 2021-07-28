import { AlbumPageAction, AlbumPageActionTypes, IAlbumPage } from "../../types/albumPage";

export const getAlbumPage = (payload: number): AlbumPageAction => {
    return {type: AlbumPageActionTypes.GET_ALBUM_PAGE, payload}
}

export const setAlbumPage = (payload: IAlbumPage): AlbumPageAction => {
    return {type: AlbumPageActionTypes.SET_ALBUM_PAGE, payload}
}

export const getPlaylistPage = (payload: number): AlbumPageAction => {
    return {type: AlbumPageActionTypes.GET_PLAYLIST_PAGE, payload}
}