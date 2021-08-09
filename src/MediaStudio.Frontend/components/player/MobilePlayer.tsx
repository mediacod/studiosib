import React from "react";
import Icons from "../Icons";


export const MobilePlayer = ({name, play, playIcon}) => {

    return (
        <>
            <div className={'container'}>
                <Icons action={play} name={playIcon} color={'#fff'} size={'20'} className={'icon'} />
                <div className={'name-container'}>
                    <div className={'name'}>{name}</div>
                    <div className={'artist'}>Студия Сибирского объединения</div>
                </div>
            </div>
            <style>{`
                .container {
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
                }
                .artist{
                    font-size: 12px;
                }
                .icon {
                    margin-top: 10px;
                }
            `}
            </style>
        </>
    )
}