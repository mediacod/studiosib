import React from 'react';
import {Pause, Play} from "../../../components/icons";

interface IProps {
    playing: boolean;
    color?: string;
    onClick?: any
}

export const ButtonPlay: React.FC<IProps> = ({playing, color = '#333', onClick}) => {
    return (
        <div onClick={onClick}>
            {!playing
                ? <Play color={color} />
                : <Pause color={color} />}
        </div>
    );
};