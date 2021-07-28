import React, { useEffect } from 'react';
import styles from '../../styles/AlbumPage.module.scss'
import MainLayout from "../../layout/MainLayout";
import Track from "../../components/track";
import MobileHeader from "../../components/nav/MobileHeaderNav";
import useMobileDetect from "../../hooks/useUserAgent";
import { useActions } from '../../hooks/useActions';
import { useTypedSelector } from '../../hooks/useTypedSelector';
import { useRouter } from 'next/router';
import { setQueue } from '../../store/action-creators/player';
import {useQueue} from "../../hooks/useQueue";

const PlaylistPage: React.FC = () => {

    const { isMobile } = useMobileDetect();

    const router = useRouter()
    const { id } = router.query;

    const { getPlaylistPage, setQueue, playTrack, pauseTrack, setActive } = useActions()
    const {playHandler} = useQueue()

    useEffect(() => {
        getPlaylistPage(Number(id))
    }, [])

    const { albumPage } = useTypedSelector(state => state.albumPage);
    const { pause, active, idAlbum, idType } = useTypedSelector(state => state.player);

    const play = (idTrack) => {
        playHandler(idTrack, albumPage, idAlbum, idType)
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
                    {albumPage.tracks?.map(track => <Track key={track.idTrack} track={track} isMobile={isMobile} play={play} isPlay={!pause && track.idTrack === active?.idTrack} />)}
                </div>
            </div>
        </MainLayout>
    );
};

export default React.memo(PlaylistPage);
