import {useQuery} from "react-query";
import {getSectionAPI, USER_FAVOURITES_TRACK, USER_HISTORY} from "../api/api";
import {useContext} from "react";
import AuthContext from "../context/authContext";

export const useUserFavoritesTrackQuery = () => {

    const {user}:any = useContext(AuthContext)
    const {data} = useQuery(['favoritesTrack'], ()=>USER_FAVOURITES_TRACK.GET(), {enabled: !!user?.idUser})

    const idFavoritesTracks = data?.data?.map((track: any) => track.idTrack)

    return {data: data?.data, idFavoritesTracks}
}