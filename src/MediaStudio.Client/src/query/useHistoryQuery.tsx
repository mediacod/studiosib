import {useQuery} from "react-query";
import {getSectionAPI, USER_HISTORY} from "../api/api";
import authContext from "../context/authContext";
import {useContext} from "react";
import AuthContext from "../context/authContext";

export const useUserHistoryQuery = () => {

    const {user}:any = useContext(AuthContext)
    const {data} = useQuery(['historyTrack'], ()=>USER_HISTORY.GET(), {enabled: !!user?.idUser})

    return {data: data?.data}
}