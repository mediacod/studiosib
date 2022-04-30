import React, {useEffect} from 'react';
import { useNavigate } from "react-router-dom";

export const NoMatch = () => {
    let navigate = useNavigate();

    useEffect(()=>{
        navigate('/')
    }, [])

    return (
        <div>
            страница не найдена
        </div>
    );
};