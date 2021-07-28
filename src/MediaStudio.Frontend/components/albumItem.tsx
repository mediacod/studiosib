import Link from 'next/link';
import React, {useRef, useState} from 'react';
import styles from '../styles/AlbumItem.module.scss'
import {IAlbumItem} from "../types/album";
// @ts-ignore
import cover from '../public/images/coverDefault.png';

interface IAlbum{
    album: IAlbumItem;
    albumWidth: number;
    type?: number;
}

const AlbumItem: React.FC<IAlbum> = ({album, albumWidth, type = 1}) => {

    const [coverLink, setCoverLink ] = useState(album.linkCover);
    const styleAlbumCover = {"--albumWidth": albumWidth + 4 +'px', display: 'block'}

    const coverError = () => {
        setCoverLink(cover);
    };

    const typeAlbum = type && type === 1 ? 'album' : 'playlist'

    return (
        <div className={styles.albumContainer}>
            <Link href={`/${typeAlbum}/`+ album.idObject}>
                <a>
                    <div className={styles.albumCoverContainer} style={styleAlbumCover}>
                        <img className={styles.albumCover} src={coverLink || cover} alt={album.name} onError={coverError} loading={"lazy"}/>
                    </div>
                    <div className={styles.albumTitle}>{album.name}</div>
                    <div className={styles.albumCountTrack}>{album.countTrack} треков</div>
                </a>
            </Link>
        </div>
    );
};

export default AlbumItem;
