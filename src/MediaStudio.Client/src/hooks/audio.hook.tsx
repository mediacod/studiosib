import React, {useEffect, useRef, useState} from 'react';
import {USER_HISTORY} from "../api/api";

export const useAudio = () => {
    const [progress, setProgress] = useState({})
    const [playing, setPlaying] = useState(false)
    const [playlist, setPlaylist] = useState([])
    const [track, setTrack] = useState({idTrack: null})
    const [albumData, setAlbumData] = useState({idAlbum: null, idTypeAudio: null})

    useEffect(()=>{
        if(track?.idTrack){
            try {
                USER_HISTORY.ADD(track?.idTrack)
            }catch (e) {
            }
        }
    }, [track])

    const play = ({data, idTrack}: any) => {
        setAudio({
            data,
            idTrack: idTrack,
        })
    }


    const togglePlaying = () => {
        setPlaying((prevState) => !prevState)
    }

    const getTrack = (tracks: any, id: any) => {
        return id ? tracks?.find((t: any) => t?.idTrack === id) : tracks[0]
    }

    const getNextTrack = ():any => {
        const index = playlist?.findIndex((t: any) => t?.idTrack === track?.idTrack)
        if(playlist?.length > index + 1){
            return playlist[index + 1];
        }else{
            return playlist[0];
        }
    }

    const getPrevTrack = ():any => {
        const index = playlist?.findIndex((t: any) => t?.idTrack === track?.idTrack)
        if(playlist?.length > index - 1){
            return playlist[index - 1];
        }else{
            return playlist[playlist.length - 1];
        }
    }

    const setAudio = ({idTrack: idTrackNew, data: {tracks, ...albumDataNew}}: any) => {

        const isNewAlbum = albumData?.idAlbum === albumDataNew?.idAlbum &&
            albumData?.idTypeAudio === albumDataNew?.idTypeAudio

        if(isNewAlbum){
            if(idTrackNew === track?.idTrack){
                togglePlaying()
            }else{
                const currentTrack = getTrack(tracks, idTrackNew)
                setTrack(currentTrack)
                setPlaying(true)
            }
        }else{
            setAlbumData(albumDataNew)
            setPlaylist(tracks)
            const currentTrack = getTrack(tracks, idTrackNew)
            setTrack(currentTrack)
            setPlaying(true)
        }
    }

    const callbacks = {
        onEnded: () => {
            callbacks.setNextTrack()
        },
        onProgress : (data: any) => {
            setProgress(data)
        },
        setPrevTrack: () => {
            const prevTrack = getPrevTrack()
            if(prevTrack) {
                setTrack(prevTrack)
                setPlaying(true)
            }
        },
        setNextTrack: () => {
            const nextTrack = getNextTrack()
            if(nextTrack) {
                setTrack(nextTrack)
                setPlaying(true)
            }
        }
    }

    const PLAYER = useRef()

    return {setAudio, togglePlaying, albumData, track, playing, playlist, callbacks, progress, play, PLAYER}
};