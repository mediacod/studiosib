import React, {useContext, useRef, useState} from 'react';
import {Avatar, List} from "antd-mobile";
import ReactPlayer from "react-player";
import audioContext from "../../../context/audioContext";
import styled from "styled-components";
import {ButtonPlay} from "./ButtonPlay";

export const Player = ({openHandler}: any) => {

    const {albumData, track, playing, callbacks, progress, togglePlaying, playlist, setPlayer, PLAYER}: any = useContext(audioContext)
    const playerHiddenStyle = !track?.link ? {display: 'none'} : {}

    const playHandler = (e: any) => {
        e.stopPropagation();
        togglePlaying()
    }

    return (
        <>
            <List style={playerHiddenStyle}>
                <StyledProgressContainer width={progress?.played * 100 || 0} />
                <StyledPlayer
                    key={'player'}
                    prefix={<Avatar src={albumData?.linkCover} />}
                    description={albumData?.name}
                    onClick={() => {openHandler(true)}}
                    extra={[<ButtonPlay onClick={playHandler} key={'play'} playing={playing}/>]}
                    arrow={false}
                >
                    <StyledPlayerName>{track.name}</StyledPlayerName>
                </StyledPlayer>
            </List>
            <StyledReactPlayer
                url={track?.link}
                playing={playing}
                onEnded={callbacks.onEnded}
                onProgress={callbacks.onProgress}
                ref={PLAYER}
            />
        </>
    );
};

const StyledReactPlayer = styled(ReactPlayer)`
  display: none;
`

const StyledPlayer = styled(List.Item)`
  --padding-right: 20px;
  font-size: 12px;
`

const StyledPlayerName = styled.span`
  display: block;
  white-space: nowrap;
  text-overflow: clip;
  overflow: hidden;
  max-width: 70vw;
  font-size: 15px;

  @media screen and (max-width:  350px){
    max-width: 65vw;
    text-overflow: ellipsis;
    white-space: nowrap;
  }
`
const StyledProgressContainer = styled.div<{width: number}>`
  display: block;
  position: relative;
  width: ${({width}) => width}%;
  height: 3px;
  background-color: #6BE8F0;
`
