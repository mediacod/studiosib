import {PlayerAction, PlayerActionTypes, PlayerState} from "../../types/player";

const initialState: PlayerState = {
    queue: [],
    idAlbum: null,
    idType: null,
    pause: true,
    duration: 0,
    active: null,
    volume: 100,
    currentTime: 0,
    repeat: false,
    repeatOne: false,
    isShuffle: false,
    isNext: false,
    isPrev: false,
    linkCover: ''
}

export const playerReducer = (state = initialState, action: PlayerAction): PlayerState => {
    switch (action.type) {
        case PlayerActionTypes.PLAY:
            return {...state, pause: false}
        case PlayerActionTypes.PAUSE:
            return {...state, pause: true}
        case PlayerActionTypes.SET_DURATION:
            return {...state, duration: action.payload}
        case PlayerActionTypes.SET_CURRENT_TIME:
            return {...state, currentTime: action.payload}
        case PlayerActionTypes.SET_VOLUME:
            return {...state, volume: action.payload}
        case PlayerActionTypes.SET_ACTIVE:
            return {...state, active: action.payload, currentTime: 0, duration: 0}
        case PlayerActionTypes.SET_QUEUE:
            return {...state, queue: action.payload.queue, idAlbum: action.payload.idAlbum, idType: action.payload.idType, currentTime: 0, duration: 0, linkCover: action.payload.linkCover}
        case PlayerActionTypes.SET_NEXT_TRUE:
            return {...state, isNext: true}
        case PlayerActionTypes.SET_NEXT_FALSE:
            return {...state, isNext: false}
        case PlayerActionTypes.SET_PREV_TRUE:
            return {...state, isPrev: true}
        case PlayerActionTypes.SET_PREV_FALSE:
            return {...state, isPrev: false}
        case PlayerActionTypes.SET_SHUFFLE_TRUE:
            return {...state, isShuffle: true}
        case PlayerActionTypes.SET_SHUFFLE_FALSE:
            return {...state, isShuffle: false}
        default: return state
    }
}
