import React from 'react';
import {IImageProps} from "../../types/iconType";


export const Play: React.FC<IImageProps> = ({color = '#fff', action, size}) => {

    const height = size ? size : 15
    const width = size ? size : 15

    return (
        <svg onClick={action} width={width} height={height} viewBox="0 0 15 15" fill="none" xmlns="http://www.w3.org/2000/svg">
            <path d="M2.71764 0.330738C1.21679 -0.444979 0 0.190498 0 1.74898V13.2499C0 14.8099 1.21679 15.4446 2.71764 14.6696L13.874 8.90464C15.3753 8.12864 15.3753 6.87142 13.874 6.09561L2.71764 0.330738Z" fill={color}/>
        </svg>
    );
};