import Link from 'next/dist/client/link';
import React from 'react';
import MIcons from '../MIcons';

const navList = [
    {name: 'home', title: 'Главная', link: '/'},
    {name: 'playlists', title: 'Плейлисты', link: '/playlist'},
    {name: 'search', title: 'Поиск', link: '/search'},
    {name: 'user', title: 'Личный кабинет', link: '/favorite'},
    {name: 'lib', title: 'Меню', link: '/menu'},
]

const NavMobile = () => {
    return (
        <>
            <div className={'navContainer'}>
                {navList.map(n => {
                    return(
                        <Link href={n.link}>
                            <a className={'navItem'}>
                                <MIcons name={n.name} color={'#4B4B4B'} size={'24px'} />
                                <p className={'navTitle'}>{n.title}</p>
                            </a>
                        </Link>)
                })}
            </div>

            <style jsx>{`
              .navContainer{
                grid-area: ct;
                position: fixed;
                bottom: 0;

                display: flex;
                justify-content: space-between;
                align-items: center;
                height: 50px;
                width: 100vw;
                padding: 0 15px;
                background: #fff;
              }

              .navItem{
                display: flex;
                flex-direction: column;
                align-items: center;
                min-width: 50px;
              }

              .navTitle{
                font-size: 7px;
                font-weight: 400;
                line-height: 8px;
                margin: 5px 0 0;
              }

            `}</style>
        </>
    );
};

export default NavMobile;
