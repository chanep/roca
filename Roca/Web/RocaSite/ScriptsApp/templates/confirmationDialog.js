angular.module('app').controller('ConfirmationDialogController', 
     ['$scope', '$modalInstance', 'title', 'question', 'fields', function ($scope, $modalInstance, title, question, fields) {
        $scope.Question = question;
        $scope.Title = title;
        $scope.Fields = fields;

        $scope.ok = function() {
            $modalInstance.close($scope.Fields);
        };

        $scope.cancel = function() {
            $modalInstance.dismiss('cancel');
        };
    }
]);