angular.module('MartinsApp').factory('ModuloService', function () {
  return {
    atualizaMenu: function ($scope,valor) {
      $scope.currentUser.menu = valor;
    }
    //,
    //logout = function () {
    //  localStorage.removeItem('jwt_token');
    //  localStorage.removeItem('user');
    //}
  };
});
