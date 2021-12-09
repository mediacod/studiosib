import React from 'react';
import styles from "../../styles/Header.module.scss";
import {useRouter} from "next/router";
import FormSearch from "../forms/formSearch";
import PrevPage from "../icons/PrevPage";
import NextPage from "../icons/NextPage";
import Tg from "../icons/Tg";
import Yt from "../icons/Yt";

const Header: React.FC = () => {
    const router = useRouter()

    const targetTg = () => {
        router.push('https://t.me/studiosib_music', '_blank')
    }

    const targetYt = () => {
        router.push('https://www.youtube.com/channel/UCx9cXwpj_scHczd4AIfsfPg/featured', '_blank')
    }



    return (
        <div className={styles.header}>
            <div className={styles.navigation}>
                <div className={styles.navigationPrev} onClick={() => router.back()}>
                    <PrevPage className={styles.navigationIconPrev} color={'#3A2C51'} size={'11px'} height={'19px'}/>
                </div>
                <div className={styles.navigationNext} onClick={() =>{}}>
                    <NextPage className={styles.navigationIconNext} color={'#3A2C51'} size={'11px'} height={'19px'}/>
                </div>
            </div>
            <div className={styles.search} onClick={()=>{}}>
                <FormSearch />
            </div>
            <div className={styles.authorization}>
                <div className={styles.contact}>
                    <Tg action={targetTg} color={''} size={'30px'} className={styles.contactIcon}/>
                    <Yt action={targetYt} color={''} size={'30px'} className={styles.contactIcon}/>
                </div>
                <span className={styles.authorizationName}>Имя пользователя</span>
            </div>
        </div>
    );
};

export default Header;
