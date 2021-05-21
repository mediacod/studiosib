import { useEffect, useState } from 'react'

const getMobileDetect = (userAgent: NavigatorID['userAgent']) => {
    const isAndroid = () => Boolean(userAgent.match(/Android/i))
    const isIos = () => Boolean(userAgent.match(/iPhone|iPad|iPod/i))
    const isOpera = () => Boolean(userAgent.match(/Opera Mini/i))
    const isWindows = () => Boolean(userAgent.match(/IEMobile/i))
    const isSSR = () => Boolean(userAgent.match(/SSR/i))
    const isMobile = () => Boolean(isAndroid() || isIos() || isOpera() || isWindows())
    const isDesktop = () => Boolean(!isMobile() && !isSSR())
    return {
        isMobile,
        isDesktop,
        isAndroid,
        isIos,
        isSSR,
    }
}

const useMobileDetect = () => {
    const [isMobile, setIsMobile] = useState(false);
    useEffect(() => {}, [])
    const userAgent = typeof navigator === 'undefined' ? 'SSR' : navigator.userAgent

    // const currentDevice = useMobileDetect()

    const currentDevice = getMobileDetect(userAgent)
    const flag = currentDevice.isMobile()

    useEffect(()=>{
        setIsMobile(flag)
    },[flag])

    return {isMobile}

    // return getMobileDetect(userAgent)
}

export default useMobileDetect
