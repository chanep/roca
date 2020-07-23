angular.module('myModule')
    .directive('ieSelectFix', [
        function () {
           //arregla el problema en IE9 en donde no se elimina correctamente el placeholder (--seleccione un ... --) ni se repinta correctamente el select 
           //al elegir una opcion si el select esta dentro de un ng-repeat
            return {
                restrict: 'A',
                link: function (scope, element, attributes) {
                    //var isIE = document.attachEvent;
                    //if (!isIE) return;

                    if (navigator.userAgent.indexOf('MSIE 9') == -1) return;

                    var control = element[0];
                    //to fix IE8 issue with parent and detail controller, we need to depend on the parent controller
                    scope.$watch(attributes.ngModel, function () {
                        //this will add and remove the options to trigger the rendering in IE8
                        var option = document.createElement("option");
                        control.add(option, null);
                        control.remove(control.options.length - 1);
                    });
                }
            }
        }
    ]);