import React from 'react';
import styles from '../../styles/AlbumPage.module.scss'
import MainLayout from "../../layout/MainLayout";
import Icons from "../../components/Icons";
import Track from "../../components/track";
import {ITrack} from "../../types/track";
import Card from '../../components/surfaces/Card';
import MobileHeader from "../../components/nav/MobileHeaderNav";
import useMobileDetect from "../../hooks/useUserAgent";

const AlbumPage: React.FC = () => {
    const album = {id: 2, title: 'Я знаю, Бог не ошибается', count: 10, url: 'http://studiosib.ru/storage/album_images/r6K1dlgNj8LNsn7A16qtw8ug67IZsiFJDcwdUk2r.png'}
    const tracks = [
        {idTrack: 1, link: 'http://msapi.ru:9000/audio/d33afd6bdcfa684b006701907a5143e7.mp3', duration: 123, name: 'Никогда, никогда, никогда' },
        {idTrack: 1, link: 'http://msapi.ru:9000/audio/d33afd6bdcfa684b006701907a5143e7.mp3', duration: 123, name: 'Синее небо' },
        {idTrack: 1, link: 'http://msapi.ru:9000/audio/d33afd6bdcfa684b006701907a5143e7.mp3', duration: 123, name: 'Тишина' },
        {idTrack: 1, link: 'http://msapi.ru:9000/audio/d33afd6bdcfa684b006701907a5143e7.mp3', duration: 123, name: 'В жизни земной, все приходит к концу' },
    ]

    const {isMobile} = useMobileDetect();

    return (
        <MainLayout>
            {isMobile && <MobileHeader />}
            <div className={styles.container}>
                <div className={styles.infoContainer}>
                        <img className={styles.cover} src={album.url}/>
                        <div className={styles.infoBlock}>
                            <div className={styles.titleContainer}>
                                <h1 className={styles.albumName}>{album.title}</h1>
                                <p className={styles.albumArtistName}>Сибирская студия</p>
                                <p className={styles.albumYear}>2011 год</p>
                            </div>
                            <div className={styles.buttonsContainer}></div>
                        </div>
                </div>
                <div className={styles.content}>
                    {tracks.map(track => <Track track={track} isMobile={isMobile}/>)}
                </div>
            </div>
        </MainLayout>
    );
};

export default AlbumPage;
