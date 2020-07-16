(function ($) {
    // Register namespace
    $.extend(true, window, {
        "Slick": {
            "AutoTooltipsAjax": AutoTooltipsAjax
        }
    });

    /**
    * AutoTooltips plugin to show/hide tooltips when columns are too narrow to fit content.
    * @constructor
    * @param {boolean} [options.enableForCells=true]        - Enable tooltip for grid cells
    * @param {boolean} [options.enableForHeaderCells=false] - Enable tooltip for header cells
    * @param {number}  [options.maxToolTipLength=null]      - The maximum length for a tooltip
    * @param {string}  [options.columnId=null]              - The ColumnId for column who load tooltip from ajax
    * @param {function(cell)}  [options.onCellMouseOverForAjax=null]       - function that should return tooltip text of the item cell {cell, row}
    */
    function AutoTooltipsAjax(options) {
        var _grid;
        var _self = this;
        var _defaults = {
            enableForCells: true,
            enableForHeaderCells: false,
            maxToolTipLength: null,
            columnId : null,
            onCellMouseOverForAjax: null
        };

        /**
        * Initialize plugin.
        */
        function init(grid) {
            options = $.extend(true, {}, _defaults, options);
            _grid = grid;
            if (options.enableForCells) _grid.onMouseEnter.subscribe(handleMouseEnter);
            if (options.enableForHeaderCells) _grid.onHeaderMouseEnter.subscribe(handleHeaderMouseEnter);
        }

        /**
        * Destroy plugin.
        */
        function destroy() {
            if (options.enableForCells) _grid.onMouseEnter.unsubscribe(handleMouseEnter);
            if (options.enableForHeaderCells) _grid.onHeaderMouseEnter.unsubscribe(handleHeaderMouseEnter);
        }

        /**
        * Handle mouse entering grid cell to add/remove tooltip.
        * @param {jQuery.Event} e - The event
        */
        function handleMouseEnter(e) {
            var cell = _grid.getCellFromEvent(e);
            if (cell) {
                var $node = $(_grid.getCellNode(cell.row, cell.cell));
                var title = $node.attr("title");
                if (title && title != "")
                    return;
                var col = _grid.getColumns()[cell.cell];               
                if (col.id == options.columnId) {
                    options.onCellMouseOverForAjax(cell);
                } else {
                    
                    var text;
                    if ($node.innerWidth() < $node[0].scrollWidth) {
                        text = $.trim($node.text());
                        if (options.maxToolTipLength && text.length > options.maxToolTipLength) {
                            text = text.substr(0, options.maxToolTipLength - 3) + "...";
                        }
                    } else {
                        text = "";
                    }
                    $node.attr("title", text);
                }               
            }
        }

        /**
        * Handle mouse entering header cell to add/remove tooltip.
        * @param {jQuery.Event} e     - The event
        * @param {object} args.column - The column definition
        */
        function handleHeaderMouseEnter(e, args) {
            var column = args.column,
          $node = $(e.target).closest(".slick-header-column");
            if (!column.toolTip) {
                $node.attr("title", ($node.innerWidth() < $node[0].scrollWidth) ? column.name : "");
            }
        }

        function setToolTip(cell, text) {
            var $node = $(_grid.getCellNode(cell.row, cell.cell));
            if (options.maxToolTipLength && text.length > options.maxToolTipLength) {
                text = text.substr(0, options.maxToolTipLength - 3) + "...";
            }
            $node.attr("title", text);
        }

        // Public API
        $.extend(this, {
            "init": init,
            "destroy": destroy,
            "setToolTip": setToolTip
        });
    }
})(jQuery);