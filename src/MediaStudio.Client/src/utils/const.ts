import {
    BOOK_PAGE,
    CONFERENCES_PAGE,
    MAIN_PAGE,
    MUSIC_PAGE, MY_FAVORITE_PLAYLIST,
    MY_FAVORITE_TRACK,
    MY_HISTORY_TRACK,
    PREACH_PAGE
} from "../router/const";

export const PAGES = [
    {id: 1, title: 'Главная', path: MAIN_PAGE},
    {id: 2, title: 'Музыка', path: MUSIC_PAGE},
    {id: 3, title: 'Проповеди', path: PREACH_PAGE},
    {id: 4, title: 'Книги', path: BOOK_PAGE},
    {id: 5, title: 'Конференции', path: CONFERENCES_PAGE}
]

export const ACCOUNT_PAGES = [
    {id: 1, title: 'Избранное', path: MY_FAVORITE_TRACK},
    {id: 2, title: 'История', path: MY_HISTORY_TRACK},
    // {id: 3, title: 'Плейлисты', path: MY_FAVORITE_PLAYLIST},
]

export enum TYPE_AUDIO {
    ALBUM = "album",
    PLAYLIST = "playlist"
}

export enum TYPE_AUDIO_CODE {
    'not_type',
    'album',
    'playlist',
    'my/playlist'
}

export enum GENDER {
    'not_type',
    'мужской',
    'женский'
}