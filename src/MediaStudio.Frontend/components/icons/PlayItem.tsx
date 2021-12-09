import React from 'react';
import {IImageProps} from "../../types/common";

const PlayItem: React.FC<IImageProps> = ({action, color, size, height, className}) => {
    return (
        <svg
            id="PlayItem"
            onClick={action}
            className={`${className}`}
            fill={color}
            width={size}
            height={height || size}
            viewBox="0 0 15 19">
            <defs/>
            <path d="M14.563 9.237L0 18.474V0l14.563 9.237z" fill=""/>
        </svg>
    );
};

export default PlayItem;