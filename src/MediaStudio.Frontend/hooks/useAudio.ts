import { useEffect } from "react";
import { useActions } from "./useActions";
import { useTypedSelector } from "./useTypedSelector";

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

  useEffect(() => {
    if (!audio) {
      audio = new Audio();
    }
  }, []);

  useEffect(() => {
    if (active) {
      audio.src = active?.link;
      audio.onloadedmetadata = () => {
        setDuration(audio.duration);
      };
      audio.ontimeupdate = () => {
        setCurrentTime(audio.currentTime);
      };
    }
  }, [active]);

  useEffect(() => {
    if (active) play();
  }, [pause]);

  const play = () => {
    if (!pause) {
      audio.play();
    } else {
      audio.pause();
    }
  };

  return;
};
