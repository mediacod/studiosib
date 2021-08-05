import React, {useRef} from 'react';
import AlbumItem from "../albumItem";
import styles from "../../styles/Section.module.scss"
import useCalcColumn from "../../hooks/useCalcColumn";
import {ICells} from "../../types/page";

const Section: React.FC<{ title: string, cells: ICells[], idTypeCells?: number}> = ({title, cells, idTypeCells= 0}) => {

    const myRef = useRef()
    const { style, column, albumWidth } = useCalcColumn(myRef)

    return (
        <div className={styles.sectionContainer}>
            <h3 className={styles.sectionTitle}>{title}</h3>
            <div className={styles.section} style={style} ref={myRef}>
                {cells.map((a, i) => {
                    if(i >= column) return
                    if(a.idTypeCell === 2){
                        return <AlbumItem key={a.idObject} album={a} albumWidth={albumWidth} type={2} />
                    }
                    return <AlbumItem key={a.idObject} album={a} albumWidth={albumWidth} />
                })}
            </div>
        </div>
    );
};

export default Section;
