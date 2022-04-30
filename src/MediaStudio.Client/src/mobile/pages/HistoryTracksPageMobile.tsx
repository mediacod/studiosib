import React, {useContext, useMemo} from 'react';
import {TrackList} from "../components/blocks/TrackList";
import styled from "styled-components";
import AudioContext from "../../context/audioContext";
import {useUserHistoryQuery} from "../../query/useHistoryQuery";

export const HistoryTracksPageMobile = () => {

    const {data: historyTracks} = useUserHistoryQuery()
    const {setAudio}: any = useContext(AudioContext)

    const uniqueHistoryTracks = useMemo(()=>{
        return historyTracks?.length ?
        [...historyTracks].reverse().reduce((acc, track) => {
            if (acc.map[track.idTrack]) // если данный город уже был
                return acc; // ничего не делаем, возвращаем уже собранное

            acc.map[track.idTrack] = true; // помечаем город, как обработанный
            acc.tracks.push(track); // добавляем объект в массив городов
            return acc; // возвращаем собранное
        }, {
            map: {}, // здесь будут отмечаться обработанные города
            tracks: [] // здесь конечный массив уникальных городов
        })
            .tracks // получаем конечный массив
        : []
    }, [historyTracks])

    const play = ({idTrack}: any) => {
        setAudio({
            data: {idAlbum: 'myHistory', tracks: uniqueHistoryTracks},
            idTrack: idTrack,
        })
    }

    return (
        <>
            {historyTracks?.length
                ?   <StyledContainerTracks>
                        <StyledTitle>История прослушивания</StyledTitle>
                        <TrackList tracks={uniqueHistoryTracks} play={play}/>
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