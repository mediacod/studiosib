import React from 'react';
import Link from 'next/link'
import styles from '../../styles/Nav.module.scss'
import Icons from "../Icons";
import {useRouter} from "next/router";
import Home from "../icons/Home";
import Books from "../icons/Books";
import Congress from "../icons/Congress";
import Music from "../icons/Music";
import Preach from "../icons/Preach";
import History from "../icons/History";
import Favorite from "../icons/Favorite";

const navList = [
    {title: 'Главная', el: <Home size={'23px'} color={'#fff'}/>, size: '23px', link: '/'},
    {title: 'Музыка', el: <Music size={'23px'} color={'#fff'}/>, size: '23px', link: '/music'},
    {title: 'Проповеди', el: <Preach size={'23px'} color={'#fff'}/>, size: '23px', link: '/preach'},
    {title: 'Аудиокниги', el: <Books size={'23px'} color={'#fff'}/>, size: '23px', link: '/books'},
    {title: 'Конференции', el: <Congress size={'23px'} color={'#fff'}/>, size: '23px', link: '/congress'}
]

const favoriteNavList = [
    {title: 'История', el: <History size={'23px'} color={'#fff'}/>, size: '23px', link: '/history'},
    {title: 'Избранное', el: <Favorite size={'23px'} color={'#fff'}/>, size: '23px', link: '/favorite'},
]

interface linkInterface {
    title: string;
    el: any;
    size: string;
    link: string;
}

interface linksInterface {
    items: linkInterface[];
    isIcons?: boolean;
}

const RenderNav:React.FC<linksInterface> = ({items, isIcons = true}): any => {
    const router = useRouter()
    return (
        items.map(item => {
            const styleItem = router.route === item.link
                ? styles.link + " " + styles.linkActive
                : styles.link

            return (
                <div key={item.link} className={styles.item}>
                    <Link href={item.link}>
                        <a className={styleItem}>
                            {item.el}
                            <span className={styles.title}>{item.title}</span>
                        </a>
                    </Link>
                </div>
            )
        })
    )
}

const Nav: React.FC = () => {

    return (
        <div className={styles.sidebar}>
            <RenderNav items={navList}/>
            <span className={styles.line}/>
            <RenderNav items={favoriteNavList}/>
        </div>
    );
};

export default Nav;

export const SectionNav: React.FC = () => {

    return (
        <div className={styles.mobile}>
            <div className={styles.container}>
                <RenderNav items={navList} isIcons={false} />
            </div>
        </div>
    );
};
