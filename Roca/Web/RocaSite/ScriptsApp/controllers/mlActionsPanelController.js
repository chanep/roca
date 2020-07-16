angular.module('app')
.controller('MlActionsPanelController', ['$scope', 'MaterialListService', 'DialogService', '$location', function($scope, materialListService, dialogService, $location) {
        
        $scope.confirmDelete = function() {
            var title = "Eliminar Lista de Materiales";
            var question = "Esta seguro que desea eliminar la Lista de Materiales?";

            dialogService.confirmationDialog(title, question)
                .then(function(result) {
                    return materialListService.remove($scope.Id);
                })
                .then(function(data) {
                    $location.path('MaterialList/List');
            });
        }

        $scope.confirmIssue = function() {
            var title = "Emitir Lista de Materiales";
            var question = "Esta seguro que desea emitir la Lista de Materiales?";
            dialogService.confirmationDialog(title, question)
                .then(function(result) {
                    return materialListService.issue($scope.Id);
                })
                .then(function(data) {
                    $location.path('MaterialList/View/' + $scope.Id);
                });
        }

        $scope.confirmNewRevision = function () {
            var title = "Nueva Revision Lista de Materiales";
            var question = "";
            var fields = [{Label: "Revision", Field: "Revision", Value: "A"}];

            dialogService.confirmationDialog(title, question, fields)
                .then(function(result) {
                    var newRevision = result[0].Value;
                    return materialListService.newRevision($scope.Id, newRevision);
                })
                .then(function(data) {
                    $location.path('MaterialList/Edit/' + data.Id);
                });
        }
    }
]);