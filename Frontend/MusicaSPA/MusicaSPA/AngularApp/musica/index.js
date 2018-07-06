angular.module('App').controller('Musica',

    function ($scope, $http) {

        var musica = [];
        var term;
        var listaGeneros = [];

        $scope.onInit = function () {
            $scope.busca();
            $scope.buscaTodosGenero();
        }
        
        $scope.busca = function () {
            $http.get('http://localhost:32666/api/Musica')
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

        $scope.buscaTodosGenero = function () {
            $http.get('http://localhost:32666/api/Genero')
                .then(function (ResData) {
                    $scope.listaGeneros = ResData.data;
                })
                .catch(function (ResData) {
                    if (ResData.data.Message !== undefined) {
                        swal({ title: 'Erro', text: ResData.data.Message, type: 'error', confirmButton: 'Ok' });
                    } else {
                        swal({ title: 'Erro', text: 'Server Error', type: 'error', confirmButton: 'Ok' });
                    }
                });
        }

        $scope.remover = function () {
            var obj = {
                "CodMusica": $scope.musica.codmusica,
                "Nome": $scope.musica.Nome,

            };
            $http.delete('http://localhost:32666/api/Musica', obj)
                .then(function (ResData) {
                    swal('Feito!', 'Musica Removida com Sucesso!.', 'success');
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

        $scope.atualizamusica = function () {
            var obj = {
                "codMusica": $scope.musica.codmusica,
                "nome": $scope.editNameAging
            };
            $http.put('http://localhost:32666/api/musica', obj)
                .then(function (ResData) {
                    swal('Feito!', 'O Música foi atualizado com sucesso.', 'success');
                    $scope.buscaTodosmusica();
                    $scope.musica.Nome = $scope.editNameAging;

                })
                .catch(function (ResData) {
                    if (ResData.data.Message !== undefined) {
                        swal({ title: 'Erro', text: ResData.data.Message, type: 'error', confirmButton: 'Ok' });
                    } else {
                        swal({ title: 'Erro', text: 'Server Error', type: 'error', confirmButton: 'Ok' });
                    }
                });
            $scope.limpamusica();
        }

        $scope.salvarNovomusica = function () {
            $scope.musica.codMusica = 0;

            $http.post('http://localhost:32666/api/musica', this.musica)
                .then(function (ResData) {
                    if (ResData.data.ErroModel.Sucesso === true) {
                        swal('Feito!', 'Música criado com sucesso.', 'success');
                        $scope.buscaTodosPeriodos();
                    } else if (ResData.Message === "Nome do Música já existente") {
                        swal('Erro!', 'Música já existente!', 'error');
                    } else {
                        swal('Erro!', 'Ocorreu um problema ao inserir, verifique o campo "Descrição do Música"', 'error');
                    }
                })
                .catch(function (ResData) {
                    if (ResData.data.Message !== undefined) {
                        swal({ title: 'Erro', text: ResData.data.Message, type: 'error', confirmButton: 'Ok' });
                    } else {
                        swal({ title: 'Erro', text: 'Server Error', type: 'error', confirmButton: 'Ok' });
                    }
                });

            $scope.limpamusica();
        }

        $scope.setClickedRow = function (index) {
            $scope.musica = index;
        }
        $scope.limpamusica = function () {
            $scope.musica = undefined;
        }
        $scope.onInit();
    }
);
