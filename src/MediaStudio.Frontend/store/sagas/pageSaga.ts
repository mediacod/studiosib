import {put, takeEvery} from 'redux-saga/effects'
import {sectionAPI} from "../../API/api";
import {PageActionTypes} from "../../types/page";
import {setPage} from "../action-creators/page";


export function* pageSagaWatcher() {
    // yield takeEvery(PageActionTypes.GET_PAGE, sagaGetList)
    // yield takeEvery(PageActionTypes., sagaSearch)
}

function* sagaGetList({payload}) {
    let response = yield sectionAPI.list(payload)
    console.log(response)
    yield put(setPage(response.data))
}

// function* sagaSearch({payload}) {
//     let response = yield sectionAPI.search(payload)
//     yield put(setDataSearch(response.data))
// }
