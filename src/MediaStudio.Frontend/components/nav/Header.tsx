import React from 'react';
import styles from "../../styles/Header.module.scss";
import {useRouter} from "next/router";
import Icons from "../Icons";

const Header: React.FC = () => {
    const router = useRouter()

    return (
        <div className={styles.header}>
            <div className={styles.navigation}>
                <div className={styles.navigationPrev} onClick={() => router.back()}>
                    <Icons name={'prevPage'} className={styles.navigationIconPrev} color={'#3A2C51'} size={'11px'} height={'19px'}/>
                </div>
                <div className={styles.navigationNext} onClick={() =>{}}>
                    <Icons name={'nextPage'} className={styles.navigationIconNext} color={'#3A2C51'} size={'11px'} height={'19px'}/>
                </div>
            </div>
            <div className={styles.search} onClick={()=>{}}>
                <form action="#">
                    <input type="search" placeholder="ПОИСК" onChange={()=>{}}/>
                    <button type="submit"/>
                </form>
            </div>
            <div className={styles.authorization}>
                <div className={styles.contact}>
                    <Icons name={'tg'} color={''} size={'30px'} className={styles.contactIcon}/>
                    <Icons name={'yt'} color={''} size={'30px'} className={styles.contactIcon}/>
                </div>
                <span className={styles.authorizationName}>Имя пользователя</span>
            </div>
        </div>
    );
};

export default Header;
