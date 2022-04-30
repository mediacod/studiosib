interface IType {
    getService: () => any;
    setToken: (tokenObj: any) => void;
    getAccessToken: () => string | null;
    getRefreshToken: () => string | null;
    clearToken: () => void;
}

export const LocalStorageService = (function(){
    let _service: any;
    function _getService(this: IType) {
        if(!_service) {
            _service = this;
            return _service
        }
        return _service
    }
    function _setToken(tokenObj: any) {
        localStorage.setItem('accessToken', tokenObj.accessToken);
        localStorage.setItem('refreshToken', tokenObj.refreshToken);
    }
    function _getToken() {
        return {
            accessToken: _getAccessToken(),
            refreshToken: _getRefreshToken()
        }
    }
    function _getAccessToken() {
        return localStorage.getItem('accessToken');
    }
    function _getRefreshToken() {
        return localStorage.getItem('refreshToken');
    }
    function _clearToken() {
        localStorage.removeItem('accessToken');
        localStorage.removeItem('refreshToken');
    }
    return {
        getService : _getService,
        setToken : _setToken,
        getToken : _getToken,
        getAccessToken : _getAccessToken,
        getRefreshToken : _getRefreshToken,
        clearToken : _clearToken
    }

})();