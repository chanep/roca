angular.module('app')
.controller('Material.List.Controller', ['$scope', 'MaterialService', 'GridService', function($scope, materialService, gridService) {
        $scope.RootModel.Title = "Materiales";
        var pageSize = 25;
        $scope.Model = {};
        var m = $scope.Model;
        m.Materials = {};
        m.Filter = { Description: '', Skip: 0, Take: pageSize };

        (function initGrid() {
            var columnDefs = [
                { displayName: "IdentCode", field: "IdentCode", cellClass: "ngGridCell", headerClass: "ngGridHeader" },
                { displayName: "Descripcion", field: "Description", cellClass: "ngGridCell", headerClass: "ngGridHeader", cellTemplate: gridService.tooltipTemplate() }
            ];

            m.totalServerItems = 0;
            m.pagingOptions = {
                pageSizes: [pageSize, pageSize * 2, pageSize * 4],
                pageSize: pageSize,
                currentPage: 1
            };

            m.gridOptions = {
                data: "Model.Materials",
                columnDefs: columnDefs,
                enablePaging: true,
                showFooter: (m.totalServerItems > m.pagingOptions.pageSize),
                enableRowSelection: false,
                totalServerItems: 'Model.totalServerItems',
                pagingOptions: m.pagingOptions,
                filterOptions: m.filterOptions
            };

            $scope.$watch('Model.pagingOptions', function(newVal, oldVal) {
                getMaterialsPage();
            }, true);

        })();

        $scope.filterChange = function() {
            getMaterials();
        }

        getMaterials();

        function getMaterials() {
            m.pagingOptions.currentPage = 1;
            getMaterialsPage();
        }

        function getMaterialsPage() {
            m.Filter.Skip = (m.pagingOptions.currentPage - 1) * m.pagingOptions.pageSize;
            m.Filter.Take = m.pagingOptions.pageSize;
            materialService.getAll(m.Filter)
                .then(function(data) {
                    m.Materials = data.Materials;
                    m.totalServerItems = data.Count;
                });
        }

    }
])
.controller('Material.ListForMl.Controller', ['$scope', '$stateParams', 'MaterialService', 'MaterialListService', 'DialogService', 'GridService', function ($scope, $stateParams, materialService, materialListService, dialogService, gridService) {
        var pageSize = 20;
        $scope.Model = {};
        var m = $scope.Model;
        m.Materials = {};
        m.Filter = { MlId: $stateParams.id, Description: '', Skip: 0, Take: pageSize };

        (function initGrid() {
            var columnDefs = [
                { displayName: "IdentCode", field: "IdentCode", cellClass: "ngGridCell", headerClass: "ngGridHeader" },
                { displayName: "Descripcion", field: "Description", cellClass: "ngGridCell", headerClass: "ngGridHeader", cellTemplate: gridService.tooltipTemplate() },
                { displayName: "", cellClass: "ngGridCell", headerClass: "ngGridHeader", width: "30px", cellTemplate: gridService.addFunctionTemplate("addItem") }
            ];

            m.totalServerItems = 0;
            m.pagingOptions = {
                pageSizes: [pageSize, pageSize * 2, pageSize * 4],
                pageSize: pageSize,
                currentPage: 1
            };

            m.gridOptions = {
                data: "Model.Materials",
                columnDefs: columnDefs,
                enablePaging: true,
                showFooter: (m.totalServerItems > m.pagingOptions.pageSize),
                enableRowSelection: false,
                totalServerItems: 'Model.totalServerItems',
                pagingOptions: m.pagingOptions,
                filterOptions: m.filterOptions
            };

            $scope.$watch('Model.pagingOptions', function(newVal, oldVal) {
                getMaterialsPage();
            }, true);

        })();

        $scope.filterChange = function() {
            getMaterials();
        }

        $scope.addItem = function (material) {
            var fields = [{ Label: "Cantidad", Field: "Quantity", Value: 1 }];
            dialogService.confirmationDialog("Agregar Item", "Material: " + material.Description, fields)
                .then(function(result) {
                    var quantity = result[0].Value;
                    return materialListService.addItem(m.Filter.MlId, material.Id, quantity);
                })
                .then(function(data) {
                    $scope.$emit('material.itemAdded');
                    getMaterials();
                });
        }

        getMaterials();

        function getMaterials() {
            m.pagingOptions.currentPage = 1;
            getMaterialsPage();
        }


        function getMaterialsPage() {
            m.Filter.Skip = (m.pagingOptions.currentPage - 1) * m.pagingOptions.pageSize;
            m.Filter.Take = m.pagingOptions.pageSize;
            materialService.getAllForMl(m.Filter)
                .then(function(data) {
                    m.Materials = data.Materials;
                    m.totalServerItems = data.Count;
                });
        }

    }
]);