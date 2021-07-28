import {useEffect, useState} from "react";
import {useActions} from "./useActions";
import {useTypedSelector} from "./useTypedSelector";
import {is} from "@babel/types";

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
        isPrev
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
    } = useActions();

    const [currentActive, setCurrentActive] = useState({idTrack: null})
    const [restoreQueue, setRestoreQueue] = useState([])
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

    useEffect(()=>{
        shuffle()
    }, [isShuffle])

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

    const shuffle = () => {

        if (isShuffle) {
            setRestoreQueue(queue)
            const shuffleQueue = queue.sort(() => Math.random() - 0.5);
            let currentId = queue?.findIndex(t => t.idTrack === active?.idTrack)

            let currentTracks = shuffleQueue.splice(0, currentId);
            console.log(isShuffle, currentTracks)
            const newQueue = [...shuffleQueue, ...currentTracks]
            setQueue({ queue: newQueue, idAlbum, idType })

        }else {
            console.log(isShuffle, restoreQueue)
            let currentId = queue.findIndex(t => t.idTrack === active?.idTrack)
            setQueue({ queue: restoreQueue, idAlbum, idType })
            setIndex(currentId)
            setRestoreQueue([])
        }
    }

    return;
};
