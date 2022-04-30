import {useCallback, useEffect, useState} from "react";
import {Login, refreshAPI, USER_API} from "../api/api";
import jwtDecode from "jwt-decode";
import {LocalStorageService} from "../utils/LocalStorageService";

export interface ITypeAuth {
    login: ()=>void;
    logout: ()=>void;
    token: string;
    email: string;
    ready: boolean;
    isAuth: boolean;
    auth: ()=>void;
}

export const useAuth = () => {
    const [token, setToken] = useState(null)
    const [login, setLogin] = useState(null)
    const [user, setUser] = useState(null)
    const [ready, setReady] = useState(false)
    const [isAuth, setIsAuth] = useState(false)

    const auth = async ({login, password}: any) => {
        if(token) {
            await logout()
        }

        try {
            const response = await Login({login, password})
            signIn(response)
            return true
        }catch (e){
            return false
        }
    }

    const signIn = useCallback( (data: any) => {

        const user: any = jwtDecode(data.accessToken)
        setToken(data.accessToken)
        setLogin(user?.unique_name)
        LocalStorageService.setToken(data)

        getUser()
    }, [])

    const logout = useCallback( async () => {

        await LocalStorageService.clearToken()
        setToken(null)
        setLogin(null)

    }, [])

    const getUser = async () => {
        try {
            const {data} = await USER_API.GET()
            setUser(data)
        }catch (e) {
            logout()
        }
    }

    const checkToken = async () => {
        const tokens = LocalStorageService.getToken()
        if(tokens?.accessToken && tokens?.refreshToken) {
            try {
                const newTokens = await refreshAPI()
                signIn(newTokens)
                await getUser()
            }catch (e) {
                await logout()
            }

        }
        setReady(true)
    }

    useEffect( () => {
        checkToken()
    }, [])

    useEffect(()=> {
        setIsAuth(!!token)
    }, [token, setIsAuth])

    const data: any = {
        signIn,
        logout,
        token,
        login,
        ready,
        isAuth,
        user,
        auth,
        getUser
    }

    return data
}