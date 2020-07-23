$.fn.serializeObject = function () {
    var o = {};
    var a = this.serializeArray();
    $.each(a, function () {
        if (o[this.name]) {
            if (!o[this.name].push) {
                o[this.name] = [o[this.name]];
            }
            o[this.name].push(this.value || '');
        } else {
            o[this.name] = this.value || '';
        }
    });
    return o;
};

var Roca;
if (!Roca) {
    Roca = {};
};

(function() {

    if (typeof Roca.setPageTitle !== 'function') {
        Roca.setPageTitle = function(title) {
            $("#title").html(title);
        };
    };

    if (typeof Roca.loadContent !== 'function') {
        Roca.loadContent = function(url) {
            $("#content").load(url);
            

        };
    };

    if (typeof Roca.loadContent !== 'function') {
        Roca.loadContent = function(url) {
            $("#content").load(url);
            

        };
    };

    if (typeof Roca.setContent !== 'function') {
        Roca.setContent = function(html) {
            $("#content").html(html);
        };
    };

    if (typeof Roca.handleAjaxErrorResult !== 'function') {
        Roca.handleAjaxErrorResult = function(result) {
            $("#content").html(result.responseText);
        };
    };

    if (typeof Roca.isErrorRedirection !== 'function') {
        Roca.isErrorRedirection = function(headers) {
            if (headers('Error-redirection')) {
                return true;
            }
            return false;
        };
    };

    if (typeof Roca.fillWindowHeight !== 'function') {
        Roca.fillWindowHeight = function () {
            function allHeight() {
                var bodyh = $(document.body)[0].offsetHeight;
                var headerh = $("#header")[0].offsetHeight;
                $("#body").height(bodyh - headerh - 1);
            }
            $(window).resize(allHeight);
            allHeight();
        };
    };

    if (typeof Roca.setHeight !== 'function') {
        Roca.setHeight = function(id, height) {
            var h = $('#content')[0].offsetHeight;
            var newH = getPixels2(h, height);
            $('#' + id).height(newH);
        };
    };

    if (typeof Roca.fillParentHeight !== 'function') {
        Roca.fillParentHeight = function(id, height) {
            if (id.substring(0, 1) != '.') {
                id = '#' + id;
            }
            var elem = $(id);
            var elems = [];
            elem.parents().each(function() {
                if ($(this).attr('id') == 'content') return;
                if ($(this).data('containerHeight') !== undefined) {
                    elems.unshift($(this));
                }
            });;

            var bodyh = $(document.body)[0].offsetHeight;
            var headerh = $("#header")[0].offsetHeight;
            var computedHeight = bodyh - headerh - 1;
            for (var i = 0; i < elems.length; i++) {
                computedHeight = getChildHeight(elems[i], elems[i].data('containerHeight'), computedHeight);
            }

//            Roca.fillWindowHeight();
//            console.log('parentOffsetHeight: ' + computedHeight);
//            console.log('parentHeight: ' + $('#content').height());
//            console.log('bodyHeight: ' + $('#body').height());
//            var bodyh = $(document.body)[0].offsetHeight;
//            var headerh = $("#header")[0].offsetHeight;
//            console.log('bodyHeightTeorico: ' + (bodyh - headerh - 1));
            computedHeight =  getChildHeight(elem, height, computedHeight);
//            console.log('gridHeight: ' + computedHeight);
            elem.height(Math.round(computedHeight));

            function getChildHeight(child, childFactor, parentHeight) {
                var sh = 0;
                child.siblings().each(function() {
                    sh += $(this).height();
                });
                var rest = parentHeight - sh;
                return getPixels(rest, childFactor);
            }
        };
    };

    if (typeof Roca.setWidth !== 'function') {
        Roca.setWidth = function(id, width) {           
            if (id.substring(0, 1) != '.') {
                id = '#' + id;
            }
            var elem = $(id);
            var parents = elem.parents();
            var widths = [width];
            parents.each(function() {
                if ($(this).attr('id') == 'content') return;
                if ($(this).data('containerWidth') !== undefined) {
                    widths.unshift($(this).data('containerWidth'));
                }
            });
            var computedWidht = $('#content')[0].offsetWidth;
            for (var i = 0; i < widths.length; i++) {
                computedWidht = getPixels(computedWidht, widths[i]);
            }
            elem.width(Math.round(computedWidht));
        };
    };

    function getPixels(px, factor) {
        var i = 0;
        i = factor.indexOf('%');
        if (i > 0) {
            var percentage =  parseInt(factor);
            return px * percentage / 100;
        }
        i = factor.indexOf('px');
        if (i > 0) {
            var size =  parseInt(factor);
            if (size > 0) {
                 return Math.min(size, px);
            }
            return px + size;
        }
        throw 'El factor: ' + factor + ' no es valido';
    }

    function getPixels2(px, factor) {
        var i = 0;
        if (typeof (factor) == 'string') {
            i = factor.indexOf('%');
        }
        if (i > 0) {
            var percentage =  parseInt(factor);
            return px * percentage / 100;
        } else {
            return px + parseInt(factor);
        }
    }

    if (typeof Roca.confirmationDialog !== 'function') {
        Roca.confirmationDialog = function(url, args, text, title) {
            
            if ($('#confirmationDialog').length != 0) {
                $('#confirmationDialog').remove();
            }

            var divText = '<div id="confirmationDialog" style="display: none" \n' + 
                                   '    <span>' + text + '</span>\n' +
                          '</div>\n';
            var $divDialog = $(divText);
            $('#content').append($divDialog);

            $("#confirmationDialog").dialog({
                resizable: false,
                modal: true,
                title: title,
                buttons: {
                    OK: function() {
                        $.post(url, args, function(result) {
                            $("#content").html(result);
                        });
                        $(this).dialog("destroy");
                    },
                    Cancel: function() {                       
                        $(this).dialog("destroy");
                    }
                }
            });

        };
    };
})();

