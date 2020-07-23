angular.module('app').factory('UserService', ['$http', '$rootScope', function($http, $rootScope) {

        var currentUser = null;

        var getCurrentUser = function() {
            return $http.get("User/GetCurrentUser")
                .then(function(data) {
                    if (data.data === "")
                        data.data = null;
                    currentUser = data.data;
                    return data.data;
                });
        };

        var isInRole = function(role) {
            if (currentUser == null)
                return false;
            var index = currentUser.RoleList.indexOf(role);
            if (index < 0)
                return false;
            return true;
        }

        var isFormsMode = function() {
            return $http.get("User/IsFormsMode")
                .then(function(data) {
                    return data.data.Result;
                });
        };

        var getAllUsers = function() {
            return $http.get("User/GetAllUsers", { cache: true })
                .then(function(data) { return data.data; });
        };


        var impersonateUser = function(id) {
            return $http.post("User/ImpersonateUser", { id: id })
                .then(function() {
                    userLogged();
                    return;
                });
        };

        var authenticate = function(username, password) {
            return $http.post("User/Authenticate", { username: username, password: password })
                .then(function(data) {
                    if (data.data.Result) {
                        userLogged();
                    }
                    return data.data.Result;
                });
        };

        function userLogged() {
            $rootScope.$broadcast('user.userLogged');
        }

        var onUserLogged = function(callback) {
            return $rootScope.$on('user.userLogged', function(event) {
                callback(event);
            });
        }


        return {
            getCurrentUser: getCurrentUser,
            isFormsMode: isFormsMode,
            isInRole: isInRole,
            getAllUsers: getAllUsers,
            impersonateUser: impersonateUser,
            authenticate: authenticate,
            onUserLogged: onUserLogged
        };
    }
]);