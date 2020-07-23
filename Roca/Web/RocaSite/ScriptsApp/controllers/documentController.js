angular.module('app')
.controller('Document.New.Controller', ['$scope', 'Utils', 'DocumentService', function ($scope, utils, documentService) {
        $scope.RootModel.Title = "Agregar Documento";
        $scope.Model = {};
        var m = $scope.Model;

        documentService.getOptions()
                .then(function (data) {
                    orderProjects(data.Projects);
                    m.Options = data;
                });

        $scope.projectChanged = function () {
            var p = utils.findById(m.Options.Projects, m.ProjectId);
            m.Subprojects = p.Subprojects;
            if (m.Subprojects.length == 1) {
                m.Doc.ProjectId = m.Subprojects[0].Id;
            }
        };

        $scope.subprojectVisible = function () {
            if (utils.isNullOrUndefined(m.Subprojects)) return true;
            return m.Subprojects.length > 1;
        };

        $scope.submit = function () {
            m.ShowError = false;
            m.Saved = false;
            $scope.docForm.$setPristine();
            documentService.add(m.Doc)
                .then(function(data) {
                    m.Saved = true;
                }, function(data) {
                    m.Error = data.Error;
                    m.ShowError = true;
                });
        };


        function orderProjects(projects) {
            for (var i in projects) {
                var p = projects[i];
                p.Subprojects = utils.orderBy(p.Subprojects, 'Id');
            }
        }
    }
])
.controller('Document.Edit.Controller', ['$scope', 'Utils', 'DocumentService', function ($scope, utils, documentService) {
    $scope.RootModel.Title = "Modificar Documento";
    $scope.Model = {};
    var m = $scope.Model;
    m.Doc = {};

    documentService.getOptions()
                .then(function (data) {
                    orderProjects(data.Projects);
                    m.Options = data;
                });

    $scope.getAutosuggestDoc = function() {
        var filter = { DocNumber: m.Doc.DocNumber };
        var filtersStr = angular.toJson(filter);
        return documentService.getAutosuggestDoc('DocNumber', m.Doc.DocNumber, filtersStr)
        .then(function (data) {
            return utils.orderBy(data, 'value');
        });
    };

    $scope.docChanged = function () {
        clearDoc();
    };

    $scope.docSelected = function () {
        var filter = { DocNumber: m.Doc.DocNumber };
        documentService.getDocument(filter)
                    .then(function (data) {
                        if (data !== null) {
                            m.Doc = data;
                            fillDocProject();
                        } else {
                            clearDoc();
                        }
                    });
    };

    $scope.subprojectVisible = function () {
        if (utils.isNullOrUndefined(m.Options)) return true;
        if (utils.isNullOrUndefined(m.Subprojects)) return true;
        return m.Subprojects.length > 1;
    };

    $scope.submit = function () {
        m.Saved = false;
        $scope.docForm.$setPristine();
        documentService.update(m.Doc)
                .then(function (data) {
                    m.Saved = true;
                });
    };

   function clearDoc() {
        m.Doc.Id = 0;
        m.Doc.Title = '';
        m.Doc.Project = null;
        m.Doc.SpecialtyId = null;
        m.Doc.ProjectId = null;
        m.Doc.TypeId = null;
    }

    function fillDocProject() {
        var project = null;
        var subproject = null;
        for (var i in m.Options.Projects) {
            var proj = m.Options.Projects[i];
            for (var j in proj.Subprojects) {
                var subproj = proj.Subprojects[j];
                if (subproj.Id == m.Doc.ProjectId) {
                    project = proj;
                    subproject = subproj;
                    break;
                }
            }
            if(project != null) break;
        }
        m.Doc.Project = subproject;
        m.Subprojects = project.Subprojects;
    }

    function orderProjects(projects) {
        for (var i in projects) {
            var p = projects[i];
            p.Subprojects = utils.orderBy(p.Subprojects, 'Id');
        }
    }
}
])
;