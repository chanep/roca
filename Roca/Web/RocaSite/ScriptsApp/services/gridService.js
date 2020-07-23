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
        return '<div class="ngCellText colt{{$index}}"><a ng-click="$parent.' + callback + '(row.entity)"><i title="Agregar" style="cursor: pointer;"class="glyphicon glyphicon-plus-sign"></i></a></div>';
    };

    var deleteFunctionTemplate = function (callback) {
        return '<div class="ngCellText colt{{$index}}"><a ng-click="$parent.' + callback + '(row.entity)"><i title="Eliminar" style="color: #ff0000; cursor: pointer;" class="glyphicon glyphicon-remove-circle"></i></a></div>';
    };

    var footerTemplate = function(visible) {
        return  "<div class=\"ngFooterPanel\" ng-show=\"$parent." + visible + "\" style='border-top: 0px;' ng-class=\"{'ui-widget-content': jqueryUITheme, 'ui-corner-bottom': jqueryUITheme}\" ng-style=\"footerStyle()\" >\r" +
                "\n" +
                "    <div class=\"ngTotalSelectContainer\" >\r" +
                "\n" +
                "        <div class=\"ngFooterTotalItems\" ng-class=\"{'ngNoMultiSelect': !multiSelect}\" >\r" +
                "\n" +
                "            <span class=\"ngLabel\">{{i18n.ngTotalItemsLabel}} {{maxRows()}}</span><span ng-show=\"filterText.length > 0\" class=\"ngLabel\">({{i18n.ngShowingItemsLabel}} {{totalFilteredItemsLength()}})</span>\r" +
                "\n" +
                "        </div>\r" +
                "\n" +
                "        <div class=\"ngFooterSelectedItems\" ng-show=\"multiSelect\">\r" +
                "\n" +
                "            <span class=\"ngLabel\">{{i18n.ngSelectedItemsLabel}} {{selectedItems.length}}</span>\r" +
                "\n" +
                "        </div>\r" +
                "\n" +
                "    </div>\r" +
                "\n" +
                "    <div class=\"ngPagerContainer\" style=\"float: right; margin-top: 10px;\" ng-show=\"enablePaging\" ng-class=\"{'ngNoMultiSelect': !multiSelect}\">\r" +
                "\n" +
                "        <div style=\"float:left; margin-right: 10px;\" class=\"ngRowCountPicker\">\r" +
                "\n" +
                "            <span style=\"float: left; margin-top: 3px;\" class=\"ngLabel\">{{i18n.ngPageSizeLabel}}</span>\r" +
                "\n" +
                "            <select style=\"float: left;height: 27px; width: 100px\" ng-model=\"pagingOptions.pageSize\" >\r" +
                "\n" +
                "                <option ng-repeat=\"size in pagingOptions.pageSizes\">{{size}}</option>\r" +
                "\n" +
                "            </select>\r" +
                "\n" +
                "        </div>\r" +
                "\n" +
                "        <div style=\"float:left; margin-right: 10px; line-height:25px;\" class=\"ngPagerControl\" style=\"float: left; min-width: 135px;\">\r" +
                "\n" +
                "            <button type=\"button\" class=\"ngPagerButton\" ng-click=\"pageToFirst()\" ng-disabled=\"cantPageBackward()\" title=\"{{i18n.ngPagerFirstTitle}}\"><div class=\"ngPagerFirstTriangle\"><div class=\"ngPagerFirstBar\"></div></div></button>\r" +
                "\n" +
                "            <button type=\"button\" class=\"ngPagerButton\" ng-click=\"pageBackward()\" ng-disabled=\"cantPageBackward()\" title=\"{{i18n.ngPagerPrevTitle}}\"><div class=\"ngPagerFirstTriangle ngPagerPrevTriangle\"></div></button>\r" +
                "\n" +
                "            <input class=\"ngPagerCurrent\" min=\"1\" max=\"{{currentMaxPages}}\" type=\"number\" style=\"width:50px; height: 24px; margin-top: 1px; padding: 0 4px;\" ng-model=\"pagingOptions.currentPage\"/>\r" +
                "\n" +
                "            <span class=\"ngGridMaxPagesNumber\" ng-show=\"maxPages() > 0\">/ {{maxPages()}}</span>\r" +
                "\n" +
                "            <button type=\"button\" class=\"ngPagerButton\" ng-click=\"pageForward()\" ng-disabled=\"cantPageForward()\" title=\"{{i18n.ngPagerNextTitle}}\"><div class=\"ngPagerLastTriangle ngPagerNextTriangle\"></div></button>\r" +
                "\n" +
                "            <button type=\"button\" class=\"ngPagerButton\" ng-click=\"pageToLast()\" ng-disabled=\"cantPageToLast()\" title=\"{{i18n.ngPagerLastTitle}}\"><div class=\"ngPagerLastTriangle\"><div class=\"ngPagerLastBar\"></div></div></button>\r" +
                "\n" +
                "        </div>\r" +
                "\n" +
                "    </div>\r" +
                "\n" +
                "</div>\r" +
                "\n";
        };

        return {
        tooltipTemplate: tooltipTemplate,
        footerTemplate: footerTemplate,
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