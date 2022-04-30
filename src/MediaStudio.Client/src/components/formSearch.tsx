import React from 'react';

const FormSearch = () => {

    const pathname = 'test'

    // const {getDataSearch} = useActions()

    const getDataSearch = (value: any) => {}

    const clickSearchHandler = () => {
        // if(pathname !== '/search'){
        //     router.push('/search')
        // }
    }

    const searchHandler = (e: any) => {
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
        </>
    );
};

export default FormSearch;