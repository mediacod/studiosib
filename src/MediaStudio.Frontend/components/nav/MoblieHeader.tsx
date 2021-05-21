import React from 'react';

const MoblieHeader = () => {
    return (
        <>
            <div className={'headerContainer'}>
                <div>Главная</div>
            </div>

            <style jsx>{`
              
              .headerContainer{
                grid-area: mhd;
                position: fixed;
                top: 0;
                
                display: flex;
                justify-content: center;
                align-items: center;
                
                height: 46px;
                width: 100vw;
                padding: 16px;
                
                background: linear-gradient(90deg, #456380 0%, #AED5FA 100%);
                box-shadow: 0px 0px 20px rgba(0, 0, 0, 0.15);
              }
              
            `}</style>
        </>
    );
};

export default MoblieHeader;
