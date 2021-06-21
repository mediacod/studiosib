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
}

export enum PlayerActionTypes {
    PLAY = 'PLAY',
    PAUSE = 'PAUSE',
    SET_ACTIVE = 'SET_ACTIVE',
    SET_DURATION = 'SET_DURATION',
    SET_CURRENT_TIME = 'SET_CURRENT_TIME',
    SET_VOLUME = 'SET_VOLUME',
    SET_QUEUE = 'SET_QUEUE',
    SET_ID_ALBUM = 'SET_ID_ALBUM'
}

export interface IQueue {
    queue: ITrack[],
    idAlbum: number,
    idType: number
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

interface SetIdAlbum {
    type: PlayerActionTypes.SET_ID_ALBUM,
    payload: ITrack[];
}

export type PlayerAction =
    PlayAction
    | PauseAction
    | SetActiveAction
    | SetDurationAction
    | SetCurrentTimeAction
    | SetVolumeAction
    | SetQueueAction
    | SetIdAlbum
