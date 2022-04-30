import React, {createContext} from 'react';
import {BrowserRouter} from "react-router-dom";
import {QueryClient, QueryClientProvider} from "react-query";
import useMobileDetect from "./hooks/useUserAgent";
import AppRouter from "./router/AppRouter";
import {AppWrapper} from "./components/AppWrapper";
import {useAuth} from "./hooks/auth.hook";
import {useAudio} from "./hooks/audio.hook";
import AudioContext from "./context/audioContext";
import AuthContext from './context/authContext';
import {AutoCenter, ConfigProvider} from "antd-mobile";
import {locale} from "./settings/locale.ru";

const queryClient = new QueryClient()

function App() {
    const {isMobile}: any = useMobileDetect()

    const data = useAuth()
    const audio: any = useAudio()

    if(!data?.ready){
        return <AutoCenter>loading...</AutoCenter>
    }

    return (
      <QueryClientProvider client={queryClient} contextSharing={true}>
          <AuthContext.Provider value={data}>
              <ConfigProvider locale={locale} >
                  <AudioContext.Provider value={audio}>
                      <BrowserRouter basename={'/'}>
                          <AppWrapper isMobile={isMobile}>
                              <AppRouter isMobile={isMobile}/>
                          </AppWrapper>
                      </BrowserRouter>
                  </AudioContext.Provider>
              </ConfigProvider>
          </AuthContext.Provider>
      </QueryClientProvider>
    );
}

export default App;