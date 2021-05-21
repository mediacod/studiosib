import React from 'react';
import styles from "../../styles/Card.module.scss"

const Card: React.FC = ({children}) => {
    return (
        <div className={styles.card}>
            {children}
        </div>
    );
};

export default Card;
