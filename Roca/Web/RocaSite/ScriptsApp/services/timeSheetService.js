angular.module('app').factory('TimeSheetService', ['$http', 'TimeSheetFactory', function($http, timeSheetFactory) {

        var getFull = function(id) {
            return $http.get("TimeSheet/GetFull/" + id)
                .then(function(data) {
                    return timeSheetFactory.createTimeSheet(data.data);
            });
        };

        var createEmptyTimeSheet = function() {
            return timeSheetFactory.createEmptyTimeSheet();
        };

        var getByDate = function(controlDate, specialtyId) {
            return $http.get("TimeSheet/GetByDate", { params: { controlDate: controlDate, specialtyId: specialtyId } })
                .then(function(data) {
                    return timeSheetFactory.createTimeSheet(data.data);
            });
        };

        var getLast = function (specialtyId) {
            return $http.get("TimeSheet/GetLast", { params: { specialtyId: specialtyId} })
                .then(function (data) {
                    if (data.data !== '')
                        return timeSheetFactory.createTimeSheet(data.data);
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

        var getByDocUserReport = function (filter) {
            return $http.get("TimeSheet/GetByDocUserReport", { params: filter })
                .then(function (data) { return data.data; });
        };

        var getByTaskReport = function (filter) {
            return $http.get("TimeSheet/GetByTaskReport", { params: filter })
                .then(function (data) { return data.data; });
        };

        var getByTaskUserReport = function (filter) {
            return $http.get("TimeSheet/GetByTaskUserReport", { params: filter })
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

        var getDetailsOptions = function(timeSheetId) {        
            return $http.get("TimeSheet/GetDetailsOptions", {cache: true, params: { timeSheetId: timeSheetId} })
                .then(function(data) { return data.data; });
        };

        var getListOptions = function () {
            return $http.get("TimeSheet/GetListOptions", { cache: true })
                .then(function (data) { return data.data; });
        };

        var save = function(timeSheet) {
            return $http.post("TimeSheet/Save", timeSheet.getDto())
                .then(function(data) {
                    return timeSheetFactory.createTimeSheet(data.data);
            });
        };

        var update = function (timeSheet) {
            return $http.post("TimeSheet/Update", timeSheet)
                .then(function(data) {
                    return timeSheetFactory.createTimeSheet(data.data);
            });
        };

        var updateAll = function (timeSheets) {
            return $http.post("TimeSheet/UpdateAll", timeSheets)
                .then(function (data) { return data.data; });
        };

        return {
            getFull: getFull,
            createEmptyTimeSheet: createEmptyTimeSheet,
            getByDate: getByDate,
            getLast: getLast,
            getDefaulters: getDefaulters,
            getAll: getAll,
            getByDocReport: getByDocReport,
            getByDocUserReport: getByDocUserReport,
            getByTaskReport: getByTaskReport,
            getByTaskUserReport: getByTaskUserReport,
            getBySpecialtyReport: getBySpecialtyReport,
            getByProjectReport: getByProjectReport,
            getDetailsOptions: getDetailsOptions,
            getListOptions: getListOptions,
            save: save,
            update: update,
            updateAll: updateAll
        };
    }
]);