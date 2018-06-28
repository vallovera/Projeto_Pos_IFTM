var app = angular.module('App', ['ui.materialize']).constant('config', {
  menu:"nenhum"
}).run(function ($rootScope) {
  $rootScope.mySetting = 42;
});


