import React from 'react';
import {IImageProps} from "../../types/iconType";

const NextPage: React.FC<IImageProps> = ({action, color, size, height, className}) => {
    return (
        <svg
            id="nextPage"
            onClick={action}
            className={`${className}`}
            fill={color}
            width={size}
            height={height || size}
            viewBox="0 0 14 25">
            <defs/>
            <path
                d="M.937 25a.884.884 0 01-.656-.29c-.375-.388-.375-1.002 0-1.357L10.803 12.5.281 1.647C-.094 1.26-.094.646.281.291c.375-.388.968-.388 1.311 0L12.77 11.82c.188.194.281.42.281.679 0 .258-.093.517-.28.678L1.591 24.71A.885.885 0 01.937 25z"
                fill="#3A2C51"/>
        </svg>
    );
};

export default NextPage;