import React from 'react';
import {Books, Congress, Favorite, Home, Music, Preach, History} from "../../icons";
import {Link, useLocation, useNavigate} from "react-router-dom";
import styled from "styled-components";
import {
    BOOK_PAGE,
    CONFERENCES_PAGE,
    MAIN_PAGE,
    MUSIC_PAGE,
    MY_HISTORY_TRACK,
    PLAYLIST_PAGE,
    PREACH_PAGE
} from "../../../router/const";
// import Icons from "../Icons";
// import {useRouter} from "next/router";


const navList = [
    {title: 'Главная', el: <Home size={23} color={'#fff'}/>, size: '23px', link: MAIN_PAGE},
    {title: 'Музыка', el: <Music size={23} color={'#fff'}/>, size: '23px', link: MUSIC_PAGE},
    {title: 'Проповеди', el: <Preach size={23} color={'#fff'}/>, size: '23px', link: PREACH_PAGE},
    {title: 'Аудиокниги', el: <Books size={23} color={'#fff'}/>, size: '23px', link: BOOK_PAGE},
    {title: 'Конференции', el: <Congress size={23} color={'#fff'}/>, size: '23px', link: CONFERENCES_PAGE}
]

const favoriteNavList = [
    {title: 'История', el: <History size={23} color={'#fff'}/>, size: '23px', link: MY_HISTORY_TRACK},
    {title: 'Избранное', el: <Favorite size={23} color={'#fff'}/>, size: '23px', link: PLAYLIST_PAGE},
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

    const navigate = useNavigate()
    const location = useLocation()
    const {pathname} = location

    const setRouteActive = (value: string) => {
        navigate(value)
    }

    return (
        items.map(item => {
            return (
                <StyledItem isActive={pathname === item.link}>
                    <StyledLink to={item.link} >
                            {item.el}
                            <StyledTitle>{item.title}</StyledTitle>
                    </StyledLink>
                </StyledItem>
            )
        })
    )
}

const Nav: React.FC = () => {

    return (
        <div>
            <RenderNav items={navList}/>
            <StyledLine />
            <RenderNav items={favoriteNavList}/>
        </div>
    );
};

export default Nav;
//
// export const SectionNav: React.FC = () => {
//
//     return (
//         <div className={styles.mobile}>
//             <div className={styles.container}>
//                 <RenderNav items={navList} isIcons={false} />
//             </div>
//         </div>
//     );
// };

const StyledLine = styled.span`
  display: block;
  width: 127px;
  height: 1px;
  background-color: #fff;
  opacity: 50%;
  margin: 35px 0 30px 43px;
`

const StyledItem = styled.div<{isActive: boolean}>`
  display: flex;
  align-items: center;

  width: 100%;
  height: 34px;
  padding: 0 40px;

  font-size: 12px;
  font-weight: 600;
  line-height: 16px;
  text-align: left;
  color: #ffffff;
  
  background-color: ${({isActive}) => isActive ? '#0092C0' : 'none'} ;
  box-sizing: border-box;
`

const StyledLink = styled(Link)`
  display: flex;
  align-items: center;
`

const StyledTitle = styled.span`
  margin-left: 10px;
  color: #fff;
  user-select: none;
`