import React from 'react';
import styled from "styled-components";
import {Headphones} from "../../icons";

interface IProps {
    name: string
    linkCover?: string
    id: number
    idTypeCell?: number
    size?: string
    color?: string
    variant?: 'default' | 'playlist';
    albumWidth?: number
}

export const AlbumItem = ({albumWidth, name, linkCover, id, idTypeCell, size, color, variant}: any) => {


    const cover = linkCover ? <img src={linkCover}/> : <Headphones size={40} />
    const styleAlbumCover = {"--albumWidth": albumWidth + 4 + 'px', display: 'block'}

    return (
        <StyledAlbumContainer style={styleAlbumCover}>
            <StyledCoverWrapper>
                {cover}
            </StyledCoverWrapper>
            <StyledAlbumDescription>
                <StyledAlbumName>{name}</StyledAlbumName>
                <StyledAlbumDesc></StyledAlbumDesc>
            </StyledAlbumDescription>
        </StyledAlbumContainer>
    );
};

const StyledAlbumContainer = styled.span`
  display: block;
  box-sizing: border-box;
  padding: 16px;
  box-shadow: 0 2.85714px 21.4286px rgba(0, 0, 0, 0.15);
  border-radius: 7px;
`

const StyledCoverWrapper = styled.div`
    display: flex;
    flex: 1;
    justify-content: center;
    align-items: center;
    width: 100%;
    border-radius: 4px;
    margin-bottom: 12px;
    height: calc(var(--albumWidth) - 32px);
    
    
    
    img {
        display: block;
        width: 100%;
        height: 100%;
        border-radius: 4px;
    }
`

const StyledAlbumDescription = styled.div`
  
`

const StyledAlbumName = styled.div`
  
`

const StyledAlbumDesc = styled.div`
  
`