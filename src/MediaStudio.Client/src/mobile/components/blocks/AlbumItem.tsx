import React from 'react';
import styled from "styled-components";
import {Headphones} from "../../../components/icons";
import {Link} from "react-router-dom";
import {TYPE_AUDIO_CODE} from "../../../utils/const";

interface IProps {
    name: string
    linkCover?: string
    id: number
    idTypeCell?: number
    size?: string
    color?: string
    variant?: 'default' | 'playlist';
}

export const AlbumItem:React.FC<IProps> = ({name, linkCover, id, idTypeCell, size, color, variant}: any) => {

    const cover = linkCover ? <img src={linkCover}/> : <Headphones size={40} color={color && "#fff"}/>

    if(variant === 'playlist'){
        return (
            <StyledContainer>
                <Link to={`/${TYPE_AUDIO_CODE[idTypeCell]}/${id}`}>
                    <StyledWrapperPlaylist size={size}>
                        <StyledCoverWrapperPlaylist color={color}>{cover}</StyledCoverWrapperPlaylist>
                        <StyledTitlePlaylist>{name}</StyledTitlePlaylist>
                    </StyledWrapperPlaylist>
                </Link>
            </StyledContainer>
        );
    }

    return (
        <StyledContainer>
            <Link to={`/${TYPE_AUDIO_CODE[idTypeCell]}/${id}`}>
                <StyledCoverWrapper size={size} color={color}>
                    {cover}
                </StyledCoverWrapper>
                <StyledTitle>{name}</StyledTitle>
            </Link>
        </StyledContainer>
    );
};

const StyledContainer = styled.div`
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  width: 100%;
  height: 100%;
`

const StyledCoverWrapper = styled.div<{size?: any, color?: any}>`
  display: flex;
  justify-content: center;
  align-items: center;
  width: ${({size}) => size ? size : '40vw'};
  height: ${({size}) => size ? size : '40vw'};
  background-color: #EBEBEB;
  background-image: linear-gradient(45deg,
    ${({color}) => color ? '#f1f1f1' : '#EBEBEB'},
    ${({color}) => color ? color : '#EBEBEB'});
  border-radius: 10px;
  margin: 0;
  
  img {
    width: 100%;
    border-radius: 5px;
  }
`


const StyledWrapperPlaylist = styled.div<{size?: any}>`
  display: flex;
  flex-direction: column;
  width: ${({size}) => size ? size : '40vw'};
  height: ${({size}) => size ? size : '40vw'};
  border-radius: 10px;
  border: 2px solid #EBEBEB;
  box-shadow: 3.72542px 33.5288px 59.6068px rgba(229, 229, 229, 0.15);
  overflow: hidden;
`

const StyledCoverWrapperPlaylist = styled.div`
  display: flex;
  flex: 1;
  justify-content: center;
  align-items: center;
  width: 100%;
  height:100%;
  background-color: #EBEBEB;
  background-color: ${({color}) => color ? color : '#EBEBEB'};
  // background-image: linear-gradient(45deg,
  // ${({color}) => color ? '#f1f1f1' : '#EBEBEB'},
  // ${({color}) => color ? color : '#EBEBEB'});
  margin: 0;

  img {
    width: 100%;
    border-radius: 5px;
  }
`

const StyledTitle = styled.div`
  height: 35px;
  width: 100%;
  color: #000;
  font-size: 12px;
  line-height: 1.3;
  text-align: center;
  font-weight: 500;
  align-items: flex-start;
  overflow: hidden;
  text-overflow: ellipsis;
  padding-top: 5px;
  display: block;
  white-space: pre-line;
`

const StyledTitlePlaylist = styled.div`
    width: 100%;
    height: 40px;
    background-color: #fff;
    color: #000;
    font-size: 14px;
    font-weight: 500;
    text-align: center;
    line-height: 2.5;
    overflow: hidden;
    text-overflow: ellipsis;
    display: block;
    white-space: nowrap;
`