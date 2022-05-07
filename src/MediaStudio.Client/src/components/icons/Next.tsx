import React from 'react';
import {IImageProps} from "../../types/iconType";

export const Next:React.FC<IImageProps> = ({color, size}) => {

    const height = size ? size : 18
    const width = size ? size * 1.44 : 26

    return (
        <svg width={width} height={height} viewBox="0 0 28 20" fill="none" xmlns="http://www.w3.org/2000/svg">
            <path d="M27.7852 9.79685L11.2079 19.1472L11.2079 0.446534L27.7852 9.79685Z" fill="#5F5A5A"/>
            <path d="M17.1025 9.79685L0.525273 19.1472L0.525272 0.446534L17.1025 9.79685Z" fill="#5F5A5A"/>
        </svg>
    );
};