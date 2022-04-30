import React, {useContext, useState} from 'react';
import {LoginFormMobile} from "./form/LoginFormMobile";
import {SignUpFormMobile} from "./form/SignUpFormMobile";
import {CreateUserFormMobile} from "./form/CreateUserFormMobile";
import {AutoCenter, Button} from "antd-mobile";
import AuthContext from "../../../context/authContext";
import {useAuth} from "../../../hooks/auth.hook";

export const AuthFormSwitch = () => {

    const {user, isAuth} = useContext(AuthContext)

    const [haveAccount, setHaveAccount] = useState(true)

    const switchHandler = () => {
        setHaveAccount((prevState) => !prevState)
    }

    const getForm = () => {

        if(isAuth && !user?.idUser){
            return <CreateUserFormMobile />
        }

        if(haveAccount  && !isAuth){
            return <LoginFormMobile  />
        }else{
            return <SignUpFormMobile />
        }
    }

    return (
        <div>
            {getForm()}
            {!isAuth && <AutoCenter>
                <Button fill={"none"}
                    onClick={switchHandler}>
                    {haveAccount ? 'Создать новый' : 'У меня есть аккаунт'}
                </Button>
            </AutoCenter>}
        </div>
    );
};