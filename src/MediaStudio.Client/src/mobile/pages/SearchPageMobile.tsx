import React, {useContext, useEffect, useRef, useState} from 'react';
import {useSearchQuery} from "../../query/useSearchQuery";
import {SearchBar} from "antd-mobile";
import {Section} from "../components/blocks";
import styled from "styled-components";
import {usePageName} from "../../hooks/pageName.hook";
import {TrackList} from "../components/blocks/TrackList";
import AudioContext from "../../context/audioContext";

export const SearchPageMobile = () => {

    const {setAudio}: any = useContext(AudioContext)
    const [value, setValue] = useState('')
    const [query, setQuery] = useState('')
    const {data} = useSearchQuery(query)
    const inputRef = useRef<any>(null);
    usePageName('Поиск')

    useEffect(()=>{
        inputRef.current!.focus({
            cursor: 'start',
        });
    },[])

    let interval: any;
    const sendTime = 3
    let counter = 0

    const queryHandler = (value: any) => {
        setValue(value)
        counter = 0
        clearInterval(interval);
        interval = null;
        interval = setInterval(() => {
            if (counter === sendTime) {
                clearInterval(interval);
                setQuery(value)
                return;
            }
            counter ++
        }, 100);
    }

    const createSection = (title: string, data: any) => {
        return {
            nameSection: title,
            cells: data
        }
    }

    const play = ({idTrack}: any) => {
        setAudio({
            data: {tracks: data.tracks, idAlbum: 'search:' + query},
            idTrack: idTrack,
        })
    }

    return (
        <>
            <StyledWrapper>
                <StyledSearchBar
                    placeholder='Введите что-нибудь'
                    onChange={(val: any) => {
                        queryHandler(val)
                    }}
                    value={value}
                    ref={inputRef}
                />
            </StyledWrapper>
            <StyledSectionWrapper>
                {data?.albums?.length ? <Section sectionData={createSection('Альбомы', data?.albums)}/> : null }
                {data?.playlists?.length ? <Section sectionData={createSection('Плейлисты', data?.playlists)}/> : null }
            </StyledSectionWrapper>
            {data?.tracks?.length
                ?   <StyledContainerTracks>
                    <StyledTitle>Треки</StyledTitle>
                    <TrackList tracks={data?.tracks} play={play}/>
                </StyledContainerTracks>
                : null
            }
        </>
    );
};

const StyledWrapper = styled.div`
  display: block;
  justify-content: center;
  align-items: center;
  padding: 10px;
  max-width: 100vw;
`

const StyledSearchBar = styled(SearchBar)`
  --height: 36px;
  .adm-input-element{
    font-size: 16px;
  }
`

const StyledSectionWrapper = styled.div`
  display: block;
  justify-content: center;
  align-items: center;
  margin-left: 10px;
  margin-top: 10px;
`

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