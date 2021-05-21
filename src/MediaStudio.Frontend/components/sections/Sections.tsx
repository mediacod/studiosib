import React from 'react';
import { SectionNav } from '../nav/Nav';
import Section from "./Section";
import {IPage} from "../../types/page";

interface page {
    sections: IPage | []
}

const Sections: React.FC<{ page }> = (page) => {

    return (
        <div className={'sectionsContainer'}>
            {/*{page.sections.map(s => {*/}
            {/*    <Section title={s.nameSection} key={s.nameSection } cells={s.cells}/>*/}
            {/*})}*/}
            {/*<Section title={'Музыка'}/>*/}
            {/*<Section title={'Музыка'}/>*/}
            {/*<Section title={'Музыка'}/>*/}
        </div>
    );
};

export default Sections;
