import React from 'react';
import {IImageProps} from "../../types/common";

const Prev: React.FC<IImageProps> = ({action, color, size, height, className}) => {
    return (
        <svg
            id="prev"
            onClick={action}
            className={`${className}`}
            fill={color}
            width={size}
            height={height || size}
            viewBox="0 0 18 12">
            <defs/>
            <path d="M0 5.898l10.463 5.898V0L0 5.898z" fill="#5F5A5A"/>
            <path d="M7 5.898l10.463 5.898V0L7 5.898z" fill="#5F5A5A"/>
        </svg>
    );
};

export default Prev;