import React from 'react';
import MainLayout from "../layout/MainLayout";
import styles from "../styles/Queue.module.scss";
import Track from "../components/track";
import {useTypedSelector} from "../hooks/useTypedSelector";
import useMobileDetect from "../hooks/useUserAgent";
import {useQueue} from "../hooks/useQueue";

const Queue = () => {

    const { isMobile } = useMobileDetect();
    const {playHandler} = useQueue()
    const { albumPage } = useTypedSelector(state => state.albumPage);

    const {
        pause,
        active,
        queue,
        idAlbum,
        idType
    } = useTypedSelector((state) => state.player);

    const play = (idTrack) => {
        playHandler(idTrack, albumPage, idAlbum, idType)
    }

    const title = queue.length ? 'Очередь треков' : 'Очередь треков пуста'

    return (
        <MainLayout>
            <div className={styles.container}>
                <div className={styles.content}>
                    {queue?.map(track => <Track key={track.idTrack} track={track} isMobile={isMobile} play={play} isPlay={!pause && track.idTrack === active?.idTrack} />)}
                </div>
            </div>
        </MainLayout>
    );
};

export default Queue;
