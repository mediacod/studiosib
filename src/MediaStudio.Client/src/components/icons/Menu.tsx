import React from 'react';
import {IImageProps} from "../../types/iconType";

const Menu: React.FC<IImageProps> = ({action, color, size, height, className}) => {
    return (
        <svg
            id="menu"
            onClick={action}
            className={`${className}`}
            fill={color}
            width={size}
            height={height || size}
            viewBox="0 0 35 35">
            <rect width="35" height="35" fill="none"/>
            <rect id="Rectangle 34" x="29.4844" y="10" width="2.96297" height="25.4843"
                  transform="rotate(90 29.4844 10)"
                  fill="#5F5A5A"/>
            <rect id="Rectangle 35" x="29.4844" y="16.5186" width="2.96297" height="25.4843"
                  transform="rotate(90 29.4844 16.5186)" fill="#5F5A5A"/>
            <rect id="Rectangle 36" x="29.4844" y="23.0371" width="2.96297" height="25.4843"
                  transform="rotate(90 29.4844 23.0371)" fill="#5F5A5A"/>
        </svg>
    );
};

export default Menu;