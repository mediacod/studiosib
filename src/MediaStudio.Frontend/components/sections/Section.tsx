import React, {useRef} from 'react';
import AlbumItem from "../albumItem";
import styles from "../../styles/Section.module.scss"
import useCalcColumn from "../../hooks/useCalcColumn";
import PlaylistItem from "../playlistItem";
import {ICells} from "../../types/page";

const albums = [
    {id: 1, title: 'Желтый колосок', count: 14, url: '', 'color': '#68b463'},
    {id: 2, title: 'Желтый колосок', count: 10, url: 'http://studiosib.ru/storage/album_images/dEiYe3AOcfqnQCCzHMZnel1YihzFPkZ0Gjffscji1.jpeg'},
    {id: 3, title: 'Песню новую пою', count: 14, url: 'http://studiosib.ru/storage/album_images/UNc2CCM4kwreHCd937nMkWJHCad5G7KnoxLS8xQQ.jpeg'},
    {id: 4, title: 'Бог не ошибается', count: 13, url: 'http://studiosib.ru/storage/album_images/r56ne0sHkHatRVXv65R16PdjCnYtEmjQEVM7r5BA.png'},
    {id: 5, title: 'Все будет иначе', count: 5, url: 'http://studiosib.ru/storage/album_images/r6K1dlgNj8LNsn7A16qtw8ug67IZsiFJDcwdUk2r.png'},
    {id: 6, title: 'Я хочу с Тобой поговорить', count: 11, url: '', 'color': '#6375b4'},
    {id: 7, title: 'Я хочу с Тобой поговорить', count: 12, url: 'http://studiosib.ru/storage/album_images/rBjrygYDuMTzRVvreKZVYhdo8vK4mS3oKiPEWrfr.png'},
    {id: 8, title: 'Я хочу с Тобой поговорить', count: 14, url: 'http://studiosib.ru/storage/album_images/rrY8Dw6gWdHMnxFA0l6rZyAGQvnmAdksH2CzfeHu.png'},
    {id: 8, title: 'Я хочу с Тобой поговорить', count: 14, url: 'http://studiosib.ru/storage/album_images/uAj80dqz61yqZvothDRaR6mXx6omZqAXIwa9qYfQ.png'},
    {id: 8, title: 'Я хочу с Тобой поговорить', count: 14, url: 'http://studiosib.ru/storage/album_images/wir6ijdIonuAVoRIJAG9abzH1E5QxRrgTJR4TytJ.png'},
    {id: 8, title: 'Я хочу с Тобой поговорить', count: 14, url: 'http://studiosib.ru/storage/album_images/UNc2CCM4kwreHCd937nMkWJHCad5G7KnoxLS8xQQ.jpeg'},
]

const Section: React.FC<{ title: string, cells: ICells[] }> = ({title, cells}) => {

    const myRef = useRef()
    const { style, column, albumWidth } = useCalcColumn(myRef)

    console.log(cells)

    return (
        <div className={styles.sectionContainer}>
            <h3 className={styles.sectionTitle}>{title}</h3>
            <div className={styles.section} style={style} ref={myRef}>
                {albums.map((a, i) => {
                    if(i >= column) return
                    return <AlbumItem key={a.id} album={a} albumWidth={albumWidth} />
                })}
            </div>
        </div>
    );
};

export default Section;
