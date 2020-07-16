angular.module('app')
    .directive('myTooltip', function() {
        return {
            restrict: 'A',
            link: function (scope, elem, attrs) {
                    elem.mouseover(function (e) {
                        if (this.offsetWidth < this.scrollWidth) {
                            var title = $(this).attr("title");
                            if (title && title != "")
                                return;
                            var content = $(this).html();
                            $(this).attr("title", content);
                        }
                    });
                }
        }
    })