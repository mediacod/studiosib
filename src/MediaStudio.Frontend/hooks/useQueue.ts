import {useTypedSelector} from "./useTypedSelector";
import {useActions} from "./useActions";
import {useEffect, useState} from "react";


export const useQueue = () => {
    const {
        pause,
        active,
        queue,
        idAlbum,
        idType,
        linkCover,
        isShuffle
    } = useTypedSelector((state) => state.player);
    const { getAlbumPage, setQueue, playTrack, pauseTrack, setActive, setIndexQueue } = useActions()
    const [restoreQueue, setRestoreQueue] = useState([])

    useEffect(()=>{
        shuffle()
    }, [isShuffle])

    const newQueue = (active, albumPage) => {

        if(isShuffle){
            const newQueue = shuffleAction(albumPage.tracks)
            setQueue({ queue: newQueue, idAlbum: albumPage.idAlbum, idType: 1, linkCover: albumPage.linkCover })
        }else {
            setQueue({ queue: albumPage.tracks, idAlbum: albumPage.idAlbum, idType: 1, linkCover: albumPage.linkCover })
        }
        setActive(active)
        playTrack();
    }

    const updateTrack = (newActive, albumPage) => {
        pauseTrack()
        newQueue(newActive, albumPage)
    }

    const shuffleAction = (queue) => {
        setRestoreQueue(queue)
        const shuffleQueue = queue.slice().sort(() => Math.random() - 0.5);
        let currentId = queue?.findIndex(t => t.idTrack === active?.idTrack)

        let currentTracks = shuffleQueue.splice(0, currentId);
        return [...shuffleQueue, ...currentTracks]
    }

    const shuffle = () => {

        if (isShuffle) {
            const newQueue = shuffleAction(queue)
            setQueue({ queue: newQueue, idAlbum, idType, linkCover })
        }else {
            let currentId = queue.findIndex(t => t.idTrack === active?.idTrack)
            setQueue({ queue: restoreQueue, idAlbum, idType, linkCover })
            setIndexQueue(currentId)
            setRestoreQueue([])
        }
    }

    const playHandler = (idTrack, albumPage, idAlbum, idType) => {

        const newActive = albumPage?.tracks.filter(a => a.idTrack === idTrack)

        if(idAlbum === albumPage?.idAlbum && idType === 1){
            if(idTrack === active?.idTrack){
                if (pause) {
                    playTrack()
                } else {
                    pauseTrack()
                }
            }else {
                updateTrack(newActive[0], albumPage)
            }
        }else {
            newQueue(newActive[0], albumPage)
        }
    }

    return {playHandler, updateTrack, newQueue};
};
