angular.module('app').factory('MatPipingService', ['$http', function ($http) {

    var importFromAp = function (projectId) {
        return $http.post("MatPiping/ImportFromAp", { projectId: projectId })
            .then(function (data) { return data.data; });
    };

    var getAutosuggest = function (property, term, filter) {
        var filterStr = angular.toJson(filter);
        return $http.get("MatPiping/GetAutosuggest", { params: { property: property, term: term, filterStr: filterStr} })
                    .then(function (data) { return data.data; });
    };

    var getAll = function (filter) {
        return $http.post("MatPiping/GetAll", { filter: filter })
            .then(function (data) {
                if (data.data == "") return null;
                return data.data;
        });
    };


    return {
        importFromAp: importFromAp,
        getAutosuggest: getAutosuggest,
        getAll: getAll
    };
}
]);