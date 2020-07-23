angular.module('app').factory('BasService', [
    '$http', '$q', 'FileService', function($http, $q, fileService) {

        var getAllCodesByField = function(fieldCode, nocache) {
            var cache = false;
            if (angular.isUndefined(nocache) || nocache == null || nocache === false)
                cache = true;
            return $http.get("Bas/GetAllCodesByField", { params: { fieldCode: fieldCode }, cache: cache })
                .then(function(data) { return data.data; });
        };

        var getAllCodesByFields = function(fieldCodes) {
            return $http.get("Bas/GetAllCodesByFields", { params: { fieldCodes: fieldCodes } })
                .then(function(data) { return data.data; });
        };

        var getAllElementTypes = function() {
            return $http.get("Bas/GetAllElementTypes", { cache: true })
                .then(function(data) { return data.data; });
        };

        var getElement = function(id) {
            return $http.get("Bas/GetElement/" + id)
                .then(function(data) { return data.data; });
        };

        var getEmptyElement = function(typeId) {
            return $http.get("Bas/GetEmptyElement", { params: { typeId: typeId }, cache: true })
                .then(function(data) { return data.data; });
        };

        var getAllElements = function(filter) {
            return $http.get("Bas/GetAllElements", { params: filter })
                .then(function(data) { return data.data; });
        };

        var getSisepcCatalog = function(filter) {
            return $http.get("Bas/GetSisepcCatalog", { params: filter, responseType: "arraybuffer" })
                .then(function(data) {
                    fileService.downloadFile(data);
            });
        };

        var saveElement = function(element) {
            return $http.post("Bas/SaveElement", element)
                .then(function(data) {
                    return data.data;
                }, function(data) {
                    return $q.reject(data.data);
                });
        }

        var deleteElement = function(id) {
            return $http.post("Bas/DeleteElement/" + id)
                .then(function(data) {
                    return data.data;
                });
        }

        var addCode = function(bas) {
            return $http.post("Bas/AddCode", bas)
                .then(function(data) {
                    return data.data;
                });
        };

        var deleteCode = function(id) {
            return $http.post("Bas/DeleteCode/" + id)
                .then(function(data) {
                    return data.data;
                });
        };

        var updateCode = function(bas) {
            return $http.post("Bas/UpdateCode", bas)
                .then(function(data) {
                    return data.data;
                }, function(data) {
                    return $q.reject(data.data);
                });
        };

        return {
            getAllCodesByField: getAllCodesByField,
            getAllCodesByFields: getAllCodesByFields,
            getAllElementTypes: getAllElementTypes,
            getElement: getElement,
            getEmptyElement: getEmptyElement,
            getAllElements: getAllElements,
            getSisepcCatalog: getSisepcCatalog,
            saveElement: saveElement,
            deleteElement: deleteElement,
            addCode: addCode,
            deleteCode: deleteCode,
            updateCode: updateCode
        };
    }
]);