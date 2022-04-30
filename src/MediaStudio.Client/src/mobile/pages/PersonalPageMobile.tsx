import React, {useContext} from 'react';
import {usePageName} from "../../hooks/pageName.hook";
import {AuthFormSwitch} from "../components/auth/AuthFormSwitch";
import {useUserHistoryQuery} from "../../query/useHistoryQuery";
import {TrackList} from "../components/blocks/TrackList";
import styled from "styled-components";
import AudioContext from "../../context/audioContext";
import {useUserFavoritesTrackQuery} from "../../query/useFavoritesTrackQuery";
import {ACCOUNT_PAGES, PAGES} from "../../utils/const";
import {CapsuleSelector} from "../components/common/layout/CapsuleSelector";
import {useLocation, useNavigate} from "react-router-dom";
import {useSectionQuery} from "../../query/useSectionQuery";
import {MY_FAVORITE_PLAYLIST, MY_FAVORITE_TRACK, MY_HISTORY_TRACK} from "../../router/const";
import {FavouritesTracksPageMobile} from "./FavouritesTracksPageMobile";
import {HistoryTracksPageMobile} from "./HistoryTracksPageMobile";
import {FavouritesPlaylistPageMobile} from "./FavouritesPlaylistPageMobile";
import AuthContext from "../../context/authContext";

export const PersonalPageMobile = () => {

    usePageName('Моя музыка')
    const {pathname} = useLocation()
    const navigate = useNavigate()
    const {isAuth, user, logout}:any = useContext(AuthContext)
    const {setAudio}: any = useContext(AudioContext)
    const {data: tracks} = useUserHistoryQuery()

    const getIdPage = () => ACCOUNT_PAGES?.find((p) => p.path === pathname)?.id

    const selectHandler = (value: string) => {
        navigate(value)
    }

    // console.log(pathname)

    if(!isAuth || !user?.idUser){
        return (<AuthFormSwitch />)
    }

    const play = ({idTrack}: any) => {
        setAudio({
            data: {idAlbum: 'history', tracks},
            idTrack: idTrack,
        })
    }

    return (
        <>
            <CapsuleSelector activeKey={pathname} selectHandler={selectHandler} data={ACCOUNT_PAGES}/>
            {pathname === MY_FAVORITE_TRACK && <FavouritesTracksPageMobile />}
            {pathname === MY_HISTORY_TRACK && <HistoryTracksPageMobile />}
            {pathname === MY_FAVORITE_PLAYLIST && <FavouritesPlaylistPageMobile />}
        </>
    );
};


const StyledTitle = styled.div`
    font-size: 22px;
    font-weight: 700;
    line-height: 2.5;
    margin-left: 10px;
`

const StyledContainerTracks = styled.div`
    margin: 15px 0;

    &:first-child{
      margin-top: 0;
    }
`