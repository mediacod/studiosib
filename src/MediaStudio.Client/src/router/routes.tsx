import {
    ALBUM_PAGE,
    BOOK_PAGE,
    CONFERENCES_PAGE,
    LOGIN_PAGE,
    MAIN_PAGE,
    MUSIC_PAGE,
    PERSONAL_PAGE,
    USER_PLAYLISTS_PAGE,
    PREACH_PAGE,
    SEARCH_PAGE,
    SIGN_UP_PAGE,
    PLAYLIST_PAGE,
    MY_FAVORITE_TRACK, MY_HISTORY_TRACK, MY_FAVORITE_PLAYLIST, MY_PLAYLIST_PAGE, SECTION
} from "./const";
import {MainPage} from '../pages'
import {MainPageMobile, PersonalPageMobile, UserPlaylistsListPageMobile, SearchPageMobile} from "../mobile/pages";
import {PlaylistPage} from "../pages/PlaylistPage";
import {PersonalPage} from "../pages/PersonalPage";
import {SearchPage} from "../pages/SearchPage";
import {LoginPageMobile} from "../mobile/pages/LoginPageMobile";
import {SignUpPageMobile} from "../mobile/pages/SignUpPageMobile";
import {AlbumPageMobile} from "../mobile/pages/AlbumPageMobile";
import {TYPE_AUDIO} from "../utils/const";
import {UserPlaylistPageMobile} from "../mobile/pages/UserPlaylistPageMobile";
import { SectionPageMobile } from "../mobile/pages/SectionPageMobile";

export const appRouter = [
    {
        path: MAIN_PAGE,
        element: <MainPage />
    },
    {
        path: MUSIC_PAGE,
        element: <MainPage />
    },
    {
        path: BOOK_PAGE,
        element: <MainPage />
    },
    {
        path: PREACH_PAGE,
        element: <MainPage />
    },
    {
        path: CONFERENCES_PAGE,
        element: <MainPage />
    },
    {
        path: USER_PLAYLISTS_PAGE,
        element: <PlaylistPage />
    },
    {
        path: PERSONAL_PAGE,
        element: <PersonalPage />
    },
    {
        path: SEARCH_PAGE,
        element: <SearchPage />
    },
]

export const appMobileRouter = [
    {
        path: MAIN_PAGE,
        element: <MainPageMobile />
    },
    {
        path: MUSIC_PAGE,
        element: <MainPageMobile />
    },
    {
        path: BOOK_PAGE,
        element: <MainPageMobile />
    },
    {
        path: PREACH_PAGE,
        element: <MainPageMobile />
    },
    {
        path: CONFERENCES_PAGE,
        element: <MainPageMobile />
    },
    {
        path: SECTION,
        element: <SectionPageMobile />
    },
    {
        path: USER_PLAYLISTS_PAGE,
        element: <UserPlaylistsListPageMobile />
    },
    {
        path: PERSONAL_PAGE,
        element: <PersonalPageMobile />
    },
    {
        path: SEARCH_PAGE,
        element: <SearchPageMobile />
    },
    {
        path: SIGN_UP_PAGE,
        element: <SignUpPageMobile />
    },
    {
        path: LOGIN_PAGE,
        element: <LoginPageMobile />
    },
    {
        path: ALBUM_PAGE,
        element: <AlbumPageMobile type={TYPE_AUDIO.ALBUM} />
    },
    {
        path: PLAYLIST_PAGE,
        element: <AlbumPageMobile type={TYPE_AUDIO.PLAYLIST} />
    },
    {
        path: MY_FAVORITE_TRACK,
        element: <PersonalPageMobile />
    },
    {
        path: MY_HISTORY_TRACK,
        element: <PersonalPageMobile />
    },
    {
        path: MY_FAVORITE_PLAYLIST,
        element: <PersonalPageMobile />
    },
    {
        path: MY_PLAYLIST_PAGE,
        element: <UserPlaylistPageMobile />
    },
]