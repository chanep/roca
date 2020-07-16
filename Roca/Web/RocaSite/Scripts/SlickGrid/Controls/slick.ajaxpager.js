(function ($) {
    function SlickGridPagerAjax(grid, pagerUpdatedCallback, $container) {
        var $status;
        var pagingInfo = { totalRows: 0, pageSize: 50, pageNum: 0 };


        function init() {
            //            dataView.onPagingInfoChanged.subscribe(function (e, pagingInfo) {
            //                updatePager(pagingInfo);
            //            });

            constructPagerUI();
            updatePager(true);
        }

        function getNavState() {
            var cannotLeaveEditMode = !Slick.GlobalEditorLock.commitCurrentEdit();
            var lastPage = totalPages() - 1;

            return {
                canGotoFirst: !cannotLeaveEditMode && pagingInfo.pageSize != 0 && pagingInfo.pageNum > 0,
                canGotoLast: !cannotLeaveEditMode && pagingInfo.pageSize != 0 && pagingInfo.pageNum != lastPage,
                canGotoPrev: !cannotLeaveEditMode && pagingInfo.pageSize != 0 && pagingInfo.pageNum > 0,
                canGotoNext: !cannotLeaveEditMode && pagingInfo.pageSize != 0 && pagingInfo.pageNum < lastPage
            };
        }

        function totalPages() {
            return Math.ceil(pagingInfo.totalRows / pagingInfo.pageSize);
        }

        function setPageSize(n) {
            pagingInfo.pageSize = n;
            updatePager();
        }

        function gotoFirst() {
            if (getNavState().canGotoFirst) {
                pagingInfo.pageNum = 0;
                updatePager();
            }
        }

        function gotoLast() {
            var state = getNavState();
            if (state.canGotoLast) {
                pagingInfo.pageNum = totalPages() - 1;
                updatePager();
            }
        }

        function gotoPrev() {
            var state = getNavState();
            if (state.canGotoPrev) {
                pagingInfo.pageNum = pagingInfo.pageNum - 1;
                updatePager();
            }
        }

        function gotoNext() {
            var state = getNavState();
            if (state.canGotoNext) {
                pagingInfo.pageNum = pagingInfo.pageNum + 1;
                updatePager();
            }
        }

        function constructPagerUI() {
            $container.empty();

            var $nav = $("<span class='slick-pager-nav' />").appendTo($container);
            var $settings = $("<span class='slick-pager-settings' />").appendTo($container);
            $status = $("<span class='slick-pager-status' />").appendTo($container);

            $settings
          .append("<span class='slick-pager-settings-expanded' style='display:none'>Show: <a data=0>All</a><a data='-1'>Auto</a><a data=25>25</a><a data=50>50</a><a data=100>100</a></span>");

            $settings.find("a[data]").click(function (e) {
                var pagesize = $(e.target).attr("data");
                if (pagesize != undefined) {
                    if (pagesize == -1) {
                        var vp = grid.getViewport();
                        setPageSize(vp.bottom - vp.top);
                    } else if (pagesize == 0) {
                        setPageSize(pagingInfo.totalRows);
                    } else {
                        setPageSize(parseInt(pagesize));
                    }
                }
            });

            var icon_prefix = "<span class='ui-state-default ui-corner-all ui-icon-container'><span class='ui-icon ";
            var icon_suffix = "' /></span>";

            $(icon_prefix + "ui-icon-lightbulb" + icon_suffix)
          .click(function () {
              $(".slick-pager-settings-expanded").toggle();
          })
          .appendTo($settings);

            $(icon_prefix + "ui-icon-seek-first" + icon_suffix)
          .click(gotoFirst)
          .appendTo($nav);

            $(icon_prefix + "ui-icon-seek-prev" + icon_suffix)
          .click(gotoPrev)
          .appendTo($nav);

            $(icon_prefix + "ui-icon-seek-next" + icon_suffix)
          .click(gotoNext)
          .appendTo($nav);

            $(icon_prefix + "ui-icon-seek-end" + icon_suffix)
          .click(gotoLast)
          .appendTo($nav);

            $container.find(".ui-icon-container")
          .hover(function () {
              $(this).toggleClass("ui-state-hover");
          });

            $container.children().wrapAll("<div class='slick-pager' />");
        }


        function updatePager(preventCallBack) {
            var state = getNavState();

            $container.find(".slick-pager-nav span").removeClass("ui-state-disabled");
            if (!state.canGotoFirst) {
                $container.find(".ui-icon-seek-first").addClass("ui-state-disabled");
            }
            if (!state.canGotoLast) {
                $container.find(".ui-icon-seek-end").addClass("ui-state-disabled");
            }
            if (!state.canGotoNext) {
                $container.find(".ui-icon-seek-next").addClass("ui-state-disabled");
            }
            if (!state.canGotoPrev) {
                $container.find(".ui-icon-seek-prev").addClass("ui-state-disabled");
            }

            if (pagingInfo.pageSize >= pagingInfo.totalRows) {
                $status.text("Showing all " + pagingInfo.totalRows + " rows");
            } else {
                $status.text("Showing page " + (pagingInfo.pageNum + 1) + " of " + totalPages());
            }

            if (!preventCallBack)
                pagerUpdatedCallback(pagingInfo.pageNum + 1, pagingInfo.pageSize);
        }

        function updateTotalRows(n) {
            pagingInfo.totalRows = n;
            pagingInfo.pageNum = 0;
            updatePager(true);
        }

        init();

        // Public API
        $.extend(this, {
            "updateTotalRows": updateTotalRows,
        });
    }

    // Slick.Controls.Pager
    $.extend(true, window, { Slick: { Controls: { PagerAjax: SlickGridPagerAjax } } });
})(jQuery);