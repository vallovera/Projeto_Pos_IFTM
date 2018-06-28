angular.module('MartinsApp').config(function ($httpProvider) {
  $httpProvider.interceptors.push('sessionInjector');

});
