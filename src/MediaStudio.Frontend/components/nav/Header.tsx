import React from 'react';
import styles from "../../styles/Header.module.scss";
import {useRouter} from "next/router";
import Icons from "../Icons";
import {useActions} from "../../hooks/useActions";
import Link from 'next/link';

const Header: React.FC = () => {
    const router = useRouter()
    const pathname = router.pathname

    const {getDataSearch} = useActions()

    const clickSearchHandler = () => {
        if(pathname != '/search'){
            router.push('/search')
        }
    }

    const searchHandler = (e) => {
        const value = e.target.value
        if(value.length > 0){
            getDataSearch(value)
        }
    }

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
                    <Icons name={'prevPage'} className={styles.navigationIconPrev} color={'#3A2C51'} size={'11px'} height={'19px'}/>
                </div>
                <div className={styles.navigationNext} onClick={() =>{}}>
                    <Icons name={'nextPage'} className={styles.navigationIconNext} color={'#3A2C51'} size={'11px'} height={'19px'}/>
                </div>
            </div>
            <div className={styles.search} onClick={()=>{}}>
                <form action="#">
                    <input type="search" placeholder="ПОИСК" onChange={searchHandler} onClick={clickSearchHandler} />
                    <button type="submit"/>
                </form>
            </div>
            <div className={styles.authorization}>
                <div className={styles.contact}>
                    <Icons action={targetTg} name={'tg'} color={''} size={'30px'} className={styles.contactIcon}/>
                    <Icons action={targetYt} name={'yt'} color={''} size={'30px'} className={styles.contactIcon}/>
                </div>
                <span className={styles.authorizationName}>Имя пользователя</span>
            </div>
        </div>
    );
};

export default Header;
