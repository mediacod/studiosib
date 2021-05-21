import {IPage, PageAction, PageActionTypes} from "../../types/page";

export const getPage = (payload: number): PageAction => {
    return {type: PageActionTypes.GET_PAGE, payload}
}
export const setPage = (payload: IPage): PageAction => {
    return {type: PageActionTypes.SET_PAGE, payload}
}
