angular.module('app').factory('ProjectService', ['$http', function ($http) {

    var getProjects = function () {
        return $http.get("Project/GetProjects")
                .then(function (data) { return data.data; });
    };


    var selectProject = function(id) {
        return $http.post("Project/SelectProject", { id: id })
            .then(function (data) { return data.data; });
    };

    

    return {
        getProjects: getProjects,
        selectProject: selectProject
    };
}
]);