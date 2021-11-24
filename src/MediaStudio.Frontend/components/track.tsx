import React, { useEffect, useState } from 'react';
import { ITrack } from "../types/track";
import styles from "../styles/Track.module.scss"
import Icons from "./Icons";
import { convertHMS } from '../utils/convertHMS';

interface Track {
    track: ITrack;
    isMobile?: boolean;
    play: any;
    isPlay: boolean;
}

const Track: React.FC<Track> = ({ track, isMobile, play, isPlay }) => {

    const [colorButton, setColorButton] = useState('#0064ac')

    useEffect(() => {
        setColorButton(isPlay ? '#0064ac' : '#B1AFAF')
    }, [isPlay])

    let activeStyle = isPlay ? styles.active : ''

    const isOpenMenu = false;
    return (
        <div className={activeStyle}>
            <div className={styles.container} onClick={() => play(track.idTrack)} >
                {!isMobile && <div className={styles.play}>
                    <Icons name={'playItem'} size={'20px'} color={colorButton} />
                </div>}
                <p className={styles.name}>{track.name}</p>
                <Icons className={styles.like} name={'like'} color={'#B1AFAF'} size={'20px'} />
                {!isMobile && <span className={styles.duration}>{track.duration ? convertHMS(track.duration) : null}</span>}
                <div className={styles.popupMenu}>
                    <Icons name={'popupMenu'} color={'#B1AFAF'} size={isMobile ? '13px' : '20px'} />
                </div>


                {isOpenMenu && <>
                    <div className='playlist__menu-background' />
                    <div className='playlist__menu-dropdown'>
                        <a className='download' href={track.link} download='fe'>Скачать</a>
                        <a className='add-playlist' href="#">Добавить в плейлист</a>
                        <a className='add-turn' href="#">Добавить в очередь</a>
                    </div>
                </>}
            </div>
        </div>
    );
};

export default React.memo(Track);
