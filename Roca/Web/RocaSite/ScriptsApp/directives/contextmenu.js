(function (angular) {
    var ngContextMenu = angular.module('myModule', []);

    ngContextMenu.directive('context', [
        function() {
            return {
                restrict: 'A',
                scope: '@&',
                compile: function compile(tElement, tAttrs, transclude) {
                    return {
                        post: function postLink(scope, iElement, iAttrs, controller) {
                            var ul = $('#' + iAttrs.context);
                            var last = null;

                            ul.css({ 'display': 'none' });

                            $(iElement).mousedown(function(event) {
                                if (event.button == 2) {
                                    ul.css({
                                        position: "fixed",
                                        display: "block",
                                        left: event.clientX + 'px',
                                        top: event.clientY + 'px'
                                    });
                                    last = event.timeStamp;
                                }
                            });

                            $(iElement).on("contextmenu", function() {
                                return false;
                            });


                            $(document).click(function(event) {
                                var target = $(event.target);
                                if (!target.is(".popover") && !target.parents().is(".popover")) {
                                    if (last === event.timeStamp)
                                        return;
                                    ul.css({
                                        'display': 'none'
                                    });
                                }
                            });
                        }
                    };
                }
            };
        }
    ]);
})(window.angular);