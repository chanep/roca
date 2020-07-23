angular.module('app')
.directive('ngChangeDelay', ['$timeout', function ($timeout) {
    return {
        restrict: 'A',
        //true -> child scope, false o undefined -> shared scope
        // scope: {name: '@' (idem '@name')} -> one way binding de literals o expressions ( name='{{Model.Name}}' ), toma el valor del atributo name en forma readOnly
        // scope: {name: '='} -> two way binding de un objeto del scope externo ( name='Model.Name' ) 
        // scope: {onChange: '&'} -> para callbacks sobre el scope externo ( on-change='changed()' )
        scope: true,  
        compile: function (element, attributes) {
            var expression = attributes['ngChange'];
            if (!expression)
                return;

            var ngModel = attributes['ngModel'];
            if (ngModel) attributes['ngModel'] = '$parent.' + ngModel;
            attributes['ngChange'] = '$$delay.execute()';

            return {
                post: function (scope, iElement, iAttributes) {
                    scope.$$delay = {
                        expression: expression,
                        delay: scope.$eval(iAttributes['ngChangeDelay']),
                        execute: function () {
                            var state = scope.$$delay;
                            state.then = Date.now();
                            $timeout(function () {
                                if (Date.now() - state.then >= state.delay)
                                    scope.$parent.$eval(expression);
                            }, state.delay);
                        }
                    };
                }
            }
        }
    };
} ]);