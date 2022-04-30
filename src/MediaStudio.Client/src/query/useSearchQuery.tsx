import {useQuery} from "react-query";
import {searchAPI} from "../api/api";

export const useSearchQuery = (query: any) => {
    const {data} = useQuery(['search', query], ()=>searchAPI(query), {enabled: !!query})

    return {data}
}