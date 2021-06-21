import React, { useEffect } from 'react';
import styles from '../../styles/AlbumPage.module.scss'
import MainLayout from "../../layout/MainLayout";
import Icons from "../../components/Icons";
import Track from "../../components/track";
import { ITrack } from "../../types/track";
import Card from '../../components/surfaces/Card';
import MobileHeader from "../../components/nav/MobileHeaderNav";
import useMobileDetect from "../../hooks/useUserAgent";
import { useActions } from '../../hooks/useActions';
import { useTypedSelector } from '../../hooks/useTypedSelector';
import { useRouter } from 'next/router';
import { setQueue } from '../../store/action-creators/player';

const AlbumPage: React.FC = () => {

    const { isMobile } = useMobileDetect();

    const router = useRouter()
    const { id } = router.query;

    const { getAlbumPage, setQueue, playTrack, pauseTrack, setActive } = useActions()

    useEffect(() => {
        getAlbumPage(Number(id))
    }, [])

    const { albumPage } = useTypedSelector(state => state.albumPage);
    const { pause, active } = useTypedSelector(state => state.player);

    const newQueue = (active) => {
        setQueue({ queue: albumPage.tracks, idAlbum: albumPage.idAlbum, idType: 1 })
        setActive(active[0])
    }

    const play = (idTrack) => {

        if (pause) {
            playTrack()
        } else {
            pauseTrack()
        }

        const active = albumPage.tracks.filter(a => a.idTrack === idTrack)
        newQueue(active)
    }

    return (
        <MainLayout>
            {isMobile && <MobileHeader />}
            <div className={styles.container}>
                <div className={styles.infoContainer}>
                    <img className={styles.cover} src={albumPage.linkCover} />
                    <div className={styles.infoBlock}>
                        <div className={styles.titleContainer}>
                            <h1 className={styles.albumName}>{albumPage.name}</h1>
                            <p className={styles.albumArtistName}>Сибирская студия</p>
                            <p className={styles.albumYear}>2011 год</p>
                        </div>
                        <div className={styles.buttonsContainer}></div>
                    </div>
                </div>
                <div className={styles.content}>
                    {albumPage.tracks?.map(track => <Track track={track} isMobile={isMobile} play={play} isPlay={!pause && track.idTrack === active?.idTrack} />)}
                </div>
            </div>
        </MainLayout>
    );
};

export default AlbumPage;
