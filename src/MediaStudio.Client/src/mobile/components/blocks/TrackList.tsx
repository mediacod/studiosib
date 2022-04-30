import React, {useState} from 'react';
import {DotLoading, Empty, ErrorBlock, InfiniteScroll, List} from "antd-mobile";
import {Track} from "./Track";
import {TracksSkeleton} from "./TracksSkeleton";
import {isArray} from "lodash-es";
const step = 30

interface IProps {
    tracks: any[]
    play: (id: number)=>void
}

export const TrackList:React.FC<IProps> = React.memo(({tracks, play, ...restProps}) => {

    const [data, setData] = useState<any>([])
    const [hasMore, setHasMore] = useState(true)
    const [number, setNumber] = useState(0)

    // const loadMore = async () => {
    //     const end = number + step > tracks?.length ? tracks?.length : number + step
    //     const append = tracks?.slice(number, number + step)
    //     setData((prevData: any) => [...prevData, ...append])
    //     setNumber(end)
    //     setHasMore(append.length > 0)
    // }

    if(!isArray(tracks)){
        return <TracksSkeleton count={10} />
    }

    if(!tracks?.length){
        return <Empty
            style={{ padding: '64px 0' }}
            imageStyle={{ width: 128 }}
            description='треков нет'
        />
    }

    return (
        <>
            <List>
                {tracks?.map((track: any, index: number) => <Track
                    key={index.toString() + track?.idTrack}
                    play={play}
                    {...track}
                    {...restProps}
                />)}
            </List>
            {/*<InfiniteScroll loadMore={loadMore} hasMore={hasMore}>*/}
            {/*    <InfiniteScrollContent hasMore={hasMore} />*/}
            {/*</InfiniteScroll>*/}
        </>

    );
});

const InfiniteScrollContent = ({ hasMore }: { hasMore?: boolean }) => {
    return (
        <>
            {hasMore ? (
                <>
                    <span>Загрузка</span>
                    <DotLoading />
                </>
            ) : (
                <span>------</span>
            )}
        </>
    )
}
