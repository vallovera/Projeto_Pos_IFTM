angular.module('App').controller('Genero',

    function ($scope, $http) {

        var genero = [];
        var term;

        $scope.onInit = function () {
            $scope.buscaTodosGenero();
        }

        $scope.buscaTodosGenero = function () {
            $http.get('http://localhost:32666/api/Genero')
                .then(function (ResData) {
                    $scope.data = ResData.data;
                })
                .catch(function (ResData) {
                    if (ResData.data.Message !== undefined) {
                        swal({ title: 'Erro', text: ResData.data.Message, type: 'error', confirmButton: 'Ok' });
                    } else {
                        swal({ title: 'Erro', text: 'Server Error', type: 'error', confirmButton: 'Ok' });
                    }
                });
        }

        $scope.removerGenero = function () {
            var obj = {
                "CodGenero": $scope.genero.codGenero,
                "Nome": $scope.genero.Nome,
              
            };
            $http.delete('http://localhost:32666/api/Genero', obj)
                .then(function (ResData) {
                    swal('Feito!', 'O período foi ativado/desativado com sucesso.', 'success');
                    $scope.onInit();
                })
                .catch(function (ResData) {
                    if (ResData.data.Message !== undefined) {
                        swal({ title: 'Erro', text: ResData.data.Message, type: 'error', confirmButton: 'Ok' });
                    } else {
                        swal({ title: 'Erro', text: 'Server Error', type: 'error', confirmButton: 'Ok' });
                    }
                });
            $scope.limpaPeriodo();

        }

        $scope.atualizaGenero  = function () {
            var obj = {
                "CodGenero": $scope.genero.codGenero,
                "Nome": $scope.editNameAging
            };
            $http.put('http://localhost:32666/api/Genero', obj)
                .then(function (ResData) {
                    swal('Feito!', 'O Gênero foi atualizado com sucesso.', 'success');
                    
                   // $scope.genero.Nome = $scope.editNameAging;
                    $scope.buscaTodosGenero();
                 
                })
                .catch(function (ResData) {
                    if (ResData.data.Message !== undefined) {
                        swal({ title: 'Erro', text: ResData.data.Message, type: 'error', confirmButton: 'Ok' });
                    } else {
                        swal({ title: 'Erro', text: 'Server Error', type: 'error', confirmButton: 'Ok' });
                    }
                });
            $scope.limpaGenero();
            $scope.buscaTodosGenero();
        }

        $scope.salvarNovoGenero = function () {
            this.genero.codGenero = 0;

            $http.post('http://localhost:32666/api/Genero', this.genero)
                .then(function (ResData) {
                    if (ResData.data.ErroModel.Sucesso === true) {
                        swal('Feito!', 'Gênero criado com sucesso.', 'success');
                        $scope.buscaTodosPeriodos();
                    } else if (ResData.Message === "Nome do Gênero já existente") {
                        swal('Erro!', 'Gênero já existente!', 'error');
                    } else {
                        swal('Erro!', 'Ocorreu um problema ao inserir, verifique o campo "Descrição do Gênero"', 'error');
                    }
                })
                .catch(function (ResData) {
                    if (ResData.data.Message !== undefined) {
                        swal({ title: 'Erro', text: ResData.data.Message, type: 'error', confirmButton: 'Ok' });
                    } else {
                        swal({ title: 'Erro', text: 'Server Error', type: 'error', confirmButton: 'Ok' });
                    }
                });

            $scope.limpaGenero();
            $scope.buscaTodosGenero();
        }

        $scope.setClickedRow = function (index) {
            $scope.genero = index;
        }
        $scope.limpaGenero = function () {
            $scope.genero = undefined;
        }
        $scope.onInit();
    }
);
