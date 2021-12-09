import {put, takeEvery} from 'redux-saga/effects'
import {albumAPI, playlistAPI} from "../../API/api";
import { AlbumPageActionTypes, GetAlbumPageAction } from '../../types/albumPage';
import { setAlbumPage } from '../action-creators/albumPage';
import cover from '../../public/static/images/coverDefault.png';


export function* AlbumPageSagaWatcher() {
    yield takeEvery(AlbumPageActionTypes.GET_ALBUM_PAGE, sagaGetAlbum)
    yield takeEvery(AlbumPageActionTypes.GET_PLAYLIST_PAGE, sagaGetPlaylist)
    // yield takeEvery(PageActionTypes., sagaSearch)
}

function* sagaGetAlbum ({payload}:GetAlbumPageAction) {
    let response = yield albumAPI.get(payload)
    yield put(setAlbumPage(response.data))
}

function* sagaGetPlaylist ({payload}:GetAlbumPageAction) {
    let response = yield playlistAPI.get(payload)
    response = yield response.data

    const dataAlbum = yield  {
        "idAlbum": response.idPlaylist,
        "idTypeAudio": 2,
        "name": response.name,
        "duration": response.duration,
        "linkCover": response.linkCover || cover,
        "highQualityExist": false,
        "isChecked": false,
        "tracks": response.tracks,
        "colourCode": response.colourCode,
        "isPublic": response.isPublic
    }

    yield put(setAlbumPage(dataAlbum))
}