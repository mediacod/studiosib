import {IAlbumItem} from "./album";
import {ITrack} from "./track";
import {ICells} from "./page";


interface IDataSearch {
    albums: ICells[]
    perfomers: []
    playlists: ICells[]
    tracks: ITrack[]
}

export interface SearchState {
    data: IDataSearch;
}

export enum PageActionTypes {
    GET_DATA_SEARCH = 'GET_DATA_SEARCH',
    SET_DATA_SEARCH = 'SET_DATA_SEARCH'
}

export interface GetDataSearchAction {
    type: PageActionTypes.GET_DATA_SEARCH,
    payload: number;
}

interface SetDataSearchAction {
    type: PageActionTypes.SET_DATA_SEARCH,
    payload: [];
}

export type PageAction =
    GetDataSearchAction
    | SetDataSearchAction
