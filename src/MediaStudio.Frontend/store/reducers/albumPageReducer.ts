import { IAlbumPage } from './../../types/albumPage';
import { AlbumPageAction, AlbumPageActionTypes, AlbumPageState } from "../../types/albumPage"

const initialState: AlbumPageState = {
    albumPage: {
        "idAlbum": null,
        "idTypeAudio": null,
        "name": '',
        "duration": null,
        "linkCover": '',
        "highQualityExist": false,
        "isChecked": false,
        "tracks": []
    }
}

export const AlbumPageReducer = (state = initialState, action: AlbumPageAction): AlbumPageState => {
    switch (action.type) {
        case AlbumPageActionTypes.SET_ALBUM_PAGE:
            return {...state, albumPage: action.payload}
        default: return state
    }
}
