import React from 'react';
import {IImageProps} from "../../types/common";

const Download: React.FC<IImageProps> = ({action, color, size, height, className}) => {
    return (
        <svg
            id="download"
            onClick={action}
            className={`${className}`}
            fill={color}
            width={size}
            height={height || size}
            viewBox="0 0 17 16">
            <defs/>
            <g clip-path="url(#clip0)" fill="#4B4B4B">
                <path
                    d="M12.702 7.293A.533.533 0 0012.22 7h-2.125V.5c0-.276-.238-.5-.531-.5H7.438c-.294 0-.532.224-.532.5V7H4.781a.534.534 0 00-.483.293.476.476 0 00.084.536l3.719 4c.1.109.246.171.4.171a.548.548 0 00.399-.171l3.718-4a.476.476 0 00.084-.536z"/>
                <path
                    d="M14.552 11.13v3H2.864v-3H.74v4c0 .553.476 1 1.063 1h13.812c.588 0 1.063-.447 1.063-1v-4h-2.125z"/>
            </g>
            <defs>
                <clipPath id="clip0">
                    <path fill="#fff" d="M0 0h17v16H0z"/>
                </clipPath>
            </defs>
        </svg>
    );
};

export default Download;