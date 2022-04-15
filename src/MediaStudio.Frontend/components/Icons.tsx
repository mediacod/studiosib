import React from 'react';

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
        <></>
    )
}

export default Icons;
