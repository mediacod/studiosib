import React, {useEffect, useState } from 'react';
import {NextPage} from 'next'
import dynamic from 'next/dynamic'
import styles from "../styles/MainLayout.module.scss";
import Sidebar from "../components/nav/Sidebar";
import Header from "../components/nav/Header";
import Player from '../components/player/Player';
import useMobileDetect from '../hooks/useUserAgent';
import NavMobile from '../components/nav/navMobile';


const MainLayout = ({children}) => {

    const {isMobile} = useMobileDetect();

    if (isMobile) {
        return (
            <div className={styles.mainMobile}>
                <div className={styles.content}>
                    {children}
                </div>
                <Player/>
                <NavMobile />
            </div>
        )
    }

    return (
        <div className={styles.main}>
            {!isMobile && <Header/>}
            {!isMobile && <Sidebar/>}
            <div className={styles.content}>
                {children}
            </div>
            <Player/>
        </div>
    );
};

export default MainLayout;
