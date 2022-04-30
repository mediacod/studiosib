import React, {useContext, useEffect} from 'react';
import configContext from "../context/configContext";

export const usePageName = (name: string) => {

    const {setPageNameHandler} = useContext(configContext)


    useEffect(()=>{
        setPageNameHandler(name)
        return () => {setPageNameHandler('Studiosib')}
    }, [name])


};