import React from 'react';
import '../styles/globals.css'
import App, {AppInitialProps, AppContext} from 'next/app';
import {END} from 'redux-saga';
import {SagaStore, wrapper} from '../store';
import {useAudio} from "../hooks/useAudio";

const WrapperAudio = () => {
    useAudio()
    return <div/>
}

class WrappedApp extends App<AppInitialProps> {
    public static getInitialProps = wrapper.getInitialAppProps(store => async context => {
        // 1. Wait for all page actions to dispatch
        const pageProps = {
            ...(await App.getInitialProps(context)).pageProps,
        };

        // 2. Stop the saga if on server
        if (context.ctx.req) {
            store.dispatch(END);
            await (store as SagaStore).sagaTask.toPromise();
        }

        // 3. Return props
        return {pageProps};
    });

    public render() {
        const {Component, pageProps} = this.props;
        return (<>
            <Component {...pageProps} />
            <WrapperAudio/>
        </>);
    }
}

export default wrapper.withRedux(WrappedApp);