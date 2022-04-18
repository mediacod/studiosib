import React from 'react';
import {IImageProps} from "../../types/common";

const History: React.FC<IImageProps> = ({action, color, size, height, className}) => {
    return (
        <svg
            id="history"
            onClick={action}
            className={`${className}`}
            fill={color}
            width={size}
            height={height || size}
            viewBox="0 0 35 35">
            <rect width="35" height="35" fill="none"/>
            <path
                d="M17.9999 6C11.3832 6 6 11.3832 6 17.9999C6 24.6167 11.3831 30 17.9998 30C24.6166 30 29.9997 24.6167 29.9997 17.9999C29.9998 11.3832 24.6167 6 17.9999 6ZM17.9999 28.3573C12.289 28.3573 7.64282 23.7109 7.64282 17.9999C7.64271 12.289 12.289 7.64271 17.9999 7.64271C23.7108 7.64271 28.3571 12.289 28.3571 17.9999C28.3571 23.711 23.7108 28.3573 17.9999 28.3573Z"/>
            <path
                d="M24.1917 17.8396H18.4913V11.6124C18.4913 11.1588 18.1237 10.791 17.67 10.791C17.2164 10.791 16.8486 11.1588 16.8486 11.6124V18.6609C16.8486 19.1145 17.2164 19.4823 17.67 19.4823H24.1917C24.6454 19.4823 25.013 19.1145 25.013 18.6609C25.013 18.2073 24.6453 17.8396 24.1917 17.8396Z"/>
        </svg>
    );
};

export default History;