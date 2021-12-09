import React from 'react';
import {IImageProps} from "../../types/common";

const PrevPage: React.FC<IImageProps> = ({action, color, size, height, className}) => {
    return (
        <svg
            id="prevPage"
            onClick={action}
            className={`${className}`}
            fill={color}
            width={size}
            height={height || size}
            viewBox="0 0 14 25">
            <defs/>
            <path
                d="M12.725 25c.25 0 .468-.097.656-.29.374-.388.374-1.002 0-1.357L2.858 12.5 13.381 1.647c.374-.387.374-1.001 0-1.356-.375-.388-.968-.388-1.312 0L.891 11.82a.945.945 0 00-.28.679c0 .258.093.517.28.678L12.07 24.71c.188.194.406.291.656.291z"
                fill="#3A2C51"/>
        </svg>
    );
};

export default PrevPage;