import React from 'react';
import styled from "styled-components";
import {Header} from "./Header";
import {Sidebar} from "./Sidebar";
import {Player} from "../player/Player";

export const Layout: React.FC = ({children}) => {

    return (
        <StyledMain>
            <Header/>
            <Sidebar/>
            <StyledContent>
                {children}
            </StyledContent>
            <Player />
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
  width: 100%;
  height: 100%;
  padding: 45px 40px 40px;
  box-sizing: border-box;
`