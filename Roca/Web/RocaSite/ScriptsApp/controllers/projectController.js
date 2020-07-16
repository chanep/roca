angular.module('app').controller('Project.Selection.Controller', ['$scope','$location', '$stateParams', 'ProjectService', function ($scope, $location, $stateParams, projectService) {
    
    $scope.Model = {};
    var m = $scope.Model;

    projectService.getProjects().then(function (data) {
        m.Projects = data.all;
        m.Project = data.current;
    });

    $scope.projectChanged = function () {
        projectService.selectProject(m.Project.Id).then(function () {
            if ($stateParams.from) {
                $location.search({});
                $location.path($stateParams.from);
            }
        });
    };

} ]);