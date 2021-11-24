import React from 'react';
import {useRouter} from "next/router";
import {useActions} from "../../hooks/useActions";

const FormSearch = () => {

    const router = useRouter()
    const pathname = router.pathname

    const {getDataSearch} = useActions()

    const clickSearchHandler = () => {
        if(pathname != '/search'){
            router.push('/search')
        }
    }

    const searchHandler = (e) => {
        const value = e.target.value
        if(value.length > 0){
            getDataSearch(value)
        }
    }



    return (
        <>
            <form className={'form'}>
                <input type="search" placeholder="ПОИСК" onChange={searchHandler} onClick={clickSearchHandler} />
                <button type="button"/>
            </form>
        <style jsx>{`
              
              @media (max-width: 500px){
                .form {
                  width: calc(100vw - 40px);
                  max-height: initial;
                  height: 40px;
                  margin: 20px 10px;
                  }
                  
                  input{
                    height: 100%;
                    width: 100%;
                    padding: 0 10px;
                    outline: none;
                    font-size: 14px;
                  }
              }
                  
                button[type="button"] {
                    display: none
                }
              
            `}</style>
        </>
    );
};

export default FormSearch;