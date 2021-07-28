import React from 'react';
import IconsSVG from '../public/sprite.svg';

interface Icons {
    name: string;
    color: string;
    size: string;
    className?: string;
    height?: string;
    width?: string;
    action?: any;
}

const Icons: React.FC<Icons> = ({ name, color, size, className, height, width, action}): any => {

    return (
        <svg onClick={action} className={`icon icon-${name} ${className}`} fill={color} stroke={'none'} width={width || size} height={height || size}>
            <use xlinkHref={`${IconsSVG}#${name}`} />
        </svg>
    )
}

export default Icons;
