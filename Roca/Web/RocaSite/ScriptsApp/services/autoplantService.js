angular.module('app').factory('AutoplantService', ['$http', function ($http) {

    var currentProjectId = 0;

    var getProjects = function () {
        return $http.get("Autoplant/GetProjects", { cache: true })
            .then(function (data) { return data.data; });
    };

    var getAreas = function (projId) {
        return $http.get("Autoplant/GetAreas", { cache: true, params: { projId: projId } })
            .then(function (data) { return data.data; });
    };

    var getMaterials = function (filters) {
        return $http.get("Autoplant/GetMaterials", { params: filters })
            .then(function (data) { return data.data; });
    };

    var getAutosuggest = function (property, term, filtersStr) {
        return $http.get("Autoplant/GetAutosuggest", { params: { property: property, term: term, filtersStr: filtersStr } });
    };


    return {
        currentProjectId : currentProjectId,
        getProjects: getProjects,
        getAreas: getAreas,
        getMaterials: getMaterials,
        getAutosuggest: getAutosuggest
    };
}
]);