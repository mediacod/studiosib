import React, {useContext, useState} from 'react';
import {List, Modal, SwipeAction} from "antd-mobile";
import {HeartFill} from "antd-mobile-icons";
import AudioContext from "../../../context/audioContext";
import styled from "styled-components";
import configContext from "../../../context/configContext";
import AuthContext from "../../../context/authContext";
import {useUpdateFavoriteTrackQuery} from "../../../query/useUpdateFavoriteTrackQuery";
import {AddTrackInPlaylistModal} from "./AddTrackInPlaylistModal";
import {useUserPlaylistOneQuery} from "../../../query/useUserPlaylistOneQuery";
import {useFileSave} from "../../../hooks/useFileSave";

interface IProps {
    idTrack: number | string,
    name: string,
    play: ({idTrack}: any) => void;
    link?: string;
    restProps?: any
    attributes?: any
    idTrackToPlaylist?: string
}

export const Track:React.FC<IProps> = ({idTrack, name, play, attributes, link, idTrackToPlaylist}) => {

    const {track, playing}: any = useContext(AudioContext)
    const {isAuth}: any = useContext(AuthContext)
    const {idFavoritesTracks}: any = useContext(configContext)
    const [visible, setVisible] = useState(false)
    const isFavorite = idFavoritesTracks?.includes(idTrack)
    const isPlaying = playing && idTrack === track?.idTrack
    const isLocatedUserPlaylist = attributes?.userPlaylist && !!idTrackToPlaylist
    const {trackSave} = useFileSave()

    const toggleVisible = () => setVisible((prevState) => !prevState)

    const {addTodoMutation, removeTodoMutation} = useUpdateFavoriteTrackQuery()
    const {removeTrack} = useUserPlaylistOneQuery()

    const playHandler = () => {
        play({idTrack})
    }

    const addFavoriteHandler = (e:any) => {
        e.stopPropagation();
        try {
            !isFavorite
                ? addTodoMutation.mutate(idTrack)
                : removeTodoMutation.mutate(idTrack)
        }catch (e){
        }
    }

    const extra = isAuth
        ? [<HeartFill
            key={'HeartFill'}
            color={isFavorite ? '#2585d5' : '#999'}
            onClick={addFavoriteHandler}/>
        ]
        : []
    // trackSave

    const leftActions: any = [
        {
            key: 'download',
            text: 'скачать',
            color: 'light',
            onClick: async () => {
                await trackSave({link, name})
            },
        },
    ]

    const rightActions: any = isAuth ? [
        {
            key: 'inPlaylist',
            text: isLocatedUserPlaylist ? 'удалить из плейлиста' : 'добавить в плейлист',
            color: 'light',
            onClick: isLocatedUserPlaylist ? () => removeTrack({
                idTrackToPlaylist,
                idPlaylist: attributes?.idPlaylist
            }) : toggleVisible,
        }
    ] : []

    return (
        <>
            <SwipeAction
                key={idTrack}
                leftActions={leftActions}
                rightActions={rightActions}
            >
                <StyledListItem key={idTrack}
                                isActive={idTrack === track.idTrack}
                                onClick={playHandler}
                                extra={extra}
                                arrow={false}
                >
                    <StyledTrackName isPlaying={isPlaying}>
                        {name}
                    </StyledTrackName>
                </StyledListItem>
            </SwipeAction>
            {!isLocatedUserPlaylist && <AddTrackInPlaylistModal
                idTrack={idTrack}
                visible={visible}
                toggleVisible={toggleVisible}
                idTrackToPlaylist={idTrackToPlaylist}
                isLocatedUserPlaylist={isLocatedUserPlaylist}
            />}
        </>
    );
};

const StyledListItem = styled(List.Item)<{isActive: boolean}>`
    background-color: ${({isActive}) => isActive ? '#f4f4f6' : '#fff'};
  
  .adm-list-item-content-main{
    width: 90%;
  }
`

const StyledTrackName = styled.span<{isPlaying: boolean}>`
    display: block;
    overflow: hidden;
    white-space: nowrap;
    text-overflow: ellipsis;
    width: 100%;
    color: ${({isPlaying}) => isPlaying ? '#111111' : '#707070'};
    font-weight: ${({isPlaying}) => isPlaying ? 600 : 400};
`