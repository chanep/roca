angular.module('app').factory('DocumentService', ['$http', '$q', function($http, $q) {

        var getAutosuggestDoc = function(property, term, filtersStr) {
            return $http.get("Document/GetAutosuggestDoc", { params: { property: property, term: term, filtersStr: filtersStr } })
                .then(function(data) { return data.data; });
        };

        var getDocument = function(filter) {
            return $http.get("Document/GetDocument", { params: filter })
                .then(function(data) {
                    if (data.data !== '')
                        return data.data;
                    return null;
                });
            };

        var add = function (document) {
            return $http.post("Document/Add", document)
            .then(function(data) {
                     return data.data;
                }, function(data) {
                    return $q.reject(data.data);
            });
        };

        var update = function (document) {
            return $http.post("Document/Update", document)
            .then(function (data) { return data.data; });
        };

        var getOptions = function () {
            return $http.get("Document/GetOptions", { cache: true })
                .then(function (data) { return data.data; });
        };

        return {
            getAutosuggestDoc: getAutosuggestDoc,
            getDocument: getDocument,
            add: add,
            update: update,
            getOptions: getOptions
        };
    }
]);