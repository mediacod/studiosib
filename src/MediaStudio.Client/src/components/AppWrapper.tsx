import React from 'react';
import {LayoutMobile} from "../mobile/components/common/layout/LayoutMobile";
import {Layout} from "./comon/layout/Layout";
import {useConfig} from "../hooks/config.hook";
import ConfigContext from '../context/configContext';

interface IProps {
    isMobile: boolean;
}

export const AppWrapper: React.FC<IProps> = ({children, isMobile}) => {
    const config: any = useConfig()

    return (
        <ConfigContext.Provider value={{...config, isMobile}} >
            {isMobile
                ?   <LayoutMobile>
                        {children}
                    </LayoutMobile>
                :   <>
                        {/*<div>*/}
                        {/*    воспользуйтесь мобильной версией*/}
                        {/*</div>*/}
                        <Layout>
                            {children}
                        </Layout>
                    </>
            }

        </ConfigContext.Provider>
    );
};