import React from 'react';
import {Routes, Route} from 'react-router-dom'
import {NoMatch} from "../pages";
import {appRouter, appMobileRouter} from "./routes";

interface IProps {
    isMobile: boolean;
}


const AppRouter:React.FC<IProps> = ({isMobile}) => {

    const router = isMobile ? appMobileRouter : appRouter

    return (
        <Routes>
            {router.map(({path, element}) =>
                <Route key={path} path={path} element={element}/> )}
            <Route path="*" element={ <NoMatch /> } />
        </Routes>
    );
};

export default AppRouter;