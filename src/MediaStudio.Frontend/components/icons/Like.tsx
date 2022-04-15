import React from 'react';
import {IImageProps} from "../../types/common";

const Like:React.FC<IImageProps> = ({action, color, size, height, className}) => {
    return (
        <svg
            id="like"
            onClick={action}
            className={`${className}`}
            fill={color}
            width={size}
            height={height || size}
            viewBox="0 0 17 17"><defs/>
            <path d="M15.651 1.567C14.771.557 13.564 0 12.251 0c-.981 0-1.88.328-2.671.976-.4.327-.761.727-1.08 1.194A5.654 5.654 0 007.42.976C6.629.328 5.73 0 4.749 0c-1.313 0-2.52.557-3.4 1.567C.478 2.566 0 3.93 0 5.41c0 1.522.536 2.915 1.686 4.385 1.03 1.315 2.508 2.65 4.22 4.195.586.527 1.248 1.126 1.937 1.763a.965.965 0 00.657.261.965.965 0 00.657-.261c.688-.637 1.351-1.236 1.937-1.764 1.712-1.545 3.191-2.88 4.22-4.194C16.464 8.325 17 6.932 17 5.409c0-1.478-.479-2.843-1.349-3.842z"/>
        </svg>
    );
};

export default Like;