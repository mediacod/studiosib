import { getPage } from './../action-creators/page';
import {put, takeEvery} from 'redux-saga/effects'
import {albumAPI} from "../../API/api";
import { AlbumPageActionTypes, GetAlbumPageAction } from '../../types/albumPage';
import {setPage} from "../action-creators/page";
import { setAlbumPage } from '../action-creators/albumPage';


export function* AlbumPageSagaWatcher() {
    yield takeEvery(AlbumPageActionTypes.GET_ALBUM_PAGE, sagaGetAlbum)
    // yield takeEvery(PageActionTypes., sagaSearch)
}

function* sagaGetAlbum ({payload}:GetAlbumPageAction) {
    let response = yield albumAPI.get(payload)
    yield put(setAlbumPage(response.data))
}
