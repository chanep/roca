angular.module('app').controller('Autoplant.List.Controller', ['$scope', 'AutoplantService', function ($scope, autoplantService) {
    $scope.RootModel.Title = "Materiales Piping";

    $scope.Model = {};
    var m = $scope.Model;
    var maxRows = 100;
    m.Loading = false;
    m.Materials = [];
    m.Filter = {};
    m.Filter.IncludeService = "1";
    m.Filter.IncludeLine = "2";
    m.Filter.IncludeTag = "4";
    m.Filter.Order = "linea";
    m.Filter.Take = maxRows;
    m.PageInfo = { TotalItems: 0, CurrentPage: 1, ItemsPerPage: maxRows, FirstItem: 0  };

    clearFilters();
    updateOptionalFields();


    autoplantService.getProjects()
        .then(function(data) {
            m.Projects = data;
            if (m.Projects && m.Projects.length > 0) {
                m.Filter.ProjectId = m.Projects[0].Id;
                updateAreas();
            }

        });

    $scope.projectChanged = function() {
        updateAreas();
    }
    $scope.areaChanged = function () {
        getMaterials();
    }

    $scope.optionalFieldsChanged = function () {
        updateOptionalFields();
        getMaterials();
    }

    $scope.filtersChanged = function () {
        getMaterials();
    }

    $scope.getAutosuggest = function (field, term) {
        //m.Filter[field] = term;
        var filtersStr = angular.toJson(m.Filter);
        return autoplantService.getAutosuggest(field, m.Filter[field], filtersStr)
        .then(function (data) {
            var first = { value: term };
            data.data.unshift(first);
            return data.data;
        });
    }

    $scope.autocompleteInputChanged = function(field) {
        if (m.Filter[field] == '') {
            getMaterials();
        }
    }

    $scope.pageChanged = function (page) {
        m.Filter.Skip = (page - 1) * m.PageInfo.ItemsPerPage;
        getMaterialsPage();
    }

    $scope.clearFilters = clearFilters;

    function updateAreas() {
        autoplantService.getAreas(m.Filter.ProjectId)
            .then(function(data) {
                m.Areas = data;
                if (m.Areas && m.Areas.length > 0) {
                    m.Filter.AreaId = m.Areas[0].Id;
                    getMaterials();
                }
            });
    }

    function getMaterials() {        
        m.PageInfo.CurrentPage = 1;
        m.Filter.Skip = 0;
        getMaterialsPage();
    }

    function getMaterialsPage() {
        m.Loading = true;
        autoplantService.getMaterials(m.Filter)
            .then(function(data) {
                m.Materials = data.Materials;
                m.PageInfo.TotalItems = data.Count;
                m.PageInfo.FirstItem = (m.PageInfo.CurrentPage - 1) * m.PageInfo.ItemsPerPage;
                m.Loading = false;
            });
    }
   
    function clearFilters() {
        var f = m.Filter;

        var filtersEmpty = f.Service == "" && f.Line == "" && f.Tag == "" && f.ShortDescription == "" &&
                           f.LongDescription == "" && f.NominalDiam == "" && f.Rating == "" && f.Schedule == "" && f.PieceMark == "";
        f.Service = "";
        f.Line = "";
        f.Tag = "";
        f.ShortDescription = "";
        f.LongDescription = "";
        f.NominalDiam = "";
        f.Rating = "";
        f.Schedule = "";
        f.PieceMark = "";

        if (!filtersEmpty) {
            getMaterials();
        }        
    }
    function updateOptionalFields() {
        var f = m.Filter;
        f.OptionalFields = parseInt(f.IncludeService) + parseInt(f.IncludeLine) + parseInt(f.IncludeTag);
    }




}]);