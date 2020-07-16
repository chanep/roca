angular.module('app').factory('MaterialListService', ['$http', function ($http) {

        var getAllHeadRevision = function() {
            return $http.get("MaterialList/GetAllHeadRevision")
                .then(function (data) { return data.data; });
        };

        var getRevisionHistory = function (id) {
            return $http.get("MaterialList/GetRevisionHistory/"+ id)
                .then(function (data) { return data.data; });
        };

        var get = function (id) {
            return $http.get("MaterialList/Get/"+ id)
                .then(function (data) { return data.data; });
        };

        var getFull = function(id) {
            return $http.get("MaterialList/GetFull/"+ id)
                .then(function (data) { return data.data; });
        };

        var getNewVm = function() {
            return $http.get("MaterialList/GetNewVm")
                .then(function (data) { return data.data; });
        };

        var getVm = function (id) {
            return $http.get("MaterialList/GetVm/"+ id)
                .then(function (data) { return data.data; });
        };

        var add = function (ml) {
            return $http.post("MaterialList/Add", ml)
                .then(function (data) { return data.data; });
        };

        var save = function (ml) {
            return $http.post("MaterialList/Save", ml)
                .then(function (data) { return data.data; });
        };

        var remove = function(id) {
            return $http.post("MaterialList/Delete/" + id)
                .then(function (data) { return data.data; });
        };

        var newRevision = function (prevRevisionId, newRevision) {
            return $http.post("MaterialList/NewRevision", { prevRevisionId: prevRevisionId, newRevision: newRevision })
                .then(function(data) { return data.data; });
        };

        var issue = function (id) {
            return $http.post("MaterialList/Issue/"+id)
                .then(function (data) { return data.data; });
        };

        var getAllItems = function (id) {
            return $http.get("MaterialList/GetAllItems/" + id)
                .then(function (data) { return data.data; });
        };

        var addItem = function (mlId, materialId, quantity) {
            return $http.post("MaterialList/AddItem", { mlId: mlId, materialId: materialId, quantity: quantity })
                .then(function (data) { return data.data; });
        };

        var updateItem = function (item) {
            return $http.post("MaterialList/UpdateItem", item)
                .then(function (data) { return data.data; });
        };

        var removeItem = function (id) {
            return $http.post("MaterialList/DeleteItem/"+id)
                .then(function (data) { return data.data; });
        };


        return {
            get: get,
            getFull: getFull,
            getAllHeadRevision: getAllHeadRevision,
            getRevisionHistory: getRevisionHistory,
            getNewVm: getNewVm,
            getVm: getVm,
            add: add,
            save: save,
            remove: remove,
            newRevision: newRevision,
            issue: issue,
            getAllItems: getAllItems,
            addItem: addItem,
            updateItem: updateItem,
            removeItem: removeItem
        };
    }
]);