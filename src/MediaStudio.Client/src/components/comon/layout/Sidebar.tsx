import React from 'react';
import Nav from "./Nav";
import styled from "styled-components";

export const Sidebar: React.FC = () => {
    return (
        <StyledSidebar>
            <StyledLogo src={''} />
            <Nav/>
        </StyledSidebar>
    );
};

const StyledSidebar = styled.div`
  grid-area: sd;
  display: grid;
  grid-template-columns: 100%;
  grid-template-rows: 80px auto;
  grid-row-gap: 80px;
  width: 100%;
  height: calc(100vh - 70px);

  background: linear-gradient(180deg, #007CA4 0%, #0064AC 0.01%, #0163AB 0.02%, #0082AB 0.03%, #2B326D 100%);
  box-shadow: 0 2.84583px 2.84583px rgba(0, 0, 0, 0.25);

  font-family: Open Sans;
  box-sizing: border-box;
`

const StyledLogo = styled.img`
  align-self: end;
  margin-left: 43px;
`

