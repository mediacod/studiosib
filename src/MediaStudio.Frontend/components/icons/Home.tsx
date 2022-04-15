import React from 'react';
import {IImageProps} from "../../types/common";

const Home: React.FC<IImageProps> = ({action, color, size, height, className}) => {
    return (
        <svg
            id="home"
            onClick={action}
            className={`${className}`}
            fill={color}
            width={size}
            height={height || size}
            viewBox="0 0 35 35">
            <rect width="35" height="35" fill="none"/>
            <path d="M28.6512 13.9447L18.4759 4.19122C18.2102 3.93626 17.7902 3.93626 17.5245 4.19122L7.33923 13.9547C7.12371 14.1703 7 14.4672 7 14.7707C7 15.4023 7.51429 15.9166 8.14591 15.9166H9.75006V24.625C9.75006 25.384 10.3661 26 11.1251 26H15.0211C15.4006 26 15.7086 25.692 15.7086 25.3125V19.3541C15.7086 19.2284 15.8111 19.1249 15.9377 19.1249H20.0628C20.1883 19.1249 20.2919 19.2284 20.2919 19.3541V25.3125C20.2919 25.692 20.5999 26 20.9794 26H24.8754C25.6344 26 26.2504 25.384 26.2504 24.625V15.9166H27.8545C28.4862 15.9166 29.0005 15.4023 29.0005 14.7707C29.0005 14.4672 28.8768 14.1703 28.6512 13.9447Z"/>
        </svg>
    );
};

export default Home;