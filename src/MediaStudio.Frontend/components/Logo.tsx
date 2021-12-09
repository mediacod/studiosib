import React from 'react';
import logo from '../public/static/images/StudioSib.svg'

const Logo: React.FC = () => {
    return (
        <div>
            <img src={logo} className={logo}/>
        </div>
    );
};

export default Logo;
