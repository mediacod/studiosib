import {useQuery} from "react-query";
import {getSectionAPI, USER_FAVOURITES_PLAYLIST, USER_FAVOURITES_TRACK, USER_HISTORY} from "../api/api";
import {useContext} from "react";
import AuthContext from "../context/authContext";

export const useUserFavoritesPlaylistQuery = () => {

    const {user}:any = useContext(AuthContext)
    const {data} = useQuery(['favoritesPlaylist'], ()=>USER_FAVOURITES_PLAYLIST.GET(), {enabled: !!user?.idUser})

    return {data: data?.data}
}