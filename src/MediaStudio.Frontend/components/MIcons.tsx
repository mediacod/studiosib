import React from 'react';
import IconsSVG from '../public/mobileSprite.svg';

interface Icons{
    name: string;
    color: string;
    size: string;
    className?: string;
    height?: string;
    width?: string;
}

const MIcons: React.FC<Icons> = ({name, color, size, className, height, width}): any => {

    return(
        <svg className={`icon icon-${name} ${className}`} fill={color} stroke={'none'} width={width || size} height={height || size}>
            <use xlinkHref={`${IconsSVG}#${name}`} />
        </svg>
    )
}

export default MIcons;
