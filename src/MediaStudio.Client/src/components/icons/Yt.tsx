import React from 'react';
import {IImageProps} from "../../types/iconType";

const Yt: React.FC<IImageProps> = ({action, color, size, height, className}) => {
    return (
        <svg
            id="yt"
            onClick={action}
            className={`${className}`}
            fill={color}
            width={size}
            height={height || size}
            viewBox="0 0 27 28">
            <circle cx="13.4558" cy="13.9681" r="13.2806" fill="#FF0000"/>
            <path
                d="M20.9655 10.2357C20.785 9.56463 20.2559 9.03569 19.585 8.85499C18.3592 8.51953 13.456 8.51953 13.456 8.51953C13.456 8.51953 8.55293 8.51953 7.32713 8.8422C6.66912 9.02278 6.12715 9.56475 5.94658 10.2357C5.62402 11.4613 5.62402 14.0033 5.62402 14.0033C5.62402 14.0033 5.62402 16.558 5.94658 17.7709C6.12727 18.4418 6.65622 18.9709 7.32725 19.1516C8.56584 19.487 13.4561 19.487 13.4561 19.487C13.4561 19.487 18.3592 19.487 19.585 19.1644C20.256 18.9838 20.785 18.4547 20.9657 17.7838C21.2882 16.558 21.2882 14.0162 21.2882 14.0162C21.2882 14.0162 21.3011 11.4613 20.9655 10.2357Z"
                fill="white"/>
            <path d="M12.0938 16.2812L16.171 13.9328L12.0938 11.5845V16.2812Z" fill="#FF0000"/>

        </svg>
    );
};

export default Yt;