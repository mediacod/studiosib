import React from 'react';
import {IImageProps} from "../../types/iconType";

const Password: React.FC<IImageProps> = ({action, color, size, height, className}) => {
    return (
        <svg
            id="password"
            onClick={action}
            className={`${className}`}
            fill={color}
            width={size}
            height={height || size}
            viewBox="0 0 35 35">
                <rect width="35" height="35" fill="none"/>
                <path d="M23.875 15.5H23.1667V13C23.1667 10.2425 20.6252 8 17.5 8C14.3748 8 11.8333 10.2425 11.8333 13V15.5H11.125C9.95389 15.5 9 16.3408 9 17.375V26.125C9 27.1592 9.95389 28 11.125 28H23.875C25.0461 28 26 27.1592 26 26.125V17.375C26 16.3408 25.0461 15.5 23.875 15.5ZM13.7222 13C13.7222 11.1617 15.4166 9.66667 17.5 9.66667C19.5834 9.66667 21.2778 11.1617 21.2778 13V15.5H13.7222V13ZM18.4444 21.935V23.8333C18.4444 24.2933 18.0223 24.6667 17.5 24.6667C16.9777 24.6667 16.5556 24.2933 16.5556 23.8333V21.935C15.9936 21.6458 15.6111 21.1142 15.6111 20.5C15.6111 19.5808 16.4583 18.8333 17.5 18.8333C18.5417 18.8333 19.3889 19.5808 19.3889 20.5C19.3889 21.1142 19.0064 21.6458 18.4444 21.935Z"/>
        </svg>
    );
};

export default Password;