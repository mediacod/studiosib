import {put, takeEvery} from 'redux-saga/effects'
import {searchAPI, sectionAPI} from "../../API/api";
import {GetDataSearchAction, PageActionTypes} from "../../types/search";
import {setDataSearch} from "../action-creators/search";
import {convertDataSearch} from "../../utils/convertData";


export function* searchSagaWatcher() {
    yield takeEvery(PageActionTypes.GET_DATA_SEARCH, sagaSearch)
}

function* sagaSearch ({payload}:GetDataSearchAction) {
    let response = yield searchAPI.get(payload)
    yield put(setDataSearch(convertDataSearch(response.data)))
}
