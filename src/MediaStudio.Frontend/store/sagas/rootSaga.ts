import { all, fork } from 'redux-saga/effects'
import {pageSagaWatcher} from "./pageSaga";

export default function* rootSaga() {
    yield all([
        fork(pageSagaWatcher)
    ])
}
