import React from 'react';
import {IImageProps} from "../../types/common";

const Play: React.FC<IImageProps> = ({action, color, size, height, className}) => {
    return (
        <svg
            id="play"
            onClick={action}
            className={`${className}`}
            fill={color}
            width={size}
            height={height || size}
            viewBox="0 0 23 26">
            <defs/>
            <g filter="url(#filter0_d)">
                <path d="M19 9L4 18V0l15 9z" fill="#fff"/>
            </g>
            <defs>
                <filter id="filter0_d" x="0" y="0" width="23" height="26" filterUnits="userSpaceOnUse"
                        color-interpolation-filters="sRGB">
                    <feFlood flood-opacity="0" result="BackgroundImageFix"/>
                    <feColorMatrix in="SourceAlpha" values="0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 127 0"/>
                    <feOffset dy="4"/>
                    <feGaussianBlur stdDeviation="2"/>
                    <feColorMatrix values="0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0.25 0"/>
                    <feBlend in2="BackgroundImageFix" result="effect1_dropShadow"/>
                    <feBlend in="SourceGraphic" in2="effect1_dropShadow" result="shape"/>
                </filter>
            </defs>
        </svg>
    );
};

export default Play;