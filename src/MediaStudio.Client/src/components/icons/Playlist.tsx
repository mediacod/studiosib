import React from 'react';
import {IImageProps} from "../../types/iconType";

const Playlist: React.FC<IImageProps> = ({action, color, size, height, className}) => {
    return (
        <svg
            id="playlist"
            onClick={action}
            className={`${className}`}
            fill={color}
            width={size}
            height={height || size}
            viewBox="0 0 35 35">
            <rect width="35" height="35" fill="none"/>
            <path
                d="M25.06 12.9406C24.1769 12.4729 23.5825 11.5964 23.4731 10.6008C23.4218 10.1025 22.9851 9.73527 22.487 9.77179C21.9922 9.8254 21.6202 10.2497 21.6305 10.7487V21.9286C21.053 21.653 20.421 21.5114 19.7817 21.5144C17.7387 21.5144 16.084 22.8991 16.084 24.6053C16.084 26.3113 17.7387 27.696 19.7817 27.696C21.6982 27.696 23.2758 26.4752 23.4607 24.9144C23.4731 24.8118 23.4792 24.7085 23.4792 24.6053V15.3581C23.481 15.1281 23.608 14.9175 23.8102 14.809C24.0122 14.7006 24.2574 14.7115 24.4491 14.8376L24.4652 14.8476C25.0722 15.2154 25.7225 15.5522 26.1199 16.2106C27.1367 17.9044 26.2093 19.932 25.2048 21.3939C25.0313 21.6445 25.0301 21.9766 25.2016 22.2284H25.2048C25.3384 22.426 25.5597 22.5458 25.7977 22.5495C26.0358 22.5532 26.2607 22.4403 26.4003 22.247C27.941 20.1391 29.2782 17.3048 27.7007 14.9249C27.0689 13.9667 26.0275 13.4752 25.06 12.9406Z"/>
            <path
                d="M6.38729 7H17.976C18.1893 7 18.3623 7.17047 18.3623 7.3807V9.28402C18.3623 9.49425 18.1893 9.66472 17.976 9.66472H6.38729C6.17396 9.66472 6.00098 9.49425 6.00098 9.28402V7.3807C6.00098 7.17047 6.17396 7 6.38729 7Z"/>
            <path
                d="M6.38631 11.5894H17.975C18.1883 11.5894 18.3613 11.7646 18.3613 11.9806V13.937C18.3613 14.1531 18.1883 14.3281 17.975 14.3281H6.38631C6.17299 14.3281 6 14.1531 6 13.937V11.9806C6 11.7646 6.17299 11.5894 6.38631 11.5894Z"/>
            <path
                d="M6.38631 16.2522H17.975C18.1883 16.2522 18.3613 16.4272 18.3613 16.6435V18.5997C18.3613 18.8157 18.1883 18.9909 17.975 18.9909H6.38631C6.17299 18.9909 6 18.8157 6 18.5997V16.6435C6 16.4272 6.17299 16.2522 6.38631 16.2522Z"/>
        </svg>
    );
};

export default Playlist;