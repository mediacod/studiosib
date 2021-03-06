import React from 'react';
import {IImageProps} from "../../types/iconType";

export const Books: React.FC<IImageProps> = ({action, color, size, height, className}) => {
    return (
        <svg
            id="books"
            onClick={action}
            className={`${className}`}
            fill={color}
            width={size}
            height={height || size}
            viewBox="0 0 35 35">
            <rect width="35" height="35" fill="none"/>
            <path
                d="M18.3778 9C12.1284 9 7 14.084 7 20.3333V25.6667C7 26.7695 7.89716 27.6667 9 27.6667H9.66667V21H9C8.76516 21 8.54289 21.0481 8.33333 21.1228V20.3333C8.33333 14.819 12.8635 10.3333 18.3778 10.3333C23.8921 10.3333 28.4222 14.819 28.4222 20.3333V21.1228C28.2127 21.0481 27.9904 21 27.7556 21H27.0889V27.6667H27.7556C28.8584 27.6667 29.7556 26.7695 29.7556 25.6667V20.3333C29.7556 14.084 24.6271 9 18.3778 9Z"/>
            <path
                d="M13 19.6665H12.3333C11.5969 19.6665 11 20.2634 11 20.9998V27.6665C11 28.4029 11.5969 28.9998 12.3333 28.9998H13C13.3682 28.9998 13.6667 28.7013 13.6667 28.3332V20.3332C13.6667 19.965 13.3682 19.6665 13 19.6665Z"/>
            <path
                d="M24.4232 19.6665H23.7565C23.3883 19.6665 23.0898 19.965 23.0898 20.3332V28.3332C23.0898 28.7013 23.3883 28.9998 23.7565 28.9998H24.4232C25.1596 28.9998 25.7565 28.4029 25.7565 27.6665V20.9998C25.7565 20.2634 25.1596 19.6665 24.4232 19.6665Z"/>
        </svg>
    );
};