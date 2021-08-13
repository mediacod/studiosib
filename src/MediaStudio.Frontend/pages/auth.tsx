import React from 'react';
import Icons from "../components/Icons";

const Auth = () => {
    return (
        <>
            <div className={'authContainer'}>
                <div className={'authContainerForm'}>
                    <div className={'authIconPrev'}>
                        <Icons name={'prevPage'} className={'prevPage'} color={'#3A2C51'} size={'11px'} height={'19px'}/>
                    </div>
                    <h2 className={'authContainerTitle'}>StudioSib</h2>
                    <h3 className={'authContainerSubTitle'}>Войдите, чтобы продолжить</h3>

                    <form>
                        <div className={'inputWrap'}>
                            <input type="text" className={'input'}/>
                        </div>
                        <div className={'inputWrap'}>
                            <input type="text" className={'input'}/>
                        </div>
                        <button type={'button'}/>
                    </form>
                </div>
            </div>
            <style jsx>{`
              .authContainer{
                position: fixed;
                top: 0;
                left: 0;
                right: 0;
                bottom: 0;
                background: linear-gradient(180deg, #007CA4 0%, #0064AC 0.01%, #0163AB 0.02%, #0082AB 0.03%, #2B326D 100%);
                box-shadow: 0px 4px 4px rgba(0, 0, 0, 0.25);
                
                display: flex;
                justify-content: center;
                align-items: center;
              }
              
              .authContainerForm{
                width: 100%;
                max-width: 422px;
                height: 540px;
                background: #fff;
                padding: 50px 67px
              }
       
              .authIconPrev {
                position: relative;
                display: flex;
                justify-content: center;
                align-items: center;
                padding-right: 4px;
                background-color: #fff;
                background-repeat: no-repeat;
                width: 34px;
                height: 34px;
                border-radius: 50%;
                box-shadow: 0px 4px 7px rgba(0, 0, 0, 0.12);
              }
              
              .authContainerTitle{
               
                font-style: normal;
                font-weight: bold;
                font-size: 30px;
                line-height: 35px;
                
                color: #0774A0;
                
                mix-blend-mode: normal;
              }
              
              .authContainerSubTitle {
                font-style: normal;
                font-weight: normal;
                font-size: 16px;
                line-height: 19px;
                color: #5F5A5A
                mix-blend-mode: normal;
              }
              
              .inputWrap {
                width: 100%;
                height: 29px;
              }
              
            `}</style>
        </>
    );
};

export default Auth;