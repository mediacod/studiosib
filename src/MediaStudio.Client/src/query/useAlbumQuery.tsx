import {useQuery} from "react-query";
import {AlbumAPI} from "../api/api";

export const useAlbumQuery = (idObject: any, type: any) => {

    const enabled = !!idObject && !!type

    const {data} = useQuery([type, idObject], ()=>AlbumAPI(idObject, type), {enabled})

    return {data}
}