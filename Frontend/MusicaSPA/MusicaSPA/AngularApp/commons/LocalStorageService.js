angular.module("MartinsApp").factory("LocalStorageService", function () {

    var _getCodUnidade = function () {
        return window.localStorage.getItem('EMPRESA_KEY');
    };

    var _getDescricaoUnidade = function () {
        return window.localStorage.getItem('DESC_UNIDADE');
    };

    var _getCodConta = function () {
        return window.localStorage.getItem('CONTA_KEY');
    };
   
    var _getDescricaoConta = function () {
        return window.localStorage.getItem('DESC_CONTA');
    };

    var _getToken = function () {
        return window.localStorage.getItem('jwt_token');
    };

    var _getMenu = function () {
        return window.localStorage.getItem('MENU');
    };

    var _getUsuario = function () {
        return JSON.parse(window.localStorage.getItem('user'));
    };

    var _getUrlBack = function () {
        return window.localStorage.getItem('URL_BACK');
    };

    var _getUrlFront = function () {
        return window.localStorage.getItem('URL_FRONT');
    };

    return {
        getCodUnidade: _getCodUnidade,
        getDescricaoUnidade: _getDescricaoUnidade,
        getCodConta: _getCodConta,
        getDescricaoConta: _getDescricaoConta,
        getToken: _getToken,
        getMenu: _getMenu,
        getUsuario: _getUsuario,
        getUrlFront:_getUrlFront,
        getUrlBack: _getUrlBack
    };
});