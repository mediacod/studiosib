import { useActions } from "./useActions";
import { useTypedSelector } from "./useTypedSelector";

export const useQueue = () => {
  const {
    pause,
    volume,
    currentTime,
    duration,
    active,
    queue,
    idAlbum,
    idType,
  } = useTypedSelector((state) => state.player);
  const {
    pauseTrack,
    playTrack,
    setCurrentTime,
    setDuration,
    setVolume,
    setActive,
    setQueue,
  } = useActions();

  const newQueue = (tracks, idAlbumNew, idTypeNew, idTrackNew) => {
    if (idAlbum === idAlbumNew && idType === idTypeNew) {
      if (active?.idTrack === idTrackNew) {
        if (pause) {
          playTrack();
        } else {
          pauseTrack();
        }
      } else {
        setActive(tracks.filter((t) => t.idTrack === idTrackNew));
        if (pause) {
          playTrack();
        } else {
          pauseTrack();
        }
      }
    } else {
      setQueue(tracks);
    }
  };

  return newQueue;
};
