import React from 'react';
import {usePageName} from "../../hooks/pageName.hook";
import {LoginFormMobile} from "../components/auth/form/LoginFormMobile";

export const LoginPageMobile = () => {

    usePageName('авторизация')
    return <LoginFormMobile />
};