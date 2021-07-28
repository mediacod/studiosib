import {useTypedSelector} from "./useTypedSelector";
import {useActions} from "./useActions";


export const useQueue = () => {
    const {
        pause,
        active,
        queue
    } = useTypedSelector((state) => state.player);
    const { getAlbumPage, setQueue, playTrack, pauseTrack, setActive } = useActions()

    const newQueue = (active, albumPage) => {
        setQueue({ queue: albumPage.tracks, idAlbum: albumPage.idAlbum, idType: 1, linkCover: albumPage.linkCover })
        setActive(active)
        playTrack();
    }

    const updateTrack = (newActive, albumPage) => {
        pauseTrack()
        newQueue(newActive, albumPage)
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
