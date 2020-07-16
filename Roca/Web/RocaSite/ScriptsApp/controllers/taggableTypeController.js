angular.module('app').controller('Taggable.Types.Controller', ['$scope', 'TaggableService', function ($scope, taggableService) {
    $scope.Model = {};
    var m = $scope.Model;
    $scope.RootModel.Title = "Tipos de Materiales Tagueables";

    m.SubtypeEditMode = false;
    m.TypeEditMode = false;

    taggableService.getSpecialties()
        .then(function(data) {
            m.Specialties = data;
            m.Specialty = m.Specialties[0];
            updateTypes();
        });

    $scope.specialtyChanged = function () {
        updateTypes();
    };

    $scope.typeChanged = function () {
        m.TypeEditMode = false;
        updateSubtypes();
    };

    $scope.subTypeChanged = function () {
        m.SubtypeEditMode = false;
        m.CurrentType = m.Subtype;
        updateAttributes();
    };


    $scope.addType = function() {
        var type = { Id: 0, SpecialtyId: m.Specialty.Id, Name: m.NewType };
        taggableService.addType(type)
            .then(function(data) {
                m.Types.push(data);
                m.Type = data;
                m.NewType = "";
                updateSubtypes();
            });
    };

    $scope.addSubtype = function() {
        var subType = { Id: 0, SpecialtyId: m.Specialty.Id, ParentId: m.Type.Id, Name: m.NewSubtype };
        taggableService.addType(subType)
            .then(function(data) {
                m.Type.Subtypes.push(data);
                m.Subtype = data;
                m.CurrentType = data;
                m.NewSubtype = "";
                updateAttributes();
            });
    };

    $scope.canSaveAttributes = function() {
        if (angular.isUndefined(m.Type))
            return false;
        if (angular.isUndefined(m.Attributes))
            return false;
        for (var i = 0; i < m.Attributes.length; i++) {
            if (angular.isUndefined(m.Attributes[i]))
                return false;
            if (m.Attributes[i].Name.length > 0)
                return true;
        }
        return false;
    };

    $scope.saveAttributes = function() {
        var attrAux = [];
        for (var i = 0; i < m.Attributes.length; i++) {
            m.Attributes[i].TypeId = m.CurrentType.Id;
            if (m.Attributes[i].Name != "") {
                attrAux.push(m.Attributes[i]);
            }
        }
        taggableService.saveAttributes(attrAux)
            .then(function() {
                m.CurrentType.Attributes = m.Attributes;
            });
    };

    $scope.deleteType = function() {
        if (typeof (m.Type) === 'undefined' && m.Type === null)
            return;
        taggableService.deleteType(m.Type)
            .then(function() {
                updateTypes();
            });
    };

    $scope.editType = function () {
        if (typeof (m.Type) === 'undefined' && m.Type === null)
            return;
        m.TypeEditMode = true;
        m.UpdatedType = m.Type.Name;
    };

    $scope.saveType = function () {
        m.TypeEditMode = false;
        var auxType = { Id: m.Type.Id, SpecialtyId: m.Type.SpecialtyId, ParentId: m.Type.ParentId, Name: m.UpdatedType };
        m.UpdatedType = "";
        taggableService.updateType(auxType)
            .then(function () {
                m.Type.Name = auxType.Name;
            });
    };

    $scope.cancelSaveType = function () {
        m.TypeEditMode = false;
        m.UpdatedType = "";
    };

    $scope.deleteSubtype = function () {
        if (typeof (m.Subtype) === 'undefined' || m.Subtype === null)
            return;
        taggableService.deleteType(m.Subtype)
            .then(function () {
                var index = m.Type.Subtypes.indexOf(m.Subtype);
                m.Type.Subtypes.splice(index, 1);
                updateSubtypes();
            });
    };

    $scope.editSubtype = function () {
        if (typeof (m.Subtype) === 'undefined' || m.Subtype === null)
            return;
        m.SubtypeEditMode = true;
        m.UpdatedSubtype = m.Subtype.Name;
    };

    $scope.saveSubtype = function () {
        m.SubtypeEditMode = false;
        var auxType = { Id: m.Subtype.Id, SpecialtyId: m.Subtype.SpecialtyId, ParentId: m.Subtype.ParentId, Name: m.UpdatedSubtype };
        m.UpdatedSubtype = "";
        taggableService.updateType(auxType)
            .then(function () {
                m.Subtype.Name = auxType.Name;
            });
    };

    $scope.cancelSaveSubtype = function () {
        m.SubtypeEditMode = false;
        m.UpdatedSubtype = "";
    };


    function updateTypes() {
        taggableService.getTypes(m.Specialty.Id)
            .then(function (data) {
                m.Types = data;
                if (m.Types.length > 0) {
                    m.Type = m.Types[0];
                } else {
                    m.Type = null;
                }
                updateSubtypes();
            });
    }

    function updateSubtypes() {
        if (typeof (m.Type) !== 'undefined' && m.Type !== null && typeof (m.Type.Subtypes) !== 'undefined' && m.Type.Subtypes !== null && m.Type.Subtypes.length > 0) {
            m.CurrentType = m.Type.Subtypes[0];
            m.Subtype = m.Type.Subtypes[0];
        } else {
            m.Subtype = null;
            m.CurrentType = m.Type;
        }
        updateAttributes();
    }

    function updateAttributes() {
        if (typeof (m.CurrentType) !== 'undefined' && m.CurrentType !== null) {
            m.Attributes = m.CurrentType.Attributes;
        } else {
            m.Attributes = [];
        }

        for (var i = m.Attributes.length; i < 5; i++) {
            m.Attributes[i] = { Name: "" };
        }
    }


}]);