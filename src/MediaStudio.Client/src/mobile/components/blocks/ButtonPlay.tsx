import React from 'react';
import {Pause, Play} from "../../../components/icons";

interface IProps {
    playing: boolean;
    color?: string;
    onClick?: any
    size?: number
}

export const ButtonPlay: React.FC<IProps> = ({playing, color = '#333', size, onClick}) => {

    return (
        <div onClick={onClick}>
            {!playing
                ? <Play color={color} size={size}/>
                : <Pause color={color} size={size}/>}
        </div>
    );
};