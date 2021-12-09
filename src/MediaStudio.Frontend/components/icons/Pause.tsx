import React from 'react';
import {IImageProps} from "../../types/common";

const Pause: React.FC<IImageProps> = ({action, color, size, height, className}) => {
    return (
        <svg
            id="pause"
            onClick={action}
            className={`${className}`}
            fill={color}
            width={size}
            height={height || size}
            viewBox="0 0 29 31">
            <defs/>
            <g filter="url(#filter0_d)">
                <path fill="#fff" d="M7 3h5v17H7z"/>
            </g>
            <g filter="url(#filter1_d)">
                <path fill="#fff" d="M16.148 3h5v17h-5z"/>
            </g>
            <defs>
                <filter id="filter0_d" x="0" y="0" width="19" height="31" filterUnits="userSpaceOnUse"
                        color-interpolation-filters="sRGB">
                    <feFlood flood-opacity="0" result="BackgroundImageFix"/>
                    <feColorMatrix in="SourceAlpha" values="0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 127 0"/>
                    <feOffset dy="4"/>
                    <feGaussianBlur stdDeviation="3.5"/>
                    <feColorMatrix values="0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0.15 0"/>
                    <feBlend in2="BackgroundImageFix" result="effect1_dropShadow"/>
                    <feBlend in="SourceGraphic" in2="effect1_dropShadow" result="shape"/>
                </filter>
                <filter id="filter1_d" x="9.148" y="0" width="19" height="31" filterUnits="userSpaceOnUse"
                        color-interpolation-filters="sRGB">
                    <feFlood flood-opacity="0" result="BackgroundImageFix"/>
                    <feColorMatrix in="SourceAlpha" values="0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 127 0"/>
                    <feOffset dy="4"/>
                    <feGaussianBlur stdDeviation="3.5"/>
                    <feColorMatrix values="0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0.15 0"/>
                    <feBlend in2="BackgroundImageFix" result="effect1_dropShadow"/>
                    <feBlend in="SourceGraphic" in2="effect1_dropShadow" result="shape"/>
                </filter>
            </defs>
        </svg>
    );
};

export default Pause;