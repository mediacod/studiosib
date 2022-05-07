import React from 'react';
import {IImageProps} from "../../types/iconType";

export const Pause:React.FC<IImageProps> = ({color = '#fff', size}) => {

    const height = size ? size : 15
    const width = size ? size * 1.15 : 13

    return (
        <svg width={width} height={height} viewBox="0 0 13 15" fill="none" xmlns="http://www.w3.org/2000/svg">
            <rect x="0.302734" y="0.176514" width="4.26477" height="14.5" fill={color}/>
            <rect x="7.97949" y="0.176514" width="4.26463" height="14.5" fill={color}/>
        </svg>
    );
};