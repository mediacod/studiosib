import React from 'react';
import {IImageProps} from "../../types/iconType";

export const Favorite: React.FC<IImageProps> = ({action, color, size, height, className}) => {
    return (
        <svg
            id="password"
            onClick={action}
            className={`${className}`}
            fill={color}
            width={size}
            height={height || size}
            viewBox="0 0 35 35">
            <rect width="35" height="35" fill="none"/>
            <path d="M10.5742 11.6149L9.30718 3.83602C9.20618 3.21807 9.86518 2.75436 10.4102 3.05782L17.0002 6.70541L23.5903 3.05682C24.1303 2.75636 24.7953 3.21106 24.6933 3.83501L23.4263 11.6139L28.7863 17.1163C29.2113 17.552 28.9673 18.2911 28.3633 18.3833L20.9853 19.512L17.6782 26.5668C17.4312 27.0936 16.5682 27.0936 16.3212 26.5668L13.0152 19.513L5.63715 18.3843C5.03115 18.2911 4.78915 17.553 5.21415 17.1173L10.5742 11.6149Z"/>
        </svg>
    );
};