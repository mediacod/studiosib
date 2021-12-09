import React from 'react';
import {IImageProps} from "../../types/common";

const Repeat:React.FC<IImageProps> = ({action, color, size, height, className}) => {
    return (
        <svg
            id="repeat"
            onClick={action}
            className={`${className}`}
            fill={color}
            width={size}
            height={height || size}
            viewBox="0 0 15 15"><defs/>
            <g clip-path="url(#clip0)" fill=""><path d="M14.863 2.95L12.051.138a.468.468 0 00-.8.33v1.407H5.156A5.156 5.156 0 000 7.031c0 .259.21.469.47.469h1.874c.259 0 .469-.21.469-.469a2.343 2.343 0 012.344-2.343h6.093v1.406a.469.469 0 00.8.332l2.813-2.813a.47.47 0 000-.663zM14.531 7.5h-1.875a.469.469 0 00-.469.468 2.343 2.343 0 01-2.343 2.344H3.75V8.906a.469.469 0 00-.8-.331L.137 11.387a.47.47 0 000 .663l2.813 2.812a.468.468 0 00.8-.332v-1.406h6.094A5.156 5.156 0 0015 7.968a.469.469 0 00-.469-.468z"/></g><defs><clipPath id="clip0"><path fill="#fff" d="M0 0h15v15H0z"/></clipPath></defs></svg>
    );
};

export default Repeat;