import React, {useContext} from 'react';
import {useUserFavoritesTrackQuery} from "../../query/useFavoritesTrackQuery";
import {TrackList} from "../components/blocks/TrackList";
import styled from "styled-components";
import AudioContext from "../../context/audioContext";
import {useUserHistoryQuery} from "../../query/useHistoryQuery";
import { Grid } from 'antd-mobile';

export const FavouritesPlaylistPageMobile = () => {

    const {data: favoritesTracks} = useUserHistoryQuery()
    const {setAudio}: any = useContext(AudioContext)

    const play = ({idTrack}: any) => {
        setAudio({
            data: {idAlbum: 'history', favoritesTracks},
            idTrack: idTrack,
        })
    }

    return (
        <>
            {favoritesTracks?.length
                ?   <StyledContainerTracks>
                        <StyledTitle>Сохраненые плейлисты</StyledTitle>
                    </StyledContainerTracks>
                : null
            }
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