angular.module('myModule')
    .directive('myReadonlyRepeat', function () {
        return {
            restrict: 'A',
            transclude: 'element',
            link: function($scope, $element, $attr, ctrl, $transclude) {
                var expression = $attr.myReadonlyRepeat;
                var match = expression.match(/(\w+)\s+in\s+(\S+)/);
                if (!match) {
                    throw new Error("Expected expression in form of 'item in array' but got " + expression);
                }

                var itemStr = match[1];
                var collectionStr = match[2];

                var prevBlocks = [];


                $scope.$watchCollection(collectionStr, function (collection) {
                    var msg = "";
                    msg += "start delete: " + new Date() + "\n\r";
                    for (var i in prevBlocks) {
                        var block = prevBlocks[i];
                        block.scope.$destroy();
                        block.elem.remove();
                    }
                    prevBlocks = [];
                    var prevElem = $element;
                    msg += "end delete: " + new Date() + "\n\r";
                    for (var i = 0; i < collection.length; i++) {
                        $transclude(function(clone, scope) {
                            var block = { elem: clone, scope: scope };
                            prevBlocks.push(block);
                            scope[itemStr] = collection[i];
                            prevElem.after(clone[0]);
                            prevElem = clone;
                        });
                    }
                    msg += "end add: " + new Date() + "\n\r";
                    console.log(msg);
                });

            }
        }
    })

    .directive('myReadonlyRepeat2', ['$interpolate', function ($interpolate) {
        return {
            restrict: 'A',
            transclude: 'element',
            link: function ($scope, $element, $attr, ctrl, $transclude) {
                var expression = $attr.myReadonlyRepeat2;
                var match = expression.match(/(\w+)\s+in\s+(\S+)/);
                if (!match) {
                    throw new Error("Expected expression in form of 'item in array' but got " + expression);
                }

                var itemStr = match[1];
                var collectionStr = match[2];

                var prevElems = [];
                var template;

                $transclude(function (clone) {
                    template = clone;
                    //scope.$destroy();
                });

                template.removeAttr('my-readonly-repeat2');
                var itplFn = $interpolate(template[0].outerHTML);

                $scope.$watchCollection(collectionStr, function (collection) {
                    var msg = "";
                    msg += "start delete: " + new Date() + "\n\r";

                    for (var i in prevElems) {
                        var elem = prevElems[i];
                        elem.remove();
                    }
                    prevElems = [];

                    var context = {};
                    var prevElem = $element;
                    
                    msg += "end delete: " + new Date() + "\n\r";

                    for (var i = 0; i < collection.length; i++) {
                        context[itemStr] = collection[i];
                        context.$index = i;
                        context.__proto__ = $scope;
                        var html = itplFn(context);
                        var elem = angular.element(html);
                        prevElems.push(elem);
                        prevElem.after(elem[0]);
                        prevElem = elem;
                    }

                    msg += "end add: " + new Date() + "\n\r";
                    //console.log(msg);
                });

            }
        }
    } ])
    
    
    ;