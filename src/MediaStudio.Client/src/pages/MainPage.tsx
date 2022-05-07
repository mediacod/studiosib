import React from 'react';
import {Section} from "../components/comon/blocks/Section";
import {useLocation, useNavigate} from "react-router-dom";
import {PAGES} from "../utils/const";
import {useSectionQuery} from "../query/useSectionQuery";
import styled from "styled-components";

export const MainPage = () => {

    const {pathname} = useLocation()
    const navigate = useNavigate()

    const getIdPage = () => PAGES?.find((p) => p.path === pathname)?.id
    const {data} = useSectionQuery(getIdPage())

    return (
        <div>
            <StyledWrapper>
                {data?.map((sectionData: any, index: number)=>
                    <Section key={sectionData.nameSection} sectionData={sectionData} index={index} />
                )}
            </StyledWrapper>
        </div>
    );
};

const StyledContainer = styled.div`
  width: 100%;
  height: 100%;
`

const StyledWrapper = styled.div`
  display: block;
  justify-content: center;
  align-items: center;
  margin-left: 10px;
  margin-top: 10px  ;
`