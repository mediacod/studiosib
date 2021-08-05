import {ITrack} from "./track";

export interface PlayerState {
    active: null | ITrack;
    volume: number;
    duration: number;
    currentTime: number;
    pause: boolean;
    idAlbum: number;
    idType?: number;
    queue: ITrack[];
    repeat: boolean;
    repeatOne: boolean;
    isShuffle: boolean;
    isNext: boolean;
    isPrev: boolean;
    linkCover: string;
    index: number;
    newTime: number;
}

export enum PlayerActionTypes {
    PLAY = 'PLAY',
    PAUSE = 'PAUSE',
    SET_ACTIVE = 'SET_ACTIVE',
    SET_DURATION = 'SET_DURATION',
    SET_CURRENT_TIME = 'SET_CURRENT_TIME',
    SET_VOLUME = 'SET_VOLUME',
    SET_QUEUE = 'SET_QUEUE',
    SET_ID_ALBUM = 'SET_ID_ALBUM',
    SET_NEXT_TRUE = 'SET_NEXT_TRUE',
    SET_NEXT_FALSE = 'SET_NEXT_FALSE',
    SET_PREV_TRUE = 'SET_PREV_TRUE',
    SET_PREV_FALSE = 'SET_PREV_FALSE',
    SET_SHUFFLE_TRUE = 'SET_SHUFFLE_TRUE',
    SET_SHUFFLE_FALSE = 'SET_SHUFFLE_FALSE',
    SET_INDEX_QUEUE = 'SET_INDEX_QUEUE',
    SET_NEW_TIME = 'SET_NEW_TIME'
}

export interface IQueue {
    queue: ITrack[],
    idAlbum: number,
    idType: number,
    linkCover?: string
}

interface PlayAction {
    type: PlayerActionTypes.PLAY
}
interface PauseAction {
    type: PlayerActionTypes.PAUSE
}
interface SetActiveAction {
    type: PlayerActionTypes.SET_ACTIVE,
    payload: ITrack;
}
interface SetDurationAction {
    type: PlayerActionTypes.SET_DURATION,
    payload: number;
}
interface SetCurrentTimeAction {
    type: PlayerActionTypes.SET_CURRENT_TIME,
    payload: number;
}
interface SetVolumeAction {
    type: PlayerActionTypes.SET_VOLUME,
    payload: number;
}

interface SetQueueAction {
    type: PlayerActionTypes.SET_QUEUE,
    payload: IQueue;
}

interface SetIdAlbumAction {
    type: PlayerActionTypes.SET_ID_ALBUM,
    payload: ITrack[];
}

interface SetNextTrueAction {
    type: PlayerActionTypes.SET_NEXT_TRUE
}

interface SetNextFalseAction {
    type: PlayerActionTypes.SET_NEXT_FALSE
}

interface SetPrevTrueAction {
    type: PlayerActionTypes.SET_PREV_TRUE
}

interface SetPrevFalseAction {
    type: PlayerActionTypes.SET_PREV_FALSE
}

interface SetShuffleTrueAction {
    type: PlayerActionTypes.SET_SHUFFLE_TRUE
}

interface SetShuffleFalseAction {
    type: PlayerActionTypes.SET_SHUFFLE_FALSE
}

interface SetIndexAudioAction {
    payload: number;
    type: PlayerActionTypes.SET_INDEX_QUEUE
}

interface SetNewTimeAction {
    payload: number;
    type: PlayerActionTypes.SET_NEW_TIME
}

export type PlayerAction =
    PlayAction
    | PauseAction
    | SetActiveAction
    | SetDurationAction
    | SetCurrentTimeAction
    | SetVolumeAction
    | SetQueueAction
    | SetIdAlbumAction
    | SetNextTrueAction
    | SetNextFalseAction
    | SetPrevTrueAction
    | SetPrevFalseAction
    | SetShuffleTrueAction
    | SetShuffleFalseAction
    | SetIndexAudioAction
    | SetNewTimeAction