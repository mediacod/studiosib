import {useTypedSelector} from "./useTypedSelector";
import {useActions} from "./useActions";
import {useEffect} from "react";

export const useGetPage = (id: number) => {
    const {getPage} = useActions()

    useEffect(()=> {
        getPage(id)
    }, [])
}
