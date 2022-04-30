import React from 'react';

const AuthContext = React.createContext({
    token: null,
    login: '',
    logout: ()=>{},
    email: null,
    ready: null,
    isAuth: null,
    auth: (values: any)=>{},
    isMobile: null,
    user: {idUser: null, firstName: "", lastName:"", gender: null, dateBirthday: null},
    getUser: ()=>{}
});

export default AuthContext