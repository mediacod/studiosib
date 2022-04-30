import React, {useState} from 'react';
import styled from "styled-components";
import {Section} from "../components/blocks";
import {useSectionQuery} from "../../query/useSectionQuery";
import {CapsuleSelector} from "../components/common/layout/CapsuleSelector";
import {useLocation, useNavigate} from "react-router-dom";
import {PAGES} from "../../utils/const";

export const MainPageMobile = () => {

    const {pathname} = useLocation()
    const navigate = useNavigate()

    const getIdPage = () => PAGES?.find((p) => p.path === pathname)?.id
    const {data} = useSectionQuery(getIdPage())

    const selectHandler = (value: string) => {
        navigate(value)
    }

    const openSection = (index: any) => {
        navigate(`/section/${getIdPage()}/${index}`)
    }

    return (
        <StyledContainer>
            <CapsuleSelector activeKey={pathname} selectHandler={selectHandler} data={PAGES}/>

            <StyledWrapper>
                {data?.map((sectionData: any, index: number)=>
                        <Section key={sectionData.nameSection} sectionData={sectionData} index={index} openSection={openSection}/>
                )}
            </StyledWrapper>
        </StyledContainer>
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