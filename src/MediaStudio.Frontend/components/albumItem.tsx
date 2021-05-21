import Link from 'next/link';
import React, {useRef, useState} from 'react';
import styles from '../styles/AlbumItem.module.scss'
import {IAlbumItem} from "../types/album";
// @ts-ignore
import cover from '../public/images/coverDefault.png';

interface IAlbum{
    album: IAlbumItem;
    albumWidth: number
}

const AlbumItem: React.FC<IAlbum> = ({album, albumWidth}) => {

    const [coverLink, setCoverLink ] = useState(album.url);
    const styleAlbumCover = {"--albumWidth": albumWidth + 4 +'px', display: 'block'}

    const coverError = () => {
        setCoverLink(cover);
    };

    return (
        <div className={styles.albumContainer}>
            <Link href={'/album/'+ album.id}>
                <a>
                    <div className={styles.albumCoverContainer} style={styleAlbumCover}>
                        <img className={styles.albumCover} src={coverLink } alt={album.title} onError={coverError} loading={"lazy"}/>
                    </div>
                    <div className={styles.albumTitle}>{album.title}</div>
                    <div className={styles.albumCountTrack}>{album.count} треков</div>
                </a>
            </Link>

        </div>
    );
};

export default AlbumItem;
