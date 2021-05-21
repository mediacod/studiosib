import React from 'react';
import Link from 'next/link'
import styles from '../../styles/Nav.module.scss'
import Icons from "../Icons";
import {useRouter} from "next/router";

const navList = [
    {title: 'Главная', name: 'home', size: '23px', link: '/'},
    {title: 'Музыка', name: 'music', size: '23px', link: '/music'},
    {title: 'Проповеди', name: 'preach', size: '23px', link: '/preach'},
    {title: 'Аудиокниги', name: 'books', size: '23px', link: '/books'},
    {title: 'Конференции', name: 'congress', size: '23px', link: '/congress'}
]

const favoriteNavList = [
    {title: 'История', name: 'history', size: '23px', link: '/history'},
    {title: 'Избранное', name: 'favorite', size: '23px', link: '/favorite'},
]

interface linkInterface {
    title: string;
    name: string;
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
                            {isIcons && <Icons name={item.name} size={item.size} color={'#fff'}/>}
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
