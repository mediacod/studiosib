import { ITrack } from './track';

export interface IAlbumPage {
    "idAlbum": number,
    "idTypeAudio": number,
    "name": string,
    "duration": number,
    "linkCover": string,
    "highQualityExist": boolean,
    "isChecked": boolean,
    "tracks": ITrack[]
}

export interface AlbumPageState {
    albumPage: IAlbumPage;
}

export enum AlbumPageActionTypes {
    SET_ALBUM_PAGE = 'SET_ALBUM_PAGE',
    GET_ALBUM_PAGE = 'GET_ALBUM_PAGE',
}

export interface GetAlbumPageAction {
    type: AlbumPageActionTypes.GET_ALBUM_PAGE,
    payload: number;
}

interface SetAlbumPageAction {
    type: AlbumPageActionTypes.SET_ALBUM_PAGE,
    payload: IAlbumPage;
}

export type AlbumPageAction =
    GetAlbumPageAction
    | SetAlbumPageAction
