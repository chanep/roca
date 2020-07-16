angular.module('app').factory('TaggableService', ['$http', function ($http) {

    var getTypes = function (specialtyId) {
        return $http.get("Taggable/GetTypes", { params: { specialtyId: specialtyId} })
                .then(function (data) { return data.data; });
    };


    var getSpecialties = function () {
        return $http.get("Login/GetLoggedUser")
                .then(function (data) { return data.data.Specialties; });
    };

    var addType = function (type) {
        return $http.post("Taggable/AddType", type)
                .then(function (data) { return data.data; });
    };

    var deleteType = function (type) {
        return $http.post("Taggable/DeleteType", type)
                .then(function (data) { return data.data; });
    };

    var updateType = function (type) {
        return $http.post("Taggable/UpdateType", type)
                .then(function (data) { return data.data; });
    };

    var saveAttributes = function (attributes) {
        return $http.post("Taggable/SaveAttributes", attributes)
                .then(function (data) { return data.data; });
    };

    return {
        getTypes: getTypes,
        getSpecialties: getSpecialties,
        addType: addType,
        saveAttributes: saveAttributes,
        deleteType: deleteType,
        updateType: updateType
    };
}
]);