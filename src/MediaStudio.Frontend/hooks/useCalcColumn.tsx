import {useEffect, useState} from "react";
const   columnGap = 16,
        minColumnWidth = 164

const useCalcColumn = (myRef) => {
    const [column, setColumn] = useState(8)
    const [albumWidth, setAlbumWidth] = useState(164)
    const style = {"--minColumnWidth": `${minColumnWidth}px`, "gridColumnGap": `${columnGap}px`};

    const handleResize = () => {
        const widthRef: number = myRef.current.offsetWidth;
        let albumCount: number = Math.floor((widthRef + columnGap) / (minColumnWidth + columnGap));
        let itemWidth = (widthRef - albumCount * columnGap) / albumCount

        if(widthRef < 500) albumCount = 8

        setColumn(albumCount)
        setAlbumWidth(itemWidth)
    }

    useEffect(() => {
        handleResize()
    }, [])

    useEffect(() => {
        window.addEventListener('resize', handleResize)

        return () => {
            window.removeEventListener('resize', handleResize)
        }
    }, [myRef])



    return { style, column, albumWidth}
}

export default useCalcColumn
