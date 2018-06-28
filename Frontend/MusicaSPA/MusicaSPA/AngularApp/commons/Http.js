angular.module('MartinsApp')
 .factory('timeoutHttpIntercept', function ($rootScope, $q) {
     return {
         'request': function (config) {
             config.timeout = 3000;
             return config;
         }
     };
 });