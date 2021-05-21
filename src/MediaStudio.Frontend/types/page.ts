import {ITrack} from "./track";

export interface ICells {
    idObject: number,
    orderSection: number,
    idTypeCell: number,
    name: string,
    colorCode: null | string,
    linkCover: null | string,
    countTrack: number
}

interface ISection {
    nameSection: string,
    cells: ICells[]
}

export interface IPage {
    section: ISection[]
}

export interface PageState {
    page: [] | IPage;
}

export enum PageActionTypes {
    SET_PAGE = 'SET_PAGE',
    GET_PAGE = 'GET_PAGE'
}

interface GetPageAction {
    type: PageActionTypes.GET_PAGE,
    payload: number;
}
interface SetPageAction {
    type: PageActionTypes.SET_PAGE,
    payload: IPage;
}

export type PageAction =
    GetPageAction
    | SetPageAction
