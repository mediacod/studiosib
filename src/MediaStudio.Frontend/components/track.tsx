import React from 'react';
import {ITrack} from "../types/track";
import styles from "../styles/Track.module.scss"
import Icons from "./Icons";

interface Track {
    track: ITrack;
    isMobile?: boolean;
}

const Track: React.FC<Track> = ({track, isMobile}) => {

    const isOpenMenu = false;
    return (
        <div>
            <div className={styles.container} >
                {!isMobile &&  <div className={styles.play}>
                    <Icons name={'playItem'} size={'20px'} color={'#B1AFAF'}/>
                </div>}
                <p className={styles.name}>{track.name}</p>
                <Icons className={styles.like} name={'like'} color={'#B1AFAF'} size={'20px'}/>
                {!isMobile && <span className={styles.duration}>{track.duration ? track.duration : null}</span>}
                <div className={styles.popupMenu}>
                    <Icons name={'popupMenu'} color={'#B1AFAF'} size={isMobile ? '13px' : '20px' }/>
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

export default Track;
