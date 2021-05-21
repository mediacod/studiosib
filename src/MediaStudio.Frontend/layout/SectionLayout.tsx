import React from 'react';
import { SectionNav } from '../components/nav/Nav';
import useMobileDetect from '../hooks/useUserAgent';

const SectionLayout: React.FC = ({children}) => {

    const {isMobile} = useMobileDetect();

    return (
        <>
            <div className={'container'}>
                {isMobile && <SectionNav />}
                <div className={'content'}>
                    {children}
                </div>
            </div>

            <style jsx>{`
        
              .container {
                display: block;
                width: 100%;
                height: 100%;
                max-height: calc(100vh - 155px);
                padding: 23px 39px 0 46px;
                overflow-y: scroll;
              }
              
              @media (max-width: 500px){
                .container{
                  padding: 50px 0;
                  width: 100vw;
                  max-height: initial;
                  }
                }
              
            `}</style>
        </>
    );


};

export default SectionLayout;
