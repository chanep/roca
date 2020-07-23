angular.module('app').factory('Utils', [ '$filter' , function ($filter) {

    var isNullOrUndefined = function (obj) {
        if (angular.isUndefined(obj) || obj === null) return true;
        return false;
    }

    var findById = function (objects, id) {
        return findBy(objects, 'Id', id);
    }

    var findBy = function (objects, key, value) {
        var aux = objects.filter(function (e) {
            return e[key] == value;
        });
        if (aux.length == 0) return null;
        return aux[0];
    }

    var filterBy = function (objects, key, value) {
        var aux = objects.filter(function (e) {
            return e[key] == value;
        });
        return aux;
    }

    var orderBy = function(objects, key) {
        return $filter('orderBy')(objects, key);
    }
    
    var aspDateToStr = function(aspDate, format) {
        return $filter('jsonDate')(aspDate, format);
    }

    var getNowTime = function() {
        return new Date();
    }

    var getDatePart = function (date) {
        if (typeof date == 'number') {
            date = new Date(date);
        }
        if (angular.isDate(date)) {
            return new Date(date.getFullYear(), date.getMonth(), date.getDate());
        }
        throw new Error("getDatePart expected a Date() or number and was " + date);

    }

    function isValidNumber(value) {
        if (isNullOrUndefined(value))
            return false;
        value = value.replace(",", ".");
        return !isNaN(value);
    }

    function isValidInt(value) {
        if (isNullOrUndefined(value))
            return false;
        if (value.match(/[,.]/))
            return false;
        return !isNaN(value);
    }
    

    return {
        isNullOrUndefined: isNullOrUndefined,
        isValidNumber: isValidNumber,
        isValidInt: isValidInt,
        findBy: findBy,
        findById: findById,
        filterBy: filterBy,
        orderBy: orderBy,
        aspDateToStr: aspDateToStr,
        getNowTime: getNowTime,
        getDatePart: getDatePart
    };
}]
);