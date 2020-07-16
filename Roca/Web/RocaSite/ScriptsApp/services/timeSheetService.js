angular.module('app').factory('TimeSheetService', [
    '$http', function($http) {

        var getFull = function(id) {
            return $http.get("TimeSheet/GetFull/" + id)
                .then(function(data) { return data.data; });
        };

        var getByDate = function(controlDate, specialtyId) {
            return $http.get("TimeSheet/GetByDate", { params: { controlDate: controlDate, specialtyId: specialtyId } })
                .then(function(data) { return data.data; });
        };

        var getLast = function (specialtyId) {
            return $http.get("TimeSheet/GetLast", { params: { specialtyId: specialtyId} })
                .then(function (data) {
                    if (data.data !== '')
                        return data.data;
                    return null;
                });
            };

        var getDefaulters = function() {
            return $http.get("TimeSheet/GetDefaulters")
                .then(function(data) { return data.data; });
        };

        var getByDocReport = function (filter) {
            return $http.get("TimeSheet/GetByDocReport", { params: filter })
                .then(function (data) { return data.data; });
        };

        var getByTaskReport = function (filter) {
            return $http.get("TimeSheet/GetByTaskReport", { params: filter })
                .then(function (data) { return data.data; });
        };

        var getBySpecialtyReport = function (filter) {
            return $http.get("TimeSheet/GetBySpecialtyReport", { params: filter })
                .then(function (data) { return data.data; });
        };

        var getByProjectReport = function (filter) {
            return $http.get("TimeSheet/GetByProjectReport", { params: filter })
                .then(function (data) { return data.data; });
        };

        var getAll = function (filter) {
            return $http.get("TimeSheet/GetAll", { params: filter })
                .then(function (data) { return data.data; });
        };

        var getDetailsOptions = function() {
            return $http.get("TimeSheet/GetDetailsOptions")
                .then(function(data) { return data.data; });
        };

        var getListOptions = function () {
            return $http.get("TimeSheet/GetListOptions")
                .then(function (data) { return data.data; });
        };


        var getAutosuggestDoc = function(property, term, filtersStr) {
            return $http.get("TimeSheet/GetAutosuggestDoc", { params: { property: property, term: term, filtersStr: filtersStr} })
                .then(function (data) { return data.data; });
        };

        var getDocument = function(filter) {
            return $http.get("TimeSheet/GetDocument", { params: filter })
                .then(function (data) {
                    if(data.data !== '')
                        return data.data;
                    return null;
            });
        };

        var save = function(timeSheet) {
            return $http.post("TimeSheet/Save", timeSheet)
                .then(function(data) { return data.data; });
        };

        var update = function (timeSheet) {
            return $http.post("TimeSheet/Update", timeSheet)
                .then(function (data) { return data.data; });
        };

        var updateAll = function (timeSheets) {
            return $http.post("TimeSheet/UpdateAll", timeSheets)
                .then(function (data) { return data.data; });
        };

        return {
            getFull: getFull,
            getByDate: getByDate,
            getLast: getLast,
            getDefaulters: getDefaulters,
            getAll: getAll,
            getByDocReport: getByDocReport,
            getByTaskReport: getByTaskReport,
            getBySpecialtyReport: getBySpecialtyReport,
            getByProjectReport: getByProjectReport,
            getDetailsOptions: getDetailsOptions,
            getListOptions: getListOptions,
            getAutosuggestDoc: getAutosuggestDoc,
            getDocument: getDocument,
            save: save,
            update: update,
            updateAll: updateAll,
        };
    }
]);