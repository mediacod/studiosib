import React, {useEffect, useState} from 'react';
import MainLayout from "../layout/MainLayout";
import SectionLayout from "../layout/SectionLayout";
import {useTypedSelector} from "../hooks/useTypedSelector";

import Section from "../components/sections/Section";
import styles from "../styles/Search.module.scss";
import Track from "../components/track";
import useMobileDetect from "../hooks/useUserAgent";
import {useQueue} from "../hooks/useQueue";
import cover from '../public/static/images/coverDefault.png';
import FormSearch from "../components/forms/formSearch";

const Search = () => {

    const [randomId, setRandomId] = useState(null)
    const [status, setStatus] = useState(null)
    const {data} = useTypedSelector(state => state.search);
    const {isMobile} = useMobileDetect();
    const {playHandler} = useQueue()

    useEffect(() => {
        setRandomId(Math.floor(Math.random() * Math.floor(-1000)))
        setStatus(!data?.tracks?.length && !data?.albums?.length && !data?.perfomers?.length && !data?.playlists?.length)
    }, [data])

    const {
        pause,
        active,
        idType
    } = useTypedSelector((state) => state.player);

    const searchDataTrack = {
        tracks: data?.tracks || [],
        idAlbum: randomId,
        linkCover: cover,
        idType: 3
    }

    const play = (idTrack) => {
        playHandler(idTrack, searchDataTrack, randomId, idType)
    }

    return (
        <MainLayout>
            <SectionLayout>
                {isMobile && <FormSearch />}
                {status && <p className={styles.noResultTitle}>Ничего не найдено...</p>}
                {data?.albums.length > 0 &&
                <Section key={'Альбомы'} title={'Альбомы'} cells={data.albums} idTypeCells={1}/>}
                {data?.playlists.length > 0 &&
                <Section key={'Плейлисты'} title={'Плейлисты'} cells={data.playlists} idTypeCells={2}/>}
                {data?.tracks.length > 0 &&
                <div className={styles.containerSearch}>
                    <div className={styles.content}>
                        {data?.tracks.map(track => <Track key={track.idTrack} track={track} isMobile={isMobile}
                                                          play={play}
                                                          isPlay={!pause && track.idTrack === active?.idTrack}/>)}
                    </div>
                </div>}
            </SectionLayout>
        </MainLayout>
    );
};

export default Search;