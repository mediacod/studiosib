import Link from 'next/link';
import Image from 'next/image'
import React, {useRef} from 'react';
import styles from '../styles/PlaylistItem.module.scss'
import cover from '../public/static/images/coverDefault.png';
import { IAlbumItem } from '../types/album';


interface IPlaylist {
    album: IAlbumItem;
    albumWidth: number
}

const PlaylistItem: React.FC<IPlaylist> = ({album, albumWidth}) => {

    const styleAlbumCover = {"--playlistWidth": Math.floor(albumWidth / 2.8) + 'px', backgroundColor: album.colorCode || '#fff'}
    const styleTitleContainer = {"--playlistWidth": Math.floor(albumWidth / 2.8) + 'px', display: 'block'}


    return (
        <Link href={'/album'}>
            <a>
                <div className={styles.playlistContainer}>
                    <div className={styles.playlistCoverContainer} style={styleAlbumCover}>
                        <Image id="require-static"
                               className={styles.playlistCover}
                               src={album.linkCover || cover}
                               alt={album.name}
                               loading={"lazy"}
                        />
                    </div>
                    <div className={styles.playlistTitleContainer} style={styleTitleContainer}>
                        <div className={styles.playlistTitle}>{album.name}</div>
                        <div className={styles.playlistCountTrack}>{album.countTrack} треков</div>
                    </div>
                </div>
            </a>
        </Link>
    );
};

export default PlaylistItem;
