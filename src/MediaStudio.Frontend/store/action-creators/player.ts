import {IQueue, PlayerAction, PlayerActionTypes} from "../../types/player";
import {ITrack} from "../../types/track";

export const playTrack = (): PlayerAction => {
    return {type: PlayerActionTypes.PLAY}
}
export const pauseTrack = (): PlayerAction => {
    return {type: PlayerActionTypes.PAUSE}
}
export const setDuration = (payload: number): PlayerAction => {
    return {type: PlayerActionTypes.SET_DURATION, payload}
}
export const setCurrentTime = (payload: number): PlayerAction => {
    return {type: PlayerActionTypes.SET_CURRENT_TIME, payload}
}
export const setVolume = (payload: number): PlayerAction => {
    return {type: PlayerActionTypes.SET_VOLUME, payload}
}
export const setActive = (payload: ITrack): PlayerAction => {
    return {type: PlayerActionTypes.SET_ACTIVE, payload}
}
export const setQueue = (payload: IQueue): PlayerAction => {
    return {type: PlayerActionTypes.SET_QUEUE, payload}
}
export const setIsNext = (): PlayerAction => {
    return {type: PlayerActionTypes.SET_NEXT_TRUE}
}
export const setIsNextFalse = (): PlayerAction => {
    return {type: PlayerActionTypes.SET_NEXT_FALSE}
}
export const setIsPrev = (): PlayerAction => {
    return {type: PlayerActionTypes.SET_PREV_TRUE}
}
export const setIsPrevFalse = (): PlayerAction => {
    return {type: PlayerActionTypes.SET_PREV_FALSE}
}
export const setShuffleTrue = (): PlayerAction => {
    return {type: PlayerActionTypes.SET_SHUFFLE_TRUE}
}
export const setShuffleFalse = (): PlayerAction => {
    return {type: PlayerActionTypes.SET_SHUFFLE_FALSE}
}
export const setIndexQueue = (payload: number): PlayerAction => {
    return {type: PlayerActionTypes.SET_INDEX_QUEUE, payload}
}
export const setNewTime = (payload: number): PlayerAction => {
    return {type: PlayerActionTypes.SET_NEW_TIME, payload}
}
