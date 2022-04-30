import {useQuery} from "react-query";
import {getSectionAPI} from "../api/api";

export const useSectionQuery = (idPage: any) => {
    const {data} = useQuery(['section', idPage], ()=>getSectionAPI(idPage), {enabled: !!idPage})

    return {data}
}