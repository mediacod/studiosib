import {useEffect, useState} from "react";
import {useActions} from "./useActions";
import {useTypedSelector} from "./useTypedSelector";
import {is} from "@babel/types";
import {setNewTime} from "../store/action-creators/player";

let audio: any;

export const useAudio = () => {
    const {
        pause,
        volume,
        currentTime,
        duration,
        active,
        queue,
        idAlbum,
        idType,
        repeat,
        repeatOne,
        isShuffle,
        isNext,
        isPrev,
        linkCover,
        newTime
    } = useTypedSelector((state) => state.player);
    const {
        pauseTrack,
        playTrack,
        setCurrentTime,
        setDuration,
        setVolume,
        setActive,
        setQueue,
        setIsNextFalse,
        setIsPrevFalse,
        setNewTime
    } = useActions();

    const [currentActive, setCurrentActive] = useState({idTrack: null})
    const [index, setIndex] = useState(0)

    useEffect(() => {
        audio = new Audio();
    }, []);

    useEffect(() => {

        if (active && currentActive?.idTrack != active?.idTrack) {

            audio.src = active?.link;
            audio.onloadedmetadata = () => {
                setDuration(audio.duration);
            };
            audio.ontimeupdate = () => {
                setCurrentTime(audio.currentTime);
            };

            audio.addEventListener('ended', () => {
                next()
            });

            let newIndex = queue.map(t => t.idTrack).indexOf(active.idTrack)
            if(newIndex < 0) newIndex = 0

            setIndex(newIndex)
            setCurrentActive(active)

        }
    }, [active]);

    useEffect(() => {

        if (!pause) play();
        else suspend()

    }, [pause, active]);

    useEffect(() => {
        if(isNext){
            next()
            setIsNextFalse()
        }
    }, [isNext]);

    useEffect(() => {
        if(isPrev){
            prev()
            setIsPrevFalse()
        }
    }, [isPrev]);

    useEffect(() => {

        if(newTime != 0) changeTime(newTime)
    }, [newTime])

    const suspend = () => {
        audio.pause();
    }

    const play = () => {
        audio.play();
    };

    const next = () => {

        if(index + 1 >= queue.length) {
          if (repeat) setIndex(0);
          else return
        }

        setActive(queue[index + 1])
        setIndex(prevState => prevState + 1);

        playTrack();
    };

    const prev = () => {

        if(index < 1 || queue.length < 1) return;

        setActive(queue[index - 1]);
        setIndex(prevState => prevState - 1);

        playTrack();
    };

    const changeTime = (data) => {
        audio.currentTime = data * duration / 100;
    }


    return;
};
