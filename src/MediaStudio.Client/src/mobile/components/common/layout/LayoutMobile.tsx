import React, {FC, useContext, useRef, useState} from 'react'
import {Avatar, NavBar, TabBar, Space, List, Modal} from 'antd-mobile'

import {
    useNavigate,
    useLocation,
} from 'react-router-dom'
import {
    AppOutline,
    MoreOutline,
    SearchOutline,
    UnorderedListOutline,
    UserOutline,
} from 'antd-mobile-icons'
import styled from "styled-components";
import {MAIN_PAGE, PERSONAL_PAGE, USER_PLAYLISTS_PAGE, SEARCH_PAGE, MY_FAVORITE_TRACK} from "../../../../router/const";
import {useThrottledCallback} from "beautiful-react-hooks";
import {Player} from "../../blocks/Player";
import configContext from "../../../../context/configContext";
import {MainMenu} from "../../MainMenu";
import AuthContext from "../../../../context/authContext";
import {PAGES} from "../../../../utils/const";
import {PlayerFull} from "../../blocks/PlayerFull";


const Bottom: FC = () => {
    const navigate = useNavigate()
    const location = useLocation()
    const {pathname} = location

    const setRouteActive = (value: string) => {
        navigate(value)
    }

    const customPersonalKey = pathname?.includes(PERSONAL_PAGE) ? pathname : MY_FAVORITE_TRACK;
    const customMainPage = PAGES.some((p) => p.path === pathname) ? pathname : MAIN_PAGE;

    const tabs = [
        {
            key: customMainPage,
            title: 'Главная',
            icon: <AppOutline />,
        },
        {
            key: SEARCH_PAGE,
            title: 'Поиск',
            icon: <SearchOutline />,
        },
        {
            key: USER_PLAYLISTS_PAGE,
            title: 'Плейлисты',
            icon: <UnorderedListOutline />,
        },
        {
            key: customPersonalKey,
            title: 'Моя музыка',
            icon: <UserOutline />,
        },
    ]

    return (
        <TabBar activeKey={pathname} onChange={value => setRouteActive(value)}>
            {tabs.map(item => (
                <TabBar.Item key={item.key} icon={item.icon} title={item.title} />
            ))}
        </TabBar>
    )
}

export const LayoutMobile:React.FC = ({children}) => {

    const [hiddenHeader, setHiddenHeader] = useState(false)
    const [menuVisible, setMenuVisible] = useState(false)
    const [isOpenFull, setIsOpenFull] = useState(false)
    const {pageName} = useContext(configContext)
    const {user, isAuth, logout, token, login} = useContext(AuthContext)
    const navigate = useNavigate()
    const appRef:any = useRef(null)
    const bottomRef: any = useRef(null)
    const bodyRef = useRef(null)

    const delay = 200;
    const delayUp = 100;
    let scrollTop = 0;

    const hiddenHeaderHandler = useThrottledCallback((isHidden) => {
        setHiddenHeader(isHidden)
    }, [],100);


    const logoutHandler = () => {
        logout()
        setMenuVisible(false)
    }

    const onScrollBody = useThrottledCallback((e: any) => {

        const currentOffsetTop = e.target.offsetTop
        const currentScrollTop = e.target.scrollTop + currentOffsetTop

        if(currentScrollTop > scrollTop) {
            if (currentScrollTop > delay) {
                hiddenHeaderHandler(true)
                return scrollTop = currentScrollTop;
            }
        }

        if(currentScrollTop + delayUp < scrollTop) {
            hiddenHeaderHandler(false)
            return scrollTop = currentScrollTop;
        }
    })

    const openHandler = (isOpen: boolean) => {
        setIsOpenFull(isOpen)
    }

    const right = (
        <div style={{ fontSize: 24 }}>
            <Space style={{ '--gap': '16px' }}>
                {/*<SearchOutline onClick={()=>navigate(SEARCH_PAGE)} />*/}
                {isAuth && <MoreOutline onClick={() => setMenuVisible(true)} /> }
            </Space>
        </div>)

    return (
        <>
            <StyledTop hiddenHeader={hiddenHeader}>
                <NavBar right={right} onBack={()=>navigate(-1)}>
                    {pageName}
                </NavBar>
            </StyledTop>
            <StyledApp
                paddingBottom={bottomRef?.current?.offsetHeight}
                hiddenHeader={hiddenHeader}
                ref={appRef}>
                <StyledBody ref={bodyRef} onScroll={onScrollBody}>
                    {children}
                </StyledBody>
            </StyledApp>
            <StyledBottom ref={bottomRef}>
                <Player openHandler={openHandler}/>
                <Bottom />
            </StyledBottom>
            {isOpenFull && <PlayerFull
                openHandler={openHandler}
            />}
            <Modal
                visible={menuVisible}
                content={<MainMenu
                    title={`${user?.firstName} ${user?.lastName}`}
                    logout={logoutHandler}
                    user={user}
                    login={login}
                />}
                closeOnAction
                closeOnMaskClick
                showCloseButton
                onClose={() => setMenuVisible(false)}
            />
        </>
    )
}

const StyledApp = styled.div<{paddingBottom: any, hiddenHeader: boolean}>`
  height: 100vh;
  box-sizing: border-box;
  padding-bottom: ${({paddingBottom}) => paddingBottom}px;
  display: flex;
  flex-direction: column;
  overflow-y: hidden;
  -webkit-overflow-scrolling: touch;
`

const StyledTop = styled.div<{hiddenHeader: any}>`
  width: 100vw;
  transform: translateY(${({hiddenHeader}) => hiddenHeader ? -45 : 0}px);
  opacity: ${({hiddenHeader}) => hiddenHeader ? 0 : 110};
  position: fixed;
  top: 0;
  left: 0;
  transition: 300ms;
  z-index: 50;
  background-color: #ffffff;
  
  .adm-nav-bar{
    --height: 45px;
    overflow: hidden;
    transition: 300ms;
  }
`

const StyledBody = styled.div`
  display: block;
  flex: 1;
  width: 100%;
  height: auto;
  padding-bottom: 20px;
  padding-top: 45px;
  overflow-y: scroll;
`

const StyledBottom = styled.div`
  width: 100vw;
  background-color: #fff;
  flex: 0;
  bottom: 0;
  position: fixed;
  overflow-y: hidden;
  border-top: solid 1px var(--adm-border-color);
`