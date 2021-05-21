import React, {useEffect} from 'react';
import MainLayout from "../layout/MainLayout";
import SectionLayout from "../layout/SectionLayout";
import Sections from "../components/sections/Sections";
import {useTypedSelector} from "../hooks/useTypedSelector";
import {useActions} from "../hooks/useActions";
import {useGetPage} from "../hooks/useGetPage";

const Music = () => {

    useGetPage(1)
    const {page} = useTypedSelector(state => state.page)
    console.log(page)

    return (
        <MainLayout>
            <SectionLayout>
                <Sections page={page} />
            </SectionLayout>
        </MainLayout>
    );
};

export default Music;
