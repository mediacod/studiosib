import { PageAction, PageActionTypes} from "../../types/search";

export const getDataSearch = (payload): PageAction => {
    return {type: PageActionTypes.GET_DATA_SEARCH, payload}
}
export const setDataSearch = (payload): PageAction => {
    return {type: PageActionTypes.SET_DATA_SEARCH, payload}
}