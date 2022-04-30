import React from 'react';

const ConfigContext = React.createContext({
    pageName: 'StudioSib',
    setPageNameHandler: (name: string) => {},
    isMobile: false,
    idFavoritesTracks: []
});

export default ConfigContext