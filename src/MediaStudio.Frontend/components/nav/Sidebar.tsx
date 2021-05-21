import React from 'react';
import styles from '../../styles/Sidebar.module.scss';
import Logo from "../Logo";
import logo from "../../public/images/StudioSib.svg";
import Nav from "./Nav";

const Sidebar: React.FC = () => {
    return (
        <div className={styles.sidebar}>
            <img src={logo} className={styles.logo}/>
            <div className={styles.nav}>
                <Nav/>
            </div>
        </div>
    );
};

export default Sidebar;
