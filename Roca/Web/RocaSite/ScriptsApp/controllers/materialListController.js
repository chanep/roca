angular.module('app')
.controller('MaterialList.List.Controller', ['$scope', 'MaterialListService', 'GridService', function($scope, materialListService, gridService) {
        $scope.RootModel.Title = "Lista de Materiales";
        $scope.Model = {};
        var m = $scope.Model;

        (function initGrid() {
            var columnDefs = [
                { displayName: "Documento", field: "DocNumber", cellClass: "ngGridCell", headerClass: "ngGridHeader" },
                { displayName: "Titulo", field: "Title", cellClass: "ngGridCell", headerClass: "ngGridHeader", cellTemplate: gridService.tooltipTemplate() },
                { displayName: "Revision", field: "Revision", maxWidth: 100, cellClass: "ngGridCell", headerClass: "ngGridHeader" },
                { displayName: "Autor", field: "CreatorShowName", cellClass: "ngGridCell", headerClass: "ngGridHeader" },
                { displayName: "Fecha Creacion", field: "CreatedOn", cellFilter: "jsonDate:'dd/MM/yyyy'", cellClass: "ngGridCell", headerClass: "ngGridHeader" },
                { displayName: "Especialidad", field: "SpecialtyName", cellClass: "ngGridCell", headerClass: "ngGridHeader" },
                { displayName: "Estado", field: "StatusDescription", cellClass: "ngGridCell", headerClass: "ngGridHeader" },
                { displayName: "", field: "Id", cellClass: "ngGridCell", headerClass: "ngGridHeader", width: "30px", cellTemplate: gridService.viewLinkTemplate("#/MaterialList/View/") },
                { displayName: "", field: "Id", cellClass: "ngGridCell", headerClass: "ngGridHeader", width: "30px", cellTemplate: gridService.editLinkTemplate("#/MaterialList/Edit/") }
            ];

            m.gridOptions = {
                data: "Model.data",
                columnDefs: columnDefs
            };

        })();

        materialListService.getAllHeadRevision()
            .then(function(data) {
                m.data = data;
            });


    }
])
.controller('MaterialList.RevisionHistory.Controller', [
    '$scope', '$stateParams', 'MaterialListService', 'GridService', function($scope, $stateParams, materialListService, gridService) {
        $scope.Model = {};
        var m = $scope.Model;
        m.Id = $stateParams.id;

        (function initGrid() {
            var columnDefs = [
                { displayName: "Documento", field: "DocNumber", cellClass: "ngGridCell", headerClass: "ngGridHeader" },
                { displayName: "Titulo", field: "Title", cellClass: "ngGridCell", headerClass: "ngGridHeader", cellTemplate: gridService.tooltipTemplate() },
                { displayName: "Revision", field: "Revision", maxWidth: 100, cellClass: "ngGridCell", headerClass: "ngGridHeader" },
                { displayName: "Autor", field: "CreatorShowName", cellClass: "ngGridCell", headerClass: "ngGridHeader" },
                { displayName: "Fecha Creacion", field: "CreatedOn", cellFilter: "jsonDate:'dd/MM/yyyy'", cellClass: "ngGridCell", headerClass: "ngGridHeader" },
                { displayName: "Especialidad", field: "SpecialtyName", cellClass: "ngGridCell", headerClass: "ngGridHeader" },
                { displayName: "Estado", field: "StatusDescription", cellClass: "ngGridCell", headerClass: "ngGridHeader" },
                { displayName: "", field: "Id", cellClass: "ngGridCell", headerClass: "ngGridHeader", width: "30px", cellTemplate: gridService.viewLinkTemplate("#/MaterialList/View/") },
                { displayName: "", field: "Id", cellClass: "ngGridCell", headerClass: "ngGridHeader", width: "30px", cellTemplate: gridService.editLinkTemplate("#/MaterialList/Edit/") }
            ];

            m.gridOptions = {
                data: "Model.data",
                columnDefs: columnDefs
            };

        })();

        materialListService.getRevisionHistory(m.Id)
            .then(function(data) {
                m.data = data;
            });
    }
])
.controller('MaterialList.New.Controller', [
    '$scope', '$location', 'MaterialListService', function($scope, $location, materialListService) {
        $scope.Model = {};
        var m = $scope.Model;
        m.New = true;

        materialListService.getNewVm()
            .then(function(data) {
                m.Ml = data.MaterialList;
                m.Specialties = data.Specialties;
                m.Users = data.Users;
                m.Purposes = data.Purposes;
            });

        $scope.submit = function() {
            materialListService.add(m.Ml)
                .then(function(data) {
                    $location.path('MaterialList/Edit/' + data.Id);
                });
        };
    }
])
.controller('MaterialList.Edit.Controller', [
    '$scope', '$stateParams', 'MaterialListService', function($scope, $stateParams, materialListService) {
        $scope.Model = {};
        var m = $scope.Model;
        m.id = $stateParams.id;
        m.New = false;
        m.Saved = false;

        getViewModel();

        $scope.submit = function() {
            var y = $scope.mlForm;
            materialListService.save(m.Ml)
                .then(function() {
                    m.Saved = true;
                    getViewModel();
                });
        };

        function getViewModel() {
            materialListService.getVm(m.id)
                .then(function(data) {
                    m.Ml = data.MaterialList;
                    m.Specialties = data.Specialties;
                    m.Users = data.Users;
                    m.Purposes = data.Purposes;
                });
        }
    }
])
.controller('MaterialList.View.Controller', [
    '$scope', '$stateParams', 'MaterialListService', function($scope, $stateParams, materialListService) {
        $scope.Model = {};
        var m = $scope.Model;
        m.id = $stateParams.id;
        materialListService.getFull(m.id)
            .then(function(data) {
                m.Ml = data;
            });
    }
])
.controller('MaterialList.Header.Controller', [
    '$scope', '$stateParams', 'MaterialListService', function($scope, $stateParams, materialListService) {
        $scope.Model = {};
        var m = $scope.Model;
        m.Id = $stateParams.id;
        materialListService.get(m.Id)
            .then(function(data) {
                m.Ml = data;
            });
    }
])
.controller('MaterialList.ItemList.Controller', [
    '$scope', '$stateParams', 'MaterialListService', 'DialogService', 'GridService', function($scope, $stateParams, materialListService, dialogService, gridService) {
        var pageSize = 25;
        $scope.Model = {};
        var m = $scope.Model;
        m.Id = $stateParams.id;
        m.EditMode = ($stateParams.mode == 'Edit');

        (function initGrid() {
            var columnDefs = [
                { displayName: "Id", field: "Id", visible: false, cellClass: "ngGridCell", headerClass: "ngGridHeader" },
                { displayName: "IdentCode", field: "MaterialIdentCode", cellClass: "ngGridCell", headerClass: "ngGridHeader" },
                { displayName: "Descripcion", field: "MaterialDescription", cellClass: "ngGridCell", headerClass: "ngGridHeader", cellTemplate: gridService.tooltipTemplate() },
                { displayName: "Cant. Previa", field: "PrevQuantity", cellClass: "ngGridCell", headerClass: "ngGridHeader" },
                { displayName: "Cantidad", field: "Quantity", enableCellEdit: m.EditMode, cellClass: "ngGridCell", headerClass: "ngGridHeader", editableCellTemplate: gridService.decimalEditableCellTemplate() },
                { displayName: "Diferencia", field: "Difference", cellClass: "ngGridCell", headerClass: "ngGridHeader" },
                { displayName: "Unidad", field: "MaterialUnitAbbreviation", cellClass: "ngGridCell", headerClass: "ngGridHeader" },
                { displayName: "", sortable: false, visible: m.EditMode, cellClass: "ngGridCell", headerClass: "ngGridHeader", width: "30px", cellTemplate: gridService.deleteFunctionTemplate("deleteItem") }
            ];


            m.totalServerItems = 0;
            m.pagingOptions = {
                pageSizes: [pageSize, pageSize * 2, pageSize * 4],
                pageSize: pageSize,
                currentPage: 1
            };

            m.gridOptions = {
                data: "Model.Items",
                columnDefs: columnDefs,
                enablePaging: true,
                showFooter: (m.totalServerItems > m.pagingOptions.pageSize),
                enableRowSelection: false,
                enableCellSelection: m.EditMode,
                totalServerItems: 'Model.totalServerItems',
                pagingOptions: m.pagingOptions,
                sortInfo: { fields: ['Id'], directions: ['desc'] }
            };

            $scope.$on('ngGridEventEndCellEdit', function (event) {
                var item = event.targetScope.row.entity;
                materialListService.updateItem(item)
                    .then(function() {
                        getAllItems();
                    });
            });

            $scope.deleteItem = function(item) {
                dialogService.confirmationDialog("Eliminar Item", "Desea eliminar " + item.MaterialDescription + "?")
                    .then(function() {
                        return materialListService.removeItem(item.Id);
                    })
                    .then(function() {
                        getAllItems();
                    });
            }
        })();

        $scope.$on('material.itemAdded', function(event) {
            getAllItems();
        });

        getAllItems();

        function getAllItems() {
            m.pagingOptions.currentPage = 1;
            materialListService.getAllItems(m.Id)
                    .then(function (data) {
                        m.Items = data;
                        m.totalServerItems = m.Items.length;
                    });
        }


    }
]);
