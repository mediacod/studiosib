import React from 'react';
import { SectionNav } from '../nav/Nav';
import Section from "./Section";
import {IPage} from "../../types/page";

interface page {
    page: IPage[] | []
}

const Sections: React.FC<page> = ({page}) => {

    return (
        <div className={'sectionsContainer'}>
            {page?.map(s => {
                return <Section title={s.nameSection} key={s.nameSection } cells={s.cells}/>
            })}
        </div>
    );
};

export default Sections;
