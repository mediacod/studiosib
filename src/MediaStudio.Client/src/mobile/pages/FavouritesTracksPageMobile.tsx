import React, {useContext} from 'react';
import {useUserFavoritesTrackQuery} from "../../query/useFavoritesTrackQuery";
import {TrackList} from "../components/blocks/TrackList";
import styled from "styled-components";
import AudioContext from "../../context/audioContext";

export const FavouritesTracksPageMobile = () => {

    const {data: favoritesTracks} = useUserFavoritesTrackQuery()
    const {setAudio}: any = useContext(AudioContext)

    const reverseFavoritesTracks = favoritesTracks ? [...favoritesTracks]?.reverse() : []

    const play = ({idTrack}: any) => {
        setAudio({
            data: {idAlbum: 'favoritesTracks', tracks: reverseFavoritesTracks},
            idTrack: idTrack,
        })
    }

    return (
        <>
            {favoritesTracks?.length
                ?   <StyledContainerTracks>
                        <StyledTitle>Избранные треки</StyledTitle>
                        <TrackList tracks={reverseFavoritesTracks} play={play}/>
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