import {PageAction, PageActionTypes, SearchState} from "../../types/search";

const initialState: SearchState = {
    data: {
        albums: [],
        perfomers: [],
        playlists: [],
        tracks: []
    }
}

export const SearchReducer = (state = initialState, action: PageAction): any => {
    switch (action.type) {
        case PageActionTypes.SET_DATA_SEARCH:
            return {...state, data: action.payload}
        default: return state
    }
}
