import { all } from 'redux-saga/effects'
import {pageSagaWatcher} from "./pageSaga";

export default function* rootSaga() {
    yield all([
        pageSagaWatcher()
    ])
}
