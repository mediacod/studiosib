import React from "react";
import Play from "../icons/Play";


export const MobilePlayer = ({name, play, playIcon}) => {

    return (
        <>
            <div className={'player-container'}>
                <Play action={play} color={'#fff'} size={'20'} className={'icon'} />
                <div className={'name-container'}>
                    <div className={'name'}>{name}</div>
                    <div className={'artist'}>Студия Сибирского объединения</div>
                </div>
            </div>
            <style>{`
                .player-container {
                    position: fixed;
                    display: flex;
                    align-items: center;
                    bottom: 50px;
                    width: 100vw;
                    height: 50px;
                    background: #fff linear-gradient(270deg, rgba(53, 169, 185, 0.8) 0%, rgba(0, 113, 151, 0.8) 100%);
                    box-shadow: inset 1px 0px 10px rgba(0, 0, 0, 0.07);
                    z-index: 1000;
                    padding: 0 10px;
                    color: #fff;
                    font-size: 16px;
                }
                .name-container {
                    display: flex;
                    flex-direction: column;
                    margin-left: 10px;
                    max-width: 80vw;
                    overflow: hidden;
                    text-overflow: ellipsis;
                }
                .name{
                    font-size: 16px;
                    white-space: nowrap;
                    width: 100%;
                    overflow: hidden;
                    text-overflow: ellipsis;
                }
                .artist{
                    font-size: 12px;
                    white-space: nowrap;
                    width: 100%;
                    overflow: hidden;
                    text-overflow: ellipsis;
                }
                .icon {
                    margin-top: 10px;
                }
            `}
            </style>
        </>
    )
}