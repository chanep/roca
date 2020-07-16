angular.module('app').factory('GridService', function () {

    var tooltipTemplate = function () {
        return '<div class="ngCellText colt{{$index}}" my-tooltip>{{row.getProperty(col.field)}}</div>';
    };

    var decimalEditableCellTemplate = function () {
        return "<input ng-class=\"'colt' + col.index\" type='number' step='any' min='0' ng-input=\"COL_FIELD\" ng-model=\"COL_FIELD\" />";
    };

    var boolEditableCellTemplate = function (trueValue, falseValue, changeCallback) {
        var values = "";
        var changeCallbackText = "";
        if (angular.isDefined(trueValue) && angular.isDefined(falseValue) && trueValue != null && falseValue != null) {
            values = ' ng-true-value="' + trueValue + '" ng-false-value="' + falseValue + '" ' + 'ng-checked="COL_FIELD.toString() == \'' + trueValue +'\'" ';
        }
        if (angular.isDefined(changeCallback) && changeCallback != null) {
            changeCallbackText = ' ng-change="' + changeCallback + '(row.entity)" ';
        }
        return '<div style="text-align: center;" class="ngCellText colt{{$index}}"><input ng-class="colt{{$index}}" type="checkbox" ng-input="COL_FIELD" ng-model="COL_FIELD"' + values + changeCallbackText + ' /></div>';
    };

    var mailLinkTemplate = function () {
        return '<div class="ngCellText colt{{$index}}"><a href="mailto:{{row.getProperty(col.field)}}">{{row.getProperty(col.field)}}</a></div>';
    };


    var viewLinkTemplate = function (url) {
        return '<div class="ngCellText colt{{$index}}"><a href="' + url + '{{row.getProperty(col.field)}}"><i title="Abrir" class="glyphicon glyphicon-folder-open"></i></a></div>';
    };

    var editLinkTemplate = function (url) {
        return '<div class="ngCellText colt{{$index}}"><a href="' + url + '{{row.getProperty(col.field)}}"><i title="Editar" class="glyphicon glyphicon-pencil"></i></a></div>';
    };

    var deleteLinkTemplate = function (url) {
        return '<div class="ngCellText colt{{$index}}"><a href="' + url + '{{row.getProperty(col.field)}}"><i title="Eliminar" class="glyphicon glyphicon-remove-circle"></i></a></div>';
    };

    var addFunctionTemplate = function (callback) {
        return '<div class="ngCellText colt{{$index}}"><a ng-click="$parent.' + callback + '(row.entity)"><i title="Agregar" class="glyphicon glyphicon-plus-sign"></i></a></div>';
    };

    var deleteFunctionTemplate = function (callback) {
        return '<div class="ngCellText colt{{$index}}"><a ng-click="$parent.' + callback + '(row.entity)"><i title="Eliminar" class="glyphicon glyphicon-remove-circle"></i></a></div>';
    };


    return {
        tooltipTemplate: tooltipTemplate,
        decimalEditableCellTemplate: decimalEditableCellTemplate,
        boolEditableCellTemplate: boolEditableCellTemplate,
        mailLinkTemplate: mailLinkTemplate,
        viewLinkTemplate: viewLinkTemplate,
        editLinkTemplate: editLinkTemplate,
        deleteLinkTemplate: deleteLinkTemplate,
        addFunctionTemplate: addFunctionTemplate,
        deleteFunctionTemplate: deleteFunctionTemplate
    };
}
);