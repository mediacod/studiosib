import React, {useCallback, useEffect, useMemo, useState} from 'react';
import {List, Modal} from "antd-mobile";
import {CheckCircleOutline} from 'antd-mobile-icons'
import {useUserPlaylistQuery} from "../../../query/useUserPlaylistQuery";
import {useUserPlaylistOneQuery} from "../../../query/useUserPlaylistOneQuery";
import styled from "styled-components";
import {CreatePlaylistForm} from "../auth/form/CreatePlaylistForm";
import {isBoolean} from "lodash-es";

interface IProps {
    idTrack: any
    visible: boolean
    toggleVisible: () => void
    idTrackToPlaylist?: any
    isLocatedUserPlaylist: boolean
}

export const AddTrackInPlaylistModal:React.FC<IProps> = ({idTrack, visible, toggleVisible, idTrackToPlaylist, isLocatedUserPlaylist}) => {

    const {data} = useUserPlaylistQuery()
    const {addTrack, removeTrack, isLoading, isError, isSuccess, playlistData} = useUserPlaylistOneQuery()
    const [visibleCreatePlaylist, setVisibleCreatePlaylist] = useState(false)

    const toggleVisibleCreatePlaylist = (isOpen?: boolean) => setVisibleCreatePlaylist((prevState) => isBoolean(isOpen) ? isOpen : !prevState)

    // const error = isError ? <StyledError>ошибка добавления трека</StyledError> : ''

    const action = useCallback( (idPlaylist) => {
        toggleVisible()
        return !isLoading &&
        (!isLocatedUserPlaylist
                ? addTrack({idPlaylist, idTrack})
                : removeTrack({idPlaylist, idTrackToPlaylist})
        )
    }, [])

    const PlaylistList =
        <>
            <StyledPlaylistListContainer>
                <List header='Выберите плейлист'>
                    {data?.map((item: any) => <List.Item
                        key={item.idPlaylist}
                        onClick={() => action(item.idPlaylist) }
                        // description={error}
                    >{item.name}</List.Item>)}
                    <List.Item onClick={() => setVisibleCreatePlaylist(true)} arrow={false}>
                        <StyledSpan>создать новый плейлист</StyledSpan>
                    </List.Item>
                </List>
            </StyledPlaylistListContainer>

            <Modal
                visible={visibleCreatePlaylist}
                content={<CreatePlaylistForm toggleVisible={toggleVisibleCreatePlaylist} />}
                closeOnAction
                onClose={() => setVisibleCreatePlaylist(false)}
                showCloseButton
            />
        </>

    return (
        <Modal
            visible={visible}
            content={PlaylistList}
            closeOnAction
            closeOnMaskClick
            onClose={toggleVisible}
            actions={[]}
        />
    );
};

const StyledPlaylistListContainer = styled.span`
  display: block;
  max-height: 50vh;
  overflow-y: scroll;
`

const StyledError = styled.span`
  color: #ff411c;
`

const StyledSpan = styled.span`
  color: #5d5d5d;
`