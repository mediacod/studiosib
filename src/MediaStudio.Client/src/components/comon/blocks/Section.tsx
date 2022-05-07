import React, {useMemo, useRef, useState} from 'react';
import styled from "styled-components";
import {AlbumItem} from "./AlbumItem";
import useCalcColumn from "../../../hooks/useCalcColumn";
import {getTypeCell} from "../../../utils/utils";
import {isArray} from "lodash-es";
import {useColorQuery} from "../../../query/useColorQuery";

export const Section = ({sectionData}:any) => {

    const [isSortEnable, setIsSortEnable] = useState(false)
    const myRef = useRef(null)
    const { style, column, albumWidth } = useCalcColumn(myRef)
    const {data: colors} = useColorQuery()
    const cells = sectionData?.cells

    const getKey = (item: any, id: any) => {
        return 's' + getTypeCell(item).toString() + id
    }

    const sortItems = useMemo(() => isSortEnable
        ? cells?.sort(()=> Math.random() - 0.5)
        : cells, [cells])

    const getColor = (typeCell: number, id: number) => {
        const remains = id % colors?.length
        return isArray(colors)
            ? (typeCell === (2 || 3) && colors[remains]?.code) ? colors[remains]?.code : undefined
            : undefined
    }

    const items = sortItems?.map((cell: any, index: number) => {
        const id = cell?.idObject || cell?.idAlbum || cell?.idPlaylist
        const typeCell = getTypeCell(cell)
        const variant = typeCell === (2 || 3) ? 'playlist' : 'default'
        if(index >= column) return
        console.log(column)
        return (
                    <AlbumItem name={cell.name}
                               linkCover={cell?.linkCover}
                               id={id}
                               idTypeCell={typeCell}
                               variant={variant}
                               color={cell?.colourCode || getColor(typeCell, id)}
                               albumWidth={albumWidth}
                    />
        )
    })

    return (
        <StyledSectionContainer>
            <StyledSectionHeader>
                <StyledSectionName>name section</StyledSectionName>
                <StyledSectionExtra>extra</StyledSectionExtra>
            </StyledSectionHeader>
            <StyledSection style={style} ref={myRef}>
                {items}
            </StyledSection>
        </StyledSectionContainer>
    );
};

const StyledSectionContainer = styled.div`
  
`

const StyledSectionHeader = styled.div`
  display: flex;
  justify-content: space-between;
  align-items: center;
`

const StyledSectionName = styled.div`
  
`

const StyledSectionExtra = styled.div`

`

const StyledSection = styled.div`
  display: grid;
  grid-template-columns: repeat(auto-fill,minmax(var(--minColumnWidth), 1fr));
  grid-auto-rows: 0;
  grid-template-rows: 1fr;
  margin-bottom: 27px;

  @media (max-width: 500px) {
    grid-template-columns: repeat(8, calc(100vw - 72%));
    overflow-x: scroll;
  }
`