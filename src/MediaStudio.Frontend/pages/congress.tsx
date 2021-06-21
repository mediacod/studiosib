import React from 'react';
import MainLayout from "../layout/MainLayout";
import SectionLayout from "../layout/SectionLayout";
import Sections from "../components/sections/Sections";
import { useGetPage } from '../hooks/useGetPage';
import { useTypedSelector } from '../hooks/useTypedSelector';

const Congress = () => {

    useGetPage(5)
    const {page} = useTypedSelector(state => state.page); 

    return (
        <MainLayout>
            <SectionLayout>
                <Sections page={page} />
            </SectionLayout>
        </MainLayout>
    );
};

export default Congress;
