import React, {useState} from 'react';
import {SectionGrid} from "./blocks/SectionGrid";
import {Section} from "./blocks";
import {isBoolean} from "lodash-es";

export const SectionWrapper:React.FC<any> = ({sectionData, index}) => {

    const [open, setOpen] = useState(false)

    const openSection: any = (isOpen?: any) => {
        setOpen((prevState) => isBoolean(isOpen) ? isOpen : !prevState)
    }

    return (
        <>
            {!!open
                ? <SectionGrid albums={sectionData?.cells}/>
                : <Section key={sectionData.nameSection} sectionData={sectionData} openSection={openSection}/>
            }
        </>
    );
};