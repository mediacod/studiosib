import { all, fork } from 'redux-saga/effects'
import { AlbumPageSagaWatcher } from './albumPageSaga';
import {pageSagaWatcher} from "./pageSaga";

export default function* rootSaga() {
    yield all([
        fork(pageSagaWatcher),
        fork(AlbumPageSagaWatcher)
    ])
}
