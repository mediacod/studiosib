import React, {useMemo, useState} from 'react';
import {Swiper} from "antd-mobile";
import styled from "styled-components";
import {AlbumItem} from "./AlbumItem";
import {ISection} from "../../../settings/models";
import {isArray, isFunction, isNumber} from "lodash-es";
import {getTypeCell} from "../../../utils/utils";
import {useColorQuery} from "../../../query/useColorQuery";

export const Section:React.FC<ISection> = ({sectionData, openSection, index}) => {

    const [isSortEnable, setIsSortEnable] = useState(false)
    const {data: colors} = useColorQuery()
    const slideSize = sectionData?.cells?.length < 2 ? 100 : 45
    const canOpenSection = isFunction(openSection) && isNumber(index)
    const limitAlbumInSection = 5
    const cells = sectionData?.cells

    const openSectionHandler = () => canOpenSection ? openSection(index) : null

    const sortItems = useMemo(() => isSortEnable
        ? cells?.sort(()=> Math.random() - 0.5).slice(0, limitAlbumInSection)
        : cells?.slice(0, limitAlbumInSection), [cells])

    const getKey = (item: any, id: any) => {
        return 's' + getTypeCell(item).toString() + id
    }

    const getColor = (typeCell: number, id: number) => {
        const remains = id % colors?.length
        return isArray(colors)
            ? (typeCell === (2 || 3) && colors[remains]?.code) ? colors[remains]?.code : undefined
            : undefined
    }

    const items = sortItems.map((cell: any, index: number) => {
        const id = cell?.idObject || cell?.idAlbum || cell?.idPlaylist
        const typeCell = getTypeCell(cell)
        const variant = typeCell === (2 || 3) ? 'playlist' : 'default'

        return (
            <Swiper.Item key={getKey(cell, id)}>
                <StyledContent>
                    <AlbumItem name={cell.name}
                               linkCover={cell?.linkCover}
                               id={id}
                               idTypeCell={typeCell}
                               variant={variant}
                               color={cell?.colourCode || getColor(typeCell, id)}
                    />
                </StyledContent>
            </Swiper.Item>
        )
    })

    const openSectionItem = cells?.length > limitAlbumInSection ? <Swiper.Item key={'m'+index}>
        <StyledContent onClick={openSectionHandler}>
            <StyledContentMore>
                {/*<Button color='default' fill='none'>*/}
                    показать <br/> больше
                {/*</Button>*/}
            </StyledContentMore>
        </StyledContent>
    </Swiper.Item> : <></>



    return (
        <StyledContainer>
            <StyledTitleContainer>
                <StyledTitle onClick={openSectionHandler}>{sectionData?.nameSection}</StyledTitle>
            </StyledTitleContainer>

            <Swiper
                indicator={() => null}
                slideSize={slideSize}
                trackOffset={0}
                // stuckAtBoundary={false}
                allowTouchMove
                rubberband
            >
                {[...items, openSectionItem]}
            </Swiper>
        </StyledContainer>
    );
};

const StyledTitle = styled.div`
    font-size: 22px;
    font-weight: 700;
    line-height: 2.5;
    max-width: 98vw;
    overflow: hidden;
    white-space: nowrap;
    text-overflow: ellipsis;
`

const StyledSpan = styled.div`
  font-size: 12px;
  font-weight: 500;
  color: #333333;
  padding-right: 10px;
`

const StyledTitleContainer = styled.div`
  display: flex;
  justify-content: space-between;
  align-items: center;
`

const StyledContainer = styled.div`
    margin: 10px 0;

    &:first-child{
      margin-top: 0;
    }

  &:last-child{
    padding-bottom: 20px;
  }
`

const StyledContent = styled.div`
    height: min-content;
    min-height: 50vw;
    width: 40vw;
    display: flex;
    justify-content: center;
    align-items: start;
    user-select: none;
    
    &:first-child{
      margin-left: 0;
    }
`

const StyledContentMore = styled.div`
  display: flex;
  justify-content: center;
  align-items: center;
  height: 40vw;
  width: 40vw;
  font-weight: 700;
  font-size: 15px;
  word-break: break-all;
  text-align: center;
  color: #999999;
`