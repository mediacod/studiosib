import {usePageName} from "../../hooks/pageName.hook";
import {SignUpFormMobile} from "../components/auth/form/SignUpFormMobile";

export const SignUpPageMobile = () => {

    usePageName('Создать акканут')
    return <SignUpFormMobile />
};