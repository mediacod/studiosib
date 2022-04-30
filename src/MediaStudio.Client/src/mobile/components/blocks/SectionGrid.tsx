import React from 'react';
import {Grid} from "antd-mobile";
import {AlbumItem} from "./AlbumItem";
import styled from "styled-components";
import {useColorQuery} from "../../../query/useColorQuery";
import {getTypeCell} from "../../../utils/utils";
import {isArray} from "lodash-es";

interface IProps {
    albums: any
    variantAlbum?: 'default' | 'playlist';
    typeCell?: string
}

export const SectionGrid:React.FC<IProps> = ({albums, variantAlbum}) => {

    const {data: colors} = useColorQuery()
    // const {data} = useColorQuery()


    const getColor = (typeCell: number, id: number) => {
        const remains = id % colors?.length
        return isArray(colors)
            ? (typeCell === (2 || 3) && colors[remains]?.code) ? colors[remains]?.code : undefined
            : undefined
    }

    const getKey = (item: any, id: number) => {
        return 's' + getTypeCell(item) + id
    }

    const SectionData = albums?.map((item: any, index: number) => {

        const id = item?.idObject || item?.idAlbum || item?.idPlaylist
        const typeCell = getTypeCell(item)
        const variant = variantAlbum || (typeCell === (2 || 3) ? 'playlist' : 'default')

        return(
            <Grid.Item key={getKey(item, id)}>
                <AlbumItem
                    variant={variant}
                    name={item?.name}
                    size={'45vw'}
                    color={item?.colourCode || getColor(typeCell, id)}
                    idTypeCell={typeCell}
                    id={id}
                />
            </Grid.Item>
        )
    })

    return (
    <StyledContainer>
        <StyledGrid columns={2} gap={8}>
            {SectionData}
        </StyledGrid>
    </StyledContainer>
    );
};

const StyledContainer = styled.div`
    padding: 10px;
`

const StyledGrid = styled(Grid)`
  --gap-vertical: 20px;
`