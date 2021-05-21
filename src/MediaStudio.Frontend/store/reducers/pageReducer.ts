import {PageAction, PageActionTypes, PageState} from "../../types/page";

const initialState: PageState = {
    page: []
}

export const pageReducer = (state = initialState, action: PageAction): PageState => {
    switch (action.type) {
        case PageActionTypes.SET_PAGE:
            return {...state, page: action.payload}
        default: return state
    }
}
