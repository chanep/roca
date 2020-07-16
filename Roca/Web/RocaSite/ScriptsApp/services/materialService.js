angular.module('app').factory('MaterialService', ['$http', function ($http) {

    var getAll = function (filter) {
        return $http.get("Material/GetAll", { params:  filter })
                .then(function (data) { return data.data; });
    };

    var getAllForMl = function (filter) {
        return $http.get("Material/GetAllForMl", { params: filter })
                .then(function (data) { return data.data; });
    };


    return {
        getAll: getAll,
        getAllForMl: getAllForMl
    };
}
]);