import React from 'react';



const MobileHeader = () => {
    return (
        <>
            <div className={'headerContainer'}>
                <div className={'title'}></div>
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
                box-shadow: 0 0 20px rgba(0, 0, 0, 0.15);
                
                z-index: 1000;
              }
              
              .title {
                color: #fff;
                font-weight: normal;
                font-size: 24px;
              }
              
            `}</style>
        </>
    );
};

export default MobileHeader;
