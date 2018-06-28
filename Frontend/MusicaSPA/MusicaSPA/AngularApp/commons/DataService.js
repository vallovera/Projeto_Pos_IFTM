angular.module('MartinsApp').factory('DataService', function () {
    return {
        TraduzData: function ($scope) {
            $scope.currentTime = new Date();
            $scope.month = ['Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'];
            $scope.monthShort = ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun', 'Jul', 'Ago', 'Set', 'Out', 'Nov', 'Dez'];
            $scope.weekdaysFull = ['Domingo', 'Segunda', 'Terça', 'Quarta', 'Quinta', 'Sexta', 'Sabado'];
            $scope.weekdaysLetter = ['D', 'S', 'T', 'Q', 'Q', 'S', 'S'];
            $scope.today = 'Hoje';
            $scope.clear = 'Limpar';
            $scope.close = 'Fechar';
            
        }
    };
});
