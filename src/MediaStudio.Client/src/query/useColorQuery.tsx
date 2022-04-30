import {useQuery} from "react-query";
import {ColorAPI} from "../api/api";

export const useColorQuery = () => {

    const {data} = useQuery(['color'], ()=>ColorAPI(), {staleTime: 0, refetchOnWindowFocus: false})

    return {data}
}