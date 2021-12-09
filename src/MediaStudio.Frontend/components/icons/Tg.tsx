import React from 'react';
import {IImageProps} from "../../types/common";

const Tg: React.FC<IImageProps> = ({action, color, size, height, className}) => {
    return (
        <svg
            id="home"
            onClick={action}
            className={`${className}`}
            fill={color}
            width={size}
            height={height || size}
            viewBox="0 0 28 27">
            <g clip-path="url(#clip0)">
                <path
                    d="M14.0214 26.5886C21.2905 26.5886 27.1833 20.6958 27.1833 13.4266C27.1833 6.15747 21.2905 0.264648 14.0214 0.264648C6.75219 0.264648 0.859375 6.15747 0.859375 13.4266C0.859375 20.6958 6.75219 26.5886 14.0214 26.5886Z"
                    fill="#039BE5"/>
                <path
                    d="M6.88212 13.1413L19.5725 8.24836C20.1615 8.03558 20.6759 8.39205 20.485 9.28267L20.4861 9.28158L18.3254 19.4613C18.1652 20.183 17.7364 20.3585 17.1364 20.0185L13.8459 17.5934L12.2588 19.1223C12.0833 19.2978 11.9352 19.4459 11.5952 19.4459L11.8288 16.0973L17.9272 10.5879C18.1926 10.3543 17.868 10.2227 17.5181 10.4552L9.98176 15.2001L6.73295 14.1866C6.02768 13.9629 6.01233 13.4813 6.88212 13.1413Z"
                    fill="white"/>
            </g>
            <defs>
                <clipPath id="clip0">
                    <rect width="26.324" height="26.324" fill="white" transform="translate(0.859375 0.264648)"/>
                </clipPath>
            </defs>
        </svg>
    );
};

export default Tg;