import React from 'react';
import {IImageProps} from "../../types/common";

const Name: React.FC<IImageProps> = ({action, color, size, height, className}) => {
    return (
        <svg
            id="name"
            onClick={action}
            className={`${className}`}
            fill={color}
            width={size}
            height={height || size}
            viewBox="0 0 35 35">
            <rect width="35" height="35" fill="none"/>
            <path
                d="M22.1099 11.7674C22.1099 14.4004 19.9754 16.5347 17.3425 16.5347C14.7097 16.5347 12.5752 14.4004 12.5752 11.7674C12.5752 9.1345 14.7097 7 17.3425 7C19.9754 7 22.1099 9.1345 22.1099 11.7674Z"/>
            <path
                d="M21.5139 19H13.4861C11.0126 19 9 21.3678 9 24.2777V28.1667C9 28.6267 9.31733 29 9.70833 29H25.2917C25.6827 29 26 28.6267 26 28.1667V24.2777C26 21.3678 23.9874 19 21.5139 19Z"/>
        </svg>
    );
};

export default Name;