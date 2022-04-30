import React, {useContext, useEffect, useMemo, useRef, useState} from 'react';
import styled from "styled-components";
import {FloatingPanel, Image, Slider} from "antd-mobile";
import {Back, Next, BasePlay} from "../../../components/icons";
import {ButtonPlay} from "./ButtonPlay";
import {TrackList} from "./TrackList";
import audioContext from "../../../context/audioContext";
import {useVerticalSwipe} from "beautiful-react-hooks";

interface IProps {
    openHandler: (arg: boolean)=>void;
}

export const PlayerFull: React.FC<IProps> = ({openHandler}) => {
    const [initial, setInitial] = useState(false)
    const {albumData, setAudio, playing, progress, togglePlaying, playlist, callbacks, track, PLAYER}: any = useContext(audioContext)
    const {name: nameAlbum, linkCover, } = albumData;
    const playerRef: any = useRef(null)
    const optionsSwipe: any = { direction: 'vertical', threshold: 120, preventDefault: false };
    const swipe = useVerticalSwipe(playerRef, optionsSwipe)

    const windowHeight = window.innerHeight

    useMemo(()=>{

        if(swipe.swiping && swipe.direction === 'down'){
            openHandler(false)
        }
    }, [swipe.swiping, swipe.direction])

    useEffect(()=> {
        setInitial(true)
    }, [])

    const [currentTime, setCurrentTime] = useState(progress?.played * 100)

    const onChange = (e:  any) => {
        setCurrentTime(e)
    }

    const onAfterChange = (e:  any) => {
        PLAYER.current.seekTo(e/100, 'fraction')
    }

    useEffect(()=> {
        setCurrentTime(progress?.played * 100)
    }, [progress?.played])

    const playHandler = (e: any) => {
        togglePlaying()
    }

    const play = ({idTrack}: any) => {
        setAudio({
            data: {...albumData, tracks: playlist},
            idTrack: idTrack,
        })
    }

    const minAnchor = windowHeight < 600 ? windowHeight * 0.05 : 90

    const anchors = [minAnchor, windowHeight * 0.8]

    const currentTrackIndex = playlist.findIndex((t: any) => t.idTrack === track.idTrack)
    const currentTracks = playlist.slice(currentTrackIndex)

    return (
        <StyledModalContainer>

        <StyledWrapper initial={initial} ref={playerRef}>
            <span onClick={()=>openHandler(false)}>закрыть</span>
            <StyledWrapperCover>
                {linkCover
                    ? <StyledImage lazy src={linkCover} />
                    : <StyledPlaylistCover><h2>{nameAlbum}</h2></StyledPlaylistCover>}

            </StyledWrapperCover>
            <StyledWrapperBody initial={initial} >

                <StyledInfoContainer>
                    <StyledTitleContainer>
                        <StyledTrackName>{track?.name}</StyledTrackName>
                        <StyledAlbumName>{nameAlbum}</StyledAlbumName>
                    </StyledTitleContainer>
                    <StyledSliderContainer>
                        <StyledLoaded width={progress?.loaded} />
                        <StyledSlider
                            onChange={onChange}
                            onAfterChange={onAfterChange}
                            value={currentTime}
                        />
                    </StyledSliderContainer>
                    <StyledButtonWrapper>
                        <div onClick={callbacks.setPrevTrack}><Back /></div>
                        <StyledPlayButtonContainer onClick={playHandler}>
                            <BasePlay />
                            <StyledPlayButton>
                                <ButtonPlay playing={playing}
                                            key={'play'}
                                            color={'#fff'}
                                />
                            </StyledPlayButton>
                        </StyledPlayButtonContainer>
                        <div onClick={callbacks.setNextTrack}><Next /></div>
                    </StyledButtonWrapper>
                </StyledInfoContainer>
            </StyledWrapperBody>

        </StyledWrapper>
            <StyledWrapperTracks>

            </StyledWrapperTracks>
            <StyledFloatingPanel anchors={anchors}>
                <TrackList tracks={currentTracks} play={play}/>
            </StyledFloatingPanel>
        </StyledModalContainer>
    );
};

const StyledModalContainer = styled.div`
  height: 100vh;
  overflow-y: hidden;
  -webkit-overflow-scrolling: touch;
`

const StyledWrapper = styled.div<{initial: boolean}>`
  position: fixed;
  height: 100%;
  width: 100vw;
  top: 0;
  left: 0;
  z-index: 100;
  display: block;
  box-sizing: border-box;
  background: #fff;
  padding: 20px;
  opacity: ${({initial}) => initial ? 100 : 0};
  transition: 400ms;
`

const StyledWrapperBody = styled.div<{initial: boolean}>`
  padding: 20px;
`

const StyledWrapperCover = styled.div`
  display: flex;
  justify-content: center;
  align-items: center;
  margin-top: 20px;

  @media screen and (max-height:  600px){
    margin-top: 10px;
  }
`

const StyledWrapperTracks = styled.div`
    position: sticky;
    top: 200px
`

const StyledPlaylistCover = styled.div`
  display: flex;
  justify-content: center;
  align-items: center;
  width: 100%;
  height: calc(100vw - 40px);
  max-width: 40vh;
  max-height: 40vh;
  border-radius: 5px;
  color: #fff;
  background: -webkit-linear-gradient(45deg, rgb(8 110 164),rgb(93 204 207));
  background: -moz-linear-gradient(45deg, rgb(8 110 164),rgb(93 204 207));
  background: linear-gradient(45deg, rgb(8 110 164),rgb(93 204 207));

  @media screen and (max-height:  600px){
    max-width: 35vh;
    max-height: 35vh;
  }
`

const StyledImage = styled(Image)`
  width: 100%;
  height: calc(100vw - 40px);
  max-width: 40vh;
  max-height: 40vh;
  border-radius: 5px;

  @media screen and (max-height:  700px){
    max-width: 35vh;
    max-height: 35vh;
  }

  @media screen and (max-height:  650px){
    max-width: 30vh;
    max-height: 30vh;
  }
`

const StyledInfoContainer = styled.div`
  
  @media screen and (min-height:  700px){
    margin-top: 50px;
  }
  
  @media screen and (min-height:  750px){
    margin-top: 80px;
  }
`

const StyledSlider = styled(Slider)`
  width: 100%;
  padding: 4px 0;

  .adm-slider-fill {
    background-color: #6BE8F0;
  }

  .adm-slider-track {
    background-color: rgba(211, 211, 211, 0.69);
  }
`

const StyledSliderContainer = styled.div`
  position: relative;
`

const StyledLoaded = styled.div<{width: any}>`
  position: absolute;
  left: 0;
  top: calc(50% - 1px);
  height: 2px;
  background-color: #a4a4a4;
  width: ${({width}) => width * 100}%;
`

const StyledButtonWrapper = styled.div`
  width: 180px;
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin: 12px auto;
`

const StyledPlayButtonContainer = styled.div`
  position: relative;
  display: flex;
  justify-content: space-between;
  align-items: center;
`

const StyledPlayButton = styled.div`
  top: calc(50% - 8px);
  left: calc(50% - 7px);
  position: absolute;
`

const StyledTitleContainer = styled.div`
  width: 100%;
  height: 55px;
  display: flex;
  flex-direction: column;
  align-items: center;
  margin: 25px 0;
  justify-content: space-between;
  box-sizing: border-box;
`

const StyledTrackName = styled.div`
  font-size: 18px;
  font-style: normal;
  font-weight: 700;
  line-height: 25px;
  text-align: center;
  text-overflow: ellipsis;
  white-space: nowrap;
  max-width: 80vw;
  overflow: hidden;
`

const StyledAlbumName = styled.div`
  font-size: 16px;
  font-style: normal;
  font-weight: 300;
  line-height: 22px;
  text-align: center;
  text-overflow: ellipsis;
  white-space: nowrap;
  max-width: 80vw;
  overflow: hidden;
`

const StyledFloatingPanel = styled(FloatingPanel)`
  .adm-floating-panel-header{
    box-shadow: 0 1px 11px 15px rgba(0, 0, 0, 0.03);
  }
`