import React from 'react';
import {IImageProps} from "../../types/iconType";

const Queue: React.FC<IImageProps> = ({action, color, size, height, className}) => {
    return (
        <svg
            id="next"
            onClick={action}
            className={`${className}`}
            fill={color}
            width={size}
            height={height || size}
            viewBox="0 0 26 16">
            <defs/>
            <path fill="#5F5A5A" d="M25.484 0v2.963H0V0zM25.484 6.518v2.963H0V6.518zM25.484 13.037V16H0v-2.963z"/>
        </svg>
    );
};

export default Queue;