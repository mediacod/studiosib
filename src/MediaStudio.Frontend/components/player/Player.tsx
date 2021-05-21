import Link from 'next/link';
import React, {useEffect} from 'react';
import styles from "../../styles/Player.module.scss";
import Icons from "../Icons";
import {useTypedSelector} from "../../hooks/useTypedSelector";
import {useActions} from "../../hooks/useActions";
import {setCurrentTime, setDuration, setVolume} from "../../store/action-creators/player";
import {useDispatch} from "react-redux";
import {convertHMS} from "../../utils/convertHMS";

let audio;
const music = 'http://msapi.ru:9000/audio/c6f41ea3f62ed4f92f00530b179a1cce.mp3?X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Credential=mL74Bd%40eD9xSak%2F20210507%2Fus-east-1%2Fs3%2Faws4_request&X-Amz-Date=20210507T094934Z&X-Amz-Expires=604400&X-Amz-SignedHeaders=host&&X-Amz-Signature=69a34f6345950b615c64ae6090e08122a744b9c826cfb46edd44951ba6599b47'

const Player: React.FC = () => {

    const {pause, volume, currentTime, duration, active} = useTypedSelector(state => state.player)
    const {pauseTrack, playTrack, setCurrentTime, setDuration, setVolume, setActive} = useActions()

    let playIcon = pause ? 'play' : 'pause'
    let trackProgress = currentTime && duration ? 100 / duration * currentTime : 0
    console.log(currentTime, duration)

    useEffect(()=>{
        if(!audio){
            audio = new Audio()
            audio.src = music;
            audio.onloadedmetadata = () => {
                setDuration(audio.duration)
            }
            audio.ontimeupdate = () => {
                setCurrentTime(audio.currentTime)
            }

        }
    }, [])

    const play = () => {
        if(pause){
            playTrack()
            audio.play()
        }else {
            pauseTrack()
            audio.pause()
        }
    }

    return (
        <>
            <div className={styles.player}>
                <div className={styles.progressBar}>
                    <div className={'progressMain'}/>
                </div>
                <div className={styles.main}>
                    <div className={styles.info}>
                        <div className={styles.infoImage}>
                            <img src={'http://studiosib.ru/storage/album_images/uAj80dqz61yqZvothDRaR6mXx6omZqAXIwa9qYfQ.png'} width={48} height={48} loading={"lazy"}/>
                        </div>
                        <div className={styles.infoNameContainer}>
                            <span className={styles.infoName}>Название трека</span>
                            <span className={styles.infoArtist}>Исполнитель</span>
                        </div>
                    </div>
                    <div className={styles.actions}>
                        <Icons name={'change'} color={'#4B4B4B'} size={'12px'}/>
                        <Icons name={'repeat'} color={'#4B4B4B'} size={'11px'}/>
                        <Icons name={'like'} color={'#4B4B4B'} size={'12px'}/>
                        <Icons name={'download'} color={'#4B4B4B'} size={'12px'}/>
                    </div>
                    <div className={styles.control}>
                        <Icons name={'prev'} color={'#4B4B4B'} size={'14px'} height={'11px'} className={styles.controlPrev}/>
                        <div className={styles.controlPlay} onClick={play}>
                            <div className={styles.controlCircleBig}/>
                            <div className={styles.controlCircle}/>
                            <Icons name={playIcon} color={'#4B4B4B'} size={'18px'} className={styles.controlPlayIcon}/>
                        </div>
                        <Icons name={'next'} color={'#4B4B4B'} size={'14px'} height={'11px'} className={styles.controlNext}/>
                    </div>
                    <div className={styles.progress}>

                    </div>
                    <div className={styles.extra}>
                        <div className={styles.extraInfo}>
                            <span className={styles.extraCurrentTime}>{convertHMS(currentTime)} &ensp;/&ensp; </span>
                            <span className={styles.extraDuration}> {convertHMS(duration)}</span>
                        </div>
                        <div className={styles.queue}>
                            <Icons name={'queue'} color={'#4B4B4B'} size={'15px'}/>
                        </div>
                        <div className={styles.volume}>
                            <Icons name={'mute'} color={'#4B4B4B'} size={'15px'}/>
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
