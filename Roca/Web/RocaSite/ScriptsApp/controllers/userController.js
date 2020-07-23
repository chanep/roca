angular.module('app').controller('User.Menu.Controller', ['$scope', 'UserService', function ($scope,  userService) {

    $scope.Model = {};
    var m = $scope.Model;
    m.ShowLogin = false;

    refreshUser();

    userService.isFormsMode()
        .then(function(data) {
            m.ShowLogin = data;
        });

    $scope.isInRole = userService.isInRole;

    userService.onUserLogged(refreshUser);

    function refreshUser() {
        userService.getCurrentUser().then(function (data) {
            if (data) {
                m.User = data;
                $scope.RootModel.UserName = data.LongUserName;
            }
        });
    }

} ])
.controller('User.Welcome.Controller', ['$scope', '$state', 'UserService', function ($scope, $state, userService) {
    $scope.Model = {};
    var m = $scope.Model;
    m.WelcomeText = "";

    userService.getCurrentUser().then(function(data) {
        if (data) {
            m.WelcomeText = "Bienvenido " + data.FullName + "!";
        } else {
            m.WelcomeText = "";
            $state.go('login');
        }
    });
} ])
.controller('User.Impersonate.Controller', ['$scope', 'UserService', function ($scope,  userService) {

    $scope.Model = {};
    var m = $scope.Model;

    userService.getCurrentUser().then(function (data) {
        m.UserId = data.Id;
    });

    userService.getAllUsers().then(function (data) {
        m.Users = data;
    });

    $scope.userChanged = function () {
        userService.impersonateUser(m.UserId);
    };

} ])
.controller('User.Login.Controller', ['$scope', '$state', 'UserService', function ($scope, $state, userService) {

    $scope.Model = {};
    var m = $scope.Model;
    m.Users = {};
    m.LoginFailed = false;

    $scope.submit = function() {
        userService.authenticate(m.User.Name, m.User.Password)
        .then(function (data) {
            if (data) {
                $state.go('home');
            } else {
                m.LoginFailed = true;
            }
        });
    }


} ])

;