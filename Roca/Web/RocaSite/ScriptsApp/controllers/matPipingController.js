angular.module('app')
    .controller('MatPiping.Import.Controller',
    ['$scope', 'AutoplantService', 'MatPipingService', function ($scope, autoplantService, matPipingService) {
            $scope.RootModel.Title = "Piping - Importar materiales desde Autoplant";
            $scope.Model = {};
            var m = $scope.Model;

            m.Importing = false;
            m.ImportEnable = true;
            m.Imported = false;

            autoplantService.getProjects()
                .then(function(data) {
                    m.Projects = data;
                    m.ProjectId = autoplantService.currentProjectId;
                });

            $scope.projectChanged = function() {
                autoplantService.currentProjectId = m.ProjectId;
                m.Imported = false;
            }

            $scope.import = function () {
                m.Importing = true;
                m.ImportEnable = false;
                m.Imported = false;
                matPipingService.importFromAp(m.ProjectId)
                    .then(function(data) {
                        m.MaterialCount = data;
                        m.Importing = false;
                        m.ImportEnable = true;
                        m.Imported = true;
                });
            }

        }
    ])

    .controller('MatPiping.LmReport.Controller',
    ['$scope', 'Utils', 'AutoplantService', 'MatPipingService', function ($scope, utils, autoplantService, matPipingService) {
        $scope.RootModel.Title = "Piping - Reporte para Lista de Materiales";
        $scope.Model = {};
        var m = $scope.Model;

        m.Filter = {};

        autoplantService.getProjects()
                .then(function (data) {
                    m.Projects = data;
                    m.Filter.ProjectId = autoplantService.currentProjectId;
                });

        $scope.projectChanged = function () {
            autoplantService.currentProjectId = m.Filter.ProjectId;
            m.Filter.CommodityCode = "";
            m.Filter.LongDescription = "";
        }

        $scope.filterChanged = function (fieldName) {
            for (var f in m.Filter) {
                if (f != fieldName && f != 'ProjectId') {
                    m.Filter[f] = "";
                }
            }
            if (m.Filter.CommodityCode == '' && m.Filter.LongDescription == '') {
                m.MatPipingFamily = null;
            }
        }

        $scope.getAutosuggest = function(field) {
            return matPipingService.getAutosuggest(field, m.Filter[field], m.Filter)
                .then(function(data) {
                    return utils.orderBy(data, field);
                });
        };

        $scope.updateLm = function () {
            matPipingService.getAll(m.Filter)
                .then(function(data) {
                    m.MatPipingFamily = data;
            });
        };

        }
    ])
    ;