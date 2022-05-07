import React from 'react';
import {IImageProps} from "../../types/iconType";

export const BasePlay:React.FC<IImageProps> = ({size}) => {

    const height = size ? size : 58
    const width = size ? size : 58

    return (
        <svg width={width} height={height} viewBox="0 0 58 58" fill="none" xmlns="http://www.w3.org/2000/svg">
            <ellipse cx="28.9678" cy="29.3971" rx="23.8506" ry="23.8356" fill="#69CAC4"/>
            <ellipse opacity="0.5" cx="29.0183" cy="29" rx="29.0183" ry="29" fill="#97DEF4"/>
        </svg>
    );
};