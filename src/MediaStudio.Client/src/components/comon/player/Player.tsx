import React, {useContext} from 'react';
import audioContext from "../../../context/audioContext";
import styled from "styled-components";
import {Back, BasePlay, Next} from "../../icons";
import {ButtonPlay} from "../../../mobile/components/blocks/ButtonPlay";
import {Heart} from "../../icons/Heart";
import Download from "../../icons/Download";
import Repeat from "../../icons/Repeat";
import Change from "../../icons/Change";
import {ButtonIcon} from "../../../mobile/components/blocks/ButtonIcon";
import Queue from "../../icons/Queue";
import Mute from "../../icons/Mute";
// import cover from "./../../assets/img/album_image.jpg";

export const Player:React.FC = () => {


    const {albumData, setAudio, playing, progress, togglePlaying, playlist, callbacks, track, PLAYER}: any = useContext(audioContext)
    const {name: nameAlbum, linkCover, } = albumData;

    /**
     * Переключатель воспроизведения
     */
    const playHandler = (e: any) => {
        togglePlaying()
    }

    const volumeHandler = () => {

    }

    return (
        <StyledPlayer>
            <StyledBar>
                <StyledBarLoader width={80} />
                <StyledBarProgress width={40} />
            </StyledBar>

            <StyledPanelContainer>
                <StyledInfoContainer>
                    <StyledAlbumInfoContainer>
                        <StyledAlbumCover>
                            {linkCover
                                ? <StyledImage src={linkCover} />
                                : <StyledPlaylistCover></StyledPlaylistCover>}
                        </StyledAlbumCover>

                        <StyledAlbumInfo>
                            <StyledTrackName>track?.name</StyledTrackName>
                            <StyledAlbumName>nameAlbum</StyledAlbumName>

                        </StyledAlbumInfo>

                        <StyledActionsPanel>
                            <ButtonIcon size={14} isActive={true}>
                                <Change size={14}/>
                            </ButtonIcon>

                            <ButtonIcon isActive={true}>
                                <Repeat size={12}/>
                            </ButtonIcon>

                            <ButtonIcon isActive={true}>
                                <Heart size={12} />
                            </ButtonIcon>

                            <ButtonIcon isActive={true}>
                                <Download size={12}/>
                            </ButtonIcon>

                        </StyledActionsPanel>
                    </StyledAlbumInfoContainer>
                </StyledInfoContainer>

                <StyledControlContainer>
                    <StyledButtonWrapper>
                        <div onClick={callbacks.setPrevTrack}><Back size={10}/></div>
                        <StyledPlayButtonContainer onClick={playHandler}>
                            <BasePlay size={48}/>
                            <StyledPlayButton playing={playing}>
                                <ButtonPlay playing={playing}
                                            key={'play'}
                                            color={'#fff'}
                                            size={12}
                                />

                            </StyledPlayButton>
                        </StyledPlayButtonContainer>

                        <div onClick={callbacks.setNextTrack}><Next size={10}/></div>

                    </StyledButtonWrapper>
                </StyledControlContainer>

                <StyledExtraContainer>
                    <StyledTimeContainer>
                        <StyledCurrentTime>13:31</StyledCurrentTime>/
                        <StyledDuration>18:61</StyledDuration>

                    </StyledTimeContainer>

                    <StyledQueue>
                        <ButtonIcon isActive={true} size={16}>
                            <Queue size={16} />
                        </ButtonIcon>
                    </StyledQueue>

                    <StyledVolumeContainer>
                        <ButtonIcon size={16}>
                            <Mute />
                        </ButtonIcon>

                        <StyledVolumeRange>
                            <StyledVolumeRangeInput className='range'
                                   type="range"
                                   min="0"
                                   max="1"
                                   step="0.01"
                                   defaultValue="65"
                                   onChange={volumeHandler}
                            />
                        </StyledVolumeRange>

                    </StyledVolumeContainer>
                </StyledExtraContainer>
            </StyledPanelContainer>
        </StyledPlayer>
    )
}

const StyledPlayer = styled.div`
  position: relative;
  width: 100vw;
  height: 100%;
  background-color: #fff;
`

const StyledBar = styled.div`
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 4px;
  background-color: #D7D7D7;
  transition: 200ms;
  
  &::before{
    content: '';
    position: absolute;
    top: 0;
    left: 0;
    height: 20px;
    width: 100%;
    transform: translateY(-10px);
    //background-color: #00b578;
  }
  
  &:hover{
    height: 7px;
    transform: translateY(-2px);
  }
`
const StyledBarLoader = styled.div<{width: number}>`
  position: absolute;
  top: 0;
  left: 0;
  width: ${({width})=> width}%;
  height: 100%;
  background-color: #B1AFAF;
`

const StyledBarProgress = styled.div<{width: number}>`
  position: absolute;
  top: 0;
  left: 0;
  width: ${({width})=> width}%;
  height: 100%;
  background-color: #6BE8F0;
`

const StyledPanelContainer = styled.div`
  display: grid;
  grid-template-areas: "info control extra";
  justify-items: stretch;
  align-items: center;
  justify-content: space-between;
  grid-template-rows: 100%;
  grid-template-columns: 1fr 187px 1fr;
  width: 100%;
  height: 100%;
`

const StyledInfoContainer = styled.div`
  grid-area: info;
  //background-color: #e3a48f;
  width: 100%;
  height: 100%;
`

const StyledControlContainer = styled.div`
  grid-area: control;
  display: flex;
  align-items: center;
  width: 187px;
  height: 100%;
  //background-color: #b3c782;
`

const StyledExtraContainer = styled.div`
  grid-area: extra;
  box-sizing: border-box;
  display: flex;
  align-items: center;
  justify-content: end;
  width: 100%;
  height: 100%;
  padding: 0 20px;
`

const StyledButtonWrapper = styled.div`
  width: 133px;
  height: 48px;
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

const StyledPlayButton = styled.div<{playing: boolean}>`
  top: calc(50% - 8px);
  left: calc(50% - ${({playing})=> playing ? 7 : 5}px);
  position: absolute;
`

const StyledAlbumInfoContainer = styled.div`
  display: flex;
  align-items: center;
  justify-content: start;
`

const StyledAlbumCover = styled.div`
  display: flex;
  align-items: center;
  width: 45px;
  height: 45px;
  padding: 12px;
  min-width: 45px;
`

const StyledAlbumInfo = styled.div`
  display: flex;
  flex-direction: column;
  color: #000;
  width: 160px;
  min-width: 160px;
`

const StyledTrackName = styled.div`
  font-style: normal;
  font-weight: 400;
  font-size: 12px;
  line-height: 16px;
  color: #4B4B4B;
`

const StyledAlbumName = styled.div`
  font-style: normal;
  font-weight: 400;
  font-size: 8px;
  line-height: 12px;
  color: #4B4B4B;
`

const StyledImage = styled.img`
  width: 100%;
  height: 100%;
  border-radius: 5px;
`

const StyledPlaylistCover = styled.div`
  width: 100%;
  height: 100%;
  border-radius: 5px;
  background-color: #D7D7D7;
`

const StyledActionsPanel = styled.div`
  display: flex;
  align-items: center;
  width: 100%;
  height: 100%;
  padding: 0 45px;
  border-radius: 5px;
`

const StyledTimeContainer = styled.div`
  display: flex;
  align-items: center;
  width: 100%;
  height: 100%;
  padding: 0 40px;
  max-width: 60px;
`

const StyledCurrentTime = styled.span`
  font-weight: 400;
  font-size: 12px;
  line-height: 16px;
  color: #4B4B4B;
  padding: 0 4px;
`

const StyledDuration = styled.span`
  font-weight: 400;
  font-size: 12px;
  line-height: 16px;
  color: #B1AFAF;
  padding: 0 4px;
`

const StyledQueue = styled.div`

`
const StyledVolumeContainer = styled.div`
  
`

const StyledVolumeRange = styled.div`
  background-color: #FFFFFF;
  box-shadow: 0px 2px 5px rgba(0, 0, 0, 0.15);
  border-radius: 2px;
  width: 23px;
  height: 84px;
  padding: 0;
  position: absolute;
  transform: translateY(-120px);
  //top: -90px;
  right: 26px;

  @media(min-width: 1366px) and (max-width: 1600px) {
    border-radius: 1.42px;
    width: 17px;
    height: 60px;
    top: -65px;
  }
`

const StyledVolumeRangeInput = styled.input`
  width: 67px;
  height: 3px;
  margin: 0;
  transform-origin: 39px 29px;
  transform: rotate(-90deg);
  -webkit-appearance: none;
  background: #B1AFAF;

  @media(min-width: 1366px) and (max-width: 1600px) {
    width: 52px;
    height: 2px;
    transform-origin: 27px 19px;
  }

  &::-webkit-slider-thumb {
    -webkit-appearance: none;
    background: #5F5A5A;
    width: 7px;
    height: 7px;
    border-radius: 50%;
    cursor: pointer;

    @media(min-width: 1366px) and (max-width: 1600px) {
      width: 6px;
      height: 6px;
    }
  }

  &::-moz-range-track {
    width: 67px;
    height: 3px;
    margin: 0;
    // transform-origin: 39px 29px;
    // transform: rotate(-90deg);
    background: #B1AFAF;
  }

  &::-moz-range-thumb {
    background: #5F5A5A;
    width: 7px;
    height: 7px;
    border-radius: 50%;
    cursor: pointer;
    border: none;

    @media(min-width: 1366px) and (max-width: 1600px) {
      width: 6px;
      height: 6px;
    }
  }

  &:focus {
    outline: none;
  }
`
