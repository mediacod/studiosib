import React from 'react';
import {IImageProps} from "../../types/common";

const PopupMenu: React.FC<IImageProps> = ({action, color, size, height, className}) => {
    return (
        <svg
            id="popupMenu"
            onClick={action}
            className={`${className}`}
            fill={color}
            width={size}
            height={height || size}
            viewBox="0 0 19 4">
            <defs/>
            <circle r="1.779" fill="#B1AFAF" transform="matrix(1 0 0 -1 2.179 1.876)"/>
            <circle r="1.779" fill="#B1AFAF" transform="matrix(1 0 0 -1 9.293 1.876)"/>
            <circle r="1.779" fill="#B1AFAF" transform="matrix(1 0 0 -1 16.408 1.876)"/>
        </svg>
    );
};

export default PopupMenu;