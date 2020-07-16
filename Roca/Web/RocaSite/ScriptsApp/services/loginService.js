angular.module('app').factory('LoginService', ['$http', function ($http) {

    var getLoggedUser = function () {
        return $http.get("Login/GetLoggedUser")
                .then(function (data) { return data.data; });
    };

    var getAllUsers = function () {
        return $http.get("Login/GetAllUsers")
                .then(function (data) { return data.data; });
    };


    var impersonateUser = function (id) {
        return $http.post("Login/ImpersonateUser", { id: id });
    };



    return {
        getLoggedUser: getLoggedUser,
        getAllUsers: getAllUsers,
        impersonateUser: impersonateUser
    };
}
]);