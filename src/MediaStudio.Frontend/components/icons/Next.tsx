import React from 'react';
import {IImageProps} from "../../types/common";

const Next:React.FC<IImageProps> = ({action, color, size, height, className}) => {
    return (
        <svg
            id="next"
            onClick={action}
            className={`${className}`}
            fill={color}
            width={size}
            height={height || size}
            viewBox="0 0 18 12"><defs/>
            <path d="M17.463 5.898L7 11.796V0l10.463 5.898z" fill="#5F5A5A"/>
            <path d="M10.463 5.898L0 11.796V0l10.463 5.898z" fill="#5F5A5A"/>
        </svg>
    );
};

export default Next;