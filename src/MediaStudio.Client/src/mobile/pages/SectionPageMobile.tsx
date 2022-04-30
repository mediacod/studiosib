import React from 'react';
import {SectionGrid} from "../components/blocks/SectionGrid";
import {useParams} from "react-router-dom";
import {useSectionQuery} from "../../query/useSectionQuery";
import styled from "styled-components";

export const SectionPageMobile = () => {

    const {idPage, index} = useParams()

    const {data} = useSectionQuery(idPage)
    const isData = index && data?.length && !(index > data?.length)
    const albums = isData ? data[index]?.cells : []

    if(!isData){
        return <StyledTitle>Данных нет</StyledTitle>
    }

    return (
        <div>
            <StyledTitleContainer>
                <StyledTitle>{data[index]?.nameSection}</StyledTitle>
            </StyledTitleContainer>
            <SectionGrid albums={albums} key={1} />
        </div>
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
  padding: 0 15px;
`

const StyledTitleContainer = styled.div`
  display: flex;
  justify-content: space-between;
  align-items: center;
`