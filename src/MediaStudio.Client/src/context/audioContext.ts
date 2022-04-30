import React from 'react';

const AudioContext = React.createContext({
    playing: false,
    track: {},
    setAudio: ()=>{},
    togglePlaying: ()=>{}
});

export default AudioContext