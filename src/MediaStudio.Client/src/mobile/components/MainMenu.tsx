import React, {useContext} from 'react';
import {Button} from "antd-mobile";
import styled from "styled-components";

interface IProps {
    logout: ()=> void;
    user?: any;
    login?: string;
    title?: string
}

export const MainMenu: React.FC<IProps> = ({logout, user, title}) => {

    return (
        <StyledModalContainer>
            <StyledTitle>{title && title}</StyledTitle>
            <StyledIndent />
            <Button color='danger' fill='none' onClick={logout}>
                Выход
            </Button>
        </StyledModalContainer>
    );
};

const StyledTitle = styled.div`
  font-weight: bold;
`

const StyledIndent = styled.div`
  height: 30px;
`

const StyledModalContainer = styled.div`
  display: flex;
  justify-content: center;
  flex-direction: column;
  align-items: center;
`