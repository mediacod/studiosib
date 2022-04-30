import React, {useState} from 'react';
import {useUserFavoritesTrackQuery} from "../query/useFavoritesTrackQuery";

export const useConfig = () => {

    const [pageName, setPageName] = useState('Studiosib')
    const {idFavoritesTracks} = useUserFavoritesTrackQuery()

    const setPageNameHandler = (title = 'Studiosib') => {
        setPageName(title)
    }

    return {pageName, setPageNameHandler, idFavoritesTracks}
};