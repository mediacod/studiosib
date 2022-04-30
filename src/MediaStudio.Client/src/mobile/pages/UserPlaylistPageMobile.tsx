import React, {useContext} from 'react';
import {TrackList} from "../components/blocks/TrackList";
import styled from "styled-components";
import {Image} from "antd-mobile";
import {useUserPlaylistOneQuery} from "../../query/useUserPlaylistOneQuery";
import {useParams} from "react-router-dom";
import AudioContext from "../../context/audioContext";
import {usePageName} from "../../hooks/pageName.hook";

export const UserPlaylistPageMobile = () => {

    const {id} = useParams()
    const {playlistData} = useUserPlaylistOneQuery(id)
    usePageName(playlistData?.name)
    const {setAudio}: any = useContext(AudioContext)

    const play = ({idTrack}: any) => {
        setAudio({
            data: playlistData,
            idTrack: idTrack
        })
    }

    return (
        <>
            {/*<StyledWrapperCover>*/}
            {/*    {playlistData?.linkCover*/}
            {/*        ? <StyledImage lazy src={playlistData?.linkCover} />*/}
            {/*        : <StyledPlaylistCover><h2>{playlistData?.name}</h2></StyledPlaylistCover>}*/}
            {/*</StyledWrapperCover>*/}

            <TrackList
                tracks={playlistData?.tracks}
                play={play}
            />
        </>
    );
};


const StyledWrapper = styled.div`
  display: flex;
  justify-content: center;
  align-items: center;
`

const StyledWrapperCover = styled.div`
  display: flex;
  justify-content: center;
  align-items: center;
  margin: 20px 0 0px;
`

const StyledImage = styled(Image)`
  width: 200px;
  min-height: 200px;
  border-radius: 5px;
`

const StyledPlaylistCover = styled.div`
  display: flex;
  justify-content: center;
  align-items: center;
  width: 200px;
  height: 200px;
  border-radius: 5px;
  color: #fff;
  background: -webkit-linear-gradient(45deg, rgb(147, 180, 174), rgb(93, 207, 167));
  background: -moz-linear-gradient(45deg, rgb(147, 180, 174), rgb(93, 207, 167));
  background: linear-gradient(45deg, rgb(147, 180, 174), rgb(93, 207, 167));
`

const StyleCard = styled.div`
  min-height: 40px;    
`