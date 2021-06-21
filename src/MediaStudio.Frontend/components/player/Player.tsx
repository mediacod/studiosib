import Link from 'next/link';
import React, { useEffect } from 'react';
import styles from "../../styles/Player.module.scss";
import Icons from "../Icons";
import { useTypedSelector } from "../../hooks/useTypedSelector";
import { useActions } from "../../hooks/useActions";
import { setCurrentTime, setDuration, setVolume } from "../../store/action-creators/player";
import { useDispatch } from "react-redux";
import { convertHMS } from "../../utils/convertHMS";
import { useAudio } from '../../hooks/useAudio';


const Player: React.FC = () => {

    useAudio();

    const { pause, volume, currentTime, duration, active, queue } = useTypedSelector(state => state.player)
    const { pauseTrack, playTrack, setCurrentTime, setDuration, setVolume, setActive } = useActions()

    let playIcon = pause ? 'play' : 'pause'
    let trackProgress = currentTime && duration ? 100 / duration * currentTime : 0

    const play = () => {

        if (pause) {
            playTrack()
        } else {
            pauseTrack()
        }
    }

    return (
        <>
            <div className={styles.player}>
                <div className={styles.progressBar}>
                    <div className={'progressMain'} />
                </div>
                <div className={styles.main}>
                    <div className={styles.info}>
                        <div className={styles.infoImage}>
                            <img src={'http://studiosib.ru/storage/album_images/uAj80dqz61yqZvothDRaR6mXx6omZqAXIwa9qYfQ.png'} width={48} height={48} loading={"lazy"} />
                        </div>
                        <div className={styles.infoNameContainer}>
                            <span className={styles.infoName}>Название трека</span>
                            <span className={styles.infoArtist}>Исполнитель</span>
                        </div>
                    </div>
                    <div className={styles.actions}>
                        <Icons name={'change'} color={'#4B4B4B'} size={'12px'} />
                        <Icons name={'repeat'} color={'#4B4B4B'} size={'11px'} />
                        <Icons name={'like'} color={'#4B4B4B'} size={'12px'} />
                        <Icons name={'download'} color={'#4B4B4B'} size={'12px'} />
                    </div>
                    <div className={styles.control}>
                        <Icons name={'prev'} color={'#4B4B4B'} size={'14px'} height={'11px'} className={styles.controlPrev} />
                        <div className={styles.controlPlay} onClick={play}>
                            <div className={styles.controlCircleBig} />
                            <div className={styles.controlCircle} />
                            <Icons name={playIcon} color={'#4B4B4B'} size={'18px'} className={styles.controlPlayIcon} />
                        </div>
                        <Icons name={'next'} color={'#4B4B4B'} size={'14px'} height={'11px'} className={styles.controlNext} />
                    </div>
                    <div className={styles.progress}>

                    </div>
                    <div className={styles.extra}>
                        <div className={styles.extraInfo}>
                            <span className={styles.extraCurrentTime}>{convertHMS(currentTime)} &ensp;/&ensp; </span>
                            <span className={styles.extraDuration}> {convertHMS(duration)}</span>
                        </div>
                        <div className={styles.queue}>
                            <Icons name={'queue'} color={'#4B4B4B'} size={'15px'} />
                        </div>
                        <div className={styles.volume}>
                            <Icons name={'mute'} color={'#4B4B4B'} size={'15px'} />
                        </div>
                    </div>
                </div>
            </div>

            <style>{`
                .progressMain{
                    position: absolute;
                    display: block;
                    height: 100%;
                    width: ${trackProgress}%;
                    background-color: aquamarine;
                }
            `}
            </style>
        </>

    );
};

export default Player;
