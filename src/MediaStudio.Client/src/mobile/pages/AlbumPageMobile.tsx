import React, {useContext} from 'react';
import {useParams} from 'react-router-dom';
import {Image, Skeleton} from "antd-mobile";
import {useAlbumQuery} from "../../query/useAlbumQuery";
import styled from "styled-components";
import {TrackList} from "../components/blocks/TrackList";
import AudioContext from "../../context/audioContext";
import {usePageName} from "../../hooks/pageName.hook";
import {TYPE_AUDIO} from "../../utils/const";
import {USER_FAVOURITES_PLAYLIST} from "../../api/api";

export const AlbumPageMobile = React.memo(({type}: any) => {

    const {id} = useParams()
    const {data} = useAlbumQuery(id, type)

    usePageName(data?.name)

    const {setAudio}: any = useContext(AudioContext)

    const play = ({idTrack}: any) => {
        setAudio({
            data,
            idTrack: idTrack,
        })
    }

    const addFavorite = () => {
        if(type = TYPE_AUDIO.PLAYLIST){
            USER_FAVOURITES_PLAYLIST.ADD(id)
        }
    }

    return (
        <>
            <StyledWrapperCover>
                {data?.linkCover
                    ? <StyledImage lazy src={data?.linkCover} />
                    : data?.name ? <StyledPlaylistCover color={data?.colourCode}>{data?.name}</StyledPlaylistCover> : <StyledCoverSkeleton animated/>}
            </StyledWrapperCover>
            <StyledWrapper>

                <StyleCard>
                    {/*<span onClick={addFavorite}><Heart key={'HeartFill'}  size={24}/></span>*/}
                </StyleCard>

            </StyledWrapper>
            <TrackList tracks={data?.tracks} play={play}/>
        </>
    );
});

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
  background: #999999;
`

const StyledPlaylistCover = styled.div<{color?: string}>`
  display: flex;
  justify-content: center;
  align-items: center;
  width: 200px;
  height: 200px;
  border-radius: 5px;
  font-size: 22px;
  font-weight: bold;
  color: #fff;
  background: -webkit-linear-gradient(45deg, rgb(8 110 164),rgb(93 204 207));
  background: -moz-linear-gradient(45deg, rgb(8 110 164),rgb(93 204 207));
  background: linear-gradient(45deg, rgb(8 110 164),rgb(93 204 207));
  background: ${({color}) => color ? color : ''};
  padding: 15px;
  hyphens: auto;
  text-align: center;
`

const StyleCard = styled.div`
  min-height: 40px;    
`

const StyledCoverSkeleton = styled(Skeleton)`
  --width: 200px;
  --height: 200px;
  --border-radius: 5px;
`