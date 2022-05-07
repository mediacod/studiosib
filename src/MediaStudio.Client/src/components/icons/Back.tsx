import React from 'react';
import {IImageProps} from "../../types/iconType";

export const Back:React.FC<IImageProps> = ({color, size}) => {

    const height = size ? size : 18
    const width = size ? size * 1.44 : 26

    return (
        <svg width={width} height={height} viewBox="0 0 26 18" fill="#5F5A5A" xmlns="http://www.w3.org/2000/svg">
            <path d="M-3.76714e-07 9.05729L15.4517 17.7672L15.4517 0.347413L-3.76714e-07 9.05729Z" />
            <path d="M9.95508 9.0573L25.4067 17.7672L25.4067 0.347413L9.95508 9.0573Z" />
        </svg>
    );
};