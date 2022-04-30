import React, {useEffect, useState } from 'react';
import styled from "styled-components";
import {Header} from "./Header";
import {Sidebar} from "./Sidebar";

export const Layout: React.FC = ({children}) => {

    return (
        <StyledMain>
            <Header/>
            <Sidebar/>
            <StyledContent>
                {children}
            </StyledContent>
            {/*<Player/>*/}
        </StyledMain>
    );
};

const StyledMain = styled.div`
  display: grid;
  grid-template-columns: 228px 1fr;
  grid-auto-rows: 85px 1fr 70px;
  grid-template-areas:
    "sd hd"
    "sd ct"
    "pl pl";
`

const StyledContent = styled.div`
  grid-area: ct;
  overflow-y: auto;
`