import React, {cloneElement, MouseEventHandler, ReactElement} from 'react';
import styled from "styled-components";


interface IProps {
    children: ReactElement,
    size?: number
    isActive?: boolean
    onClick?: MouseEventHandler<HTMLDivElement>
}

export const ButtonIcon:React.FC<IProps> = ({children, size = 12, isActive, onClick}) => {

    const color = isActive ? "#6BE8F0" : "#4B4B4B"

    return (
        <StyledButtonIcon size={size} onClick={onClick}>
            {cloneElement(children, {...children.props, color: color})}
        </StyledButtonIcon>
    );
};

const StyledButtonIcon = styled.div<{size: number}>`
    display: flex;
    align-items: center;
    justify-content: space-between;
    width: ${({size})=> size}px;
    height: ${({size})=> size}px;
    padding: 0 9px;
  
    &:hover{
      opacity: .8;
    }
`