import {PageAction, PageActionTypes, SearchState} from "../../types/search";

const initialState: SearchState = {
    data: []
}

export const SearchReducer = (state = initialState, action: PageAction): SearchState => {
    switch (action.type) {
        case PageActionTypes.SET_DATA_SEARCH:
            return {...state, data: action.payload}
        default: return state
    }
}
