import React from 'react';

interface Icons{
    name: string;
    color: string;
    size: string;
    className?: string;
    height?: string;
    width?: string;
}

const MIcons: React.FC<Icons> = ({name, color, size, className, height, width}): any => {

    return(<></>
    )
}

export default MIcons;
