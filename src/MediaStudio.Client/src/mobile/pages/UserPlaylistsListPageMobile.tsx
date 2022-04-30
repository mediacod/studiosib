import React, {useContext, useState} from 'react';
import {usePageName} from "../../hooks/pageName.hook";
import {AuthFormSwitch} from "../components/auth/AuthFormSwitch";
import AuthContext from "../../context/authContext";
import {Button, Form, Input, Modal} from "antd-mobile";
import {SectionGrid} from "../components/blocks/SectionGrid";
import styled from "styled-components";
import {useUserPlaylistQuery} from "../../query/useUserPlaylistQuery";
import {CreatePlaylistForm} from "../components/auth/form/CreatePlaylistForm";
import {isBoolean} from "lodash-es";

export const UserPlaylistsListPageMobile = () => {

    usePageName('Плейлисты')
    const {isAuth, user} = useContext(AuthContext)
    const {data: playlists} = useUserPlaylistQuery()
    const [visible, setVisible] = useState(false)


    if(!isAuth || !user?.idUser){
        return (<AuthFormSwitch />)
    }

    const toggleVisible = (isOpen?: boolean) => setVisible((prevState) => isBoolean(isOpen) ? isOpen : !prevState)

    return (
        <StyledContainer>
            <SectionGrid albums={playlists} variantAlbum={'playlist'}/>
            <StyledButton
                onClick={() => setVisible(true)}
                size='small'
            >
                создать плейлист
            </StyledButton>
            <Modal
                visible={visible}
                content={<CreatePlaylistForm toggleVisible={toggleVisible} />}
                closeOnAction
                onClose={() => setVisible(false)}
                showCloseButton
            />
        </StyledContainer>
    );
};

const StyledRadioColor = styled.div<{color: string}>`
  background-color: ${({color})=> color};
  width: 30px;
  height: 30px;
  display: block;
`

const testPlaylist = [
    {name: 'Любимые', color: '#eaeaea'},
]

const StyledButton = styled(Button)`
    margin: 10px auto;
    color: #333333;
`

const StyledContainer = styled.div`
    display: flex;
    flex-direction: column;
    padding-top: 15px;
`