import React from 'react';
import {IImageProps} from "../../types/common";

const Mute: React.FC<IImageProps> = ({action, color, size, height, className}) => {
    return (
        <svg
            id="mute"
            onClick={action}
            className={`${className}`}
            fill={color}
            width={size}
            height={height || size}
            viewBox="0 0 18 18">
            <defs/>
            <g clip-path="url(#clip0)" fill="#5F5A5A">
                <path
                    d="M10.116.644a.574.574 0 00-.614.07l-5.644 4.56H1.16C.52 5.275 0 5.802 0 6.448v4.689c0 .647.521 1.172 1.16 1.172h2.698l5.643 4.56a.582.582 0 00.615.07.585.585 0 00.329-.528V1.172a.588.588 0 00-.329-.528zM13.382 4.647a.578.578 0 00-.82.004.592.592 0 00.004.83 4.635 4.635 0 011.36 3.31c0 1.26-.483 2.436-1.36 3.31a.59.59 0 00-.005.829.575.575 0 00.82.004 5.793 5.793 0 001.706-4.143 5.803 5.803 0 00-1.705-4.144z"/>
                <path
                    d="M15.02 2.995a.577.577 0 00-.822.004.592.592 0 00.003.828 6.974 6.974 0 012.046 4.964c0 1.88-.726 3.642-2.046 4.963a.593.593 0 00-.003.83.58.58 0 00.822.002 8.136 8.136 0 002.388-5.795 8.141 8.141 0 00-2.389-5.796z"/>
            </g>
            <defs>
                <clipPath id="clip0">
                    <path fill="#fff" d="M0 0h17.408v17.582H0z"/>
                </clipPath>
            </defs>
        </svg>

    );
};

export default Mute;