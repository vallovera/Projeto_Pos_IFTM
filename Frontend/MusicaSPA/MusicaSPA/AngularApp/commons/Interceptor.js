angular.module('MartinsApp').factory('sessionInjector', function (LocalStorageService) {
    var sessionInjector = {
        request: function (config) {
            //if (!LocalStorageService.TOKEN != undefined) {
                config.headers['Access-Control-Allow-Origin'] = '*';
                config.headers['x-session-token'] = LocalStorageService.getToken();
                config.headers['x-codigo-conta'] = LocalStorageService.getCodConta();
                config.headers['x-codigo-unidade'] = LocalStorageService.getCodUnidade();
                config.headers['x-descricao-conta'] = LocalStorageService.getDescricaoConta();
                config.headers['x-descricao-unidade'] = LocalStorageService.getDescricaoUnidade();
            //}
            return config;
        }
    };
    return sessionInjector;
});
