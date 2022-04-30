import React, {useMemo} from 'react';
import {List, Skeleton} from "antd-mobile";
import styled from "styled-components";

interface IProps {
    count?: number
}

const getRandomNumber = (min: number, max: number) => Math.random() * (max - min) + min;

export const TracksSkeleton:React.FC<IProps> = React.memo(({count}) => {

    const createArray = useMemo(() =>
        (s = 10) => Array.from({ length: s }, () => getRandomNumber(45, 75)), [count]);

    return (
        <>
            <List>
                {createArray(count)?.map((number, index) =>
                    <List.Item key={index&&number}>
                        <StyledSkeletonTrack lineCount={1} animated width={number}/>
                    </List.Item>)}
            </List>
        </>
    );
});

const StyledSkeletonTrack = styled(Skeleton.Paragraph)<{width: number}>`
  .adm-skeleton.adm-skeleton-paragraph-line:last-child{
    --width: ${({width})=> width}%;
  }
  .adm-skeleton.adm-skeleton-paragraph-line{
    --height: 16px;
    margin: 4px 0;
  }
`