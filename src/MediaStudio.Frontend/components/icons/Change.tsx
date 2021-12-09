import React from 'react';
import {IImageProps} from "../../types/common";

const Change:React.FC<IImageProps> = ({action, color, size, height, className}) => {
    return (
        <svg
            id="next"
            onClick={action}
            className={`${className}`}
            fill={color}
            width={size}
            height={height || size}
            viewBox="0 0 18 12">
            <defs/>
            <g clip-path="url(#clip0)">
                <path
                    d="M4.998 6.777l1.198 1.37L7.69 6.437l-.999-1.141a5.623 5.623 0 00-4.233-1.922H0v2.25h2.458c.973 0 1.9.42 2.54 1.152z"/>
                <path
                    d="M14.625 12.375h-.208c-.973 0-1.9-.42-2.54-1.152L9.932 9l1.945-2.223a3.375 3.375 0 012.54-1.152h.208v2.25L18 4.5l-3.375-3.375v2.25h-.208c-1.622 0-3.165.7-4.233 1.921l-5.186 5.926a3.375 3.375 0 01-2.54 1.153H0v2.25h2.458c1.622 0 3.165-.7 4.233-1.921l1.747-1.996 1.746 1.995a5.623 5.623 0 004.233 1.922h.208v2.25L18 13.5l-3.375-3.375v2.25z"/>
            </g>
            <defs>
                <clipPath id="clip0">
                    <path fill="#fff" d="M0 0h18v18H0z"/>
                </clipPath>
            </defs>
        </svg>
    );
};

export default Change;