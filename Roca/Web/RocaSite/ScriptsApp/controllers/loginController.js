angular.module('app').controller('Login.Impersonate.Controller', ['$scope', 'LoginService', function ($scope,  loginService) {

    $scope.Model = {};
    var m = $scope.Model;

    loginService.getLoggedUser().then(function (data) {
        m.UserId = data.Id;
    });

    loginService.getAllUsers().then(function (data) {
        m.Users = data;
    });

    $scope.userChanged = function () {
        loginService.impersonateUser(m.UserId);
    };

}]);