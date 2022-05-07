import React from 'react';
import {IImageProps} from "../../types/iconType";

const Change:React.FC<IImageProps> = ({action, color, size, className}) => {

    const height = size ? size/1.07 : 13
    const width = size ? size : 14

    return (
        <svg width={width} height={height} viewBox="0 0 14 13" fill="none" xmlns="http://www.w3.org/2000/svg">
            <path d="M4.31338 4.99153L5.16541 5.96525L6.22867 4.75014L5.51788 3.93788C4.75774 3.06871 3.65995 2.5708 2.50624 2.5708H0.757324V4.17157H2.50624C3.19837 4.1716 3.85727 4.4702 4.31338 4.99153V4.99153Z" fill={color}/>
            <path d="M11.1624 8.97364H11.0143C10.3222 8.97364 9.66326 8.67506 9.20718 8.1537L7.82351 6.57245L9.20718 4.99121C9.66326 4.46984 10.3222 4.17127 11.0143 4.17127H11.1624V5.77204L13.5636 3.37081L11.1624 0.969727V2.5705H11.0143C9.86064 2.5705 8.76282 3.06839 8.00268 3.93758L4.31336 8.1537C3.85727 8.67504 3.19835 8.97364 2.50622 8.97364H0.757324V10.5744H2.50624C3.65992 10.5744 4.75774 10.0765 5.51788 9.20733L6.76029 7.78749L8.0027 9.20733C8.76285 10.0765 9.86064 10.5744 11.0143 10.5744H11.1625V12.1752L13.5636 9.77405L11.1624 7.37286V8.97364Z" fill={color}/>
        </svg>
    );
};

export default Change;