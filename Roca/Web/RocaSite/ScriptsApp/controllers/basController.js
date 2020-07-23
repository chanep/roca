angular.module('app').controller('Bas.New.Controller',
    [
        '$scope', 'Utils', 'BasService',
        function ($scope, utils, basService) {

            $scope.RootModel.Title = "Alta de Codigos Bas";
            $scope.Model = {};

            var m = $scope.Model;

            getAllElementTypes();

            $scope.elementTypeChanged = function () {
                m.fieldDefinition = m.elementType.FieldDefinitions[0];
                clearBas();
                getExistingCodes(m.fieldDefinition.Code, true);
            }

            $scope.fieldChanged = function () {
                clearBas();
                getExistingCodes(m.fieldDefinition.Code, true);
            }

            $scope.basChanged = function () {
                filterExistingCodes();
            }

            $scope.addCode = addCode;
            $scope.isCodeValid = isCodeValid;


            function clearBas() {
                m.bas = { Field: m.fieldDefinition.Code, Code: '', Description: '', ShortDescription: '' }
            }


            function isCodeValid() {
                if (utils.isNullOrUndefined(m.fieldDefinition))
                    return false;
                var fieldLength = m.fieldDefinition.Length;
                if (fieldLength != m.bas.Code.length)
                    return false;
                if (m.bas.Description === '')
                    return false;
                if (isBasDuplicated())
                    return false;
                return true;
            }

            function isBasDuplicated() {
                if (utils.isNullOrUndefined(m.existingCodes))
                    return false;
                if (utils.filterBy(m.existingCodes, 'Description', m.bas.Description).length > 0)
                    return true;
                if (utils.filterBy(m.existingCodes, 'Code', m.bas.Code).length > 0)
                    return true;
                return false;
            }

            function addCode() {
                basService.addCode(m.bas)
                    .then(function (data) {
                        refreshListAfterAdd(data);
                    });
            }

            function filterExistingCodes() {
                function codeFilter(item) {
                    if (m.bas.Code === '' && m.bas.Description === '' && m.bas.ShortDescription === '')
                        return true;
                    if (m.bas.Code !== '' && item.Code.toLowerCase().indexOf(m.bas.Code.toLowerCase()) !== -1)
                        return true;
                    if (m.bas.Description !== '' && item.Description.toLowerCase().indexOf(m.bas.Description.toLowerCase()) !== -1)
                        return true;
                    if (!utils.isNullOrUndefined(item.ShortDescription) && m.bas.ShortDescription !== '' && item.ShortDescription.toLowerCase().indexOf(m.bas.ShortDescription.toLowerCase()) != -1)
                        return true;
                    return false;
                }

                    m.existingCodesFiltered = m.existingCodes.filter(codeFilter);
            }

            function refreshListAfterAdd(newBas) {
                basService.getAllCodesByField(m.fieldDefinition.FullCode, true)
                    .then(function (data) {
                        m.existingCodes = data.filter(function (bas) {
                            return bas.Code !== newBas.Code;
                        });
                        m.existingCodes.unshift(newBas);
                        clearBas();
                        filterExistingCodes();
                    });
            }

            function getAllElementTypes() {
                return basService.getAllElementTypes()
                    .then(function (data) {
                        m.elementTypes = data;
                        m.elementType = data[0];
                        m.fieldDefinition = m.elementType.FieldDefinitions[0];
                        clearBas();
                        getExistingCodes(m.fieldDefinition.Code, true);
                    });
            }

            function getExistingCodes(fieldFullCode, nocache) {
                return basService.getAllCodesByField(fieldFullCode, nocache)
                    .then(function (data) {
                        m.existingCodes = data;
                        filterExistingCodes();
                    });
            }


        }
    ])
    .controller('Bas.Edit.Controller', [
        '$scope', 'Utils', 'BasService', 'DialogService', 'GridService', function ($scope, utils, basService, dialogService, gridService) {
            var pageSize = 100;
            $scope.RootModel.Title = "Modificacion de Codigos Bas";

            $scope.Model = {};
            var m = $scope.Model;
            m.error = "";
            m.errorVisible = false;

            (function initGrid() {
                var columnDefs = [
                    { displayName: "Id", field: "Id", visible: false, cellClass: "ngGridCell", headerClass: "ngGridHeader" },
                    { displayName: "Codigo", field: "Code", enableCellEdit: false, cellClass: "ngGridCell", headerClass: "ngGridHeader", width: "80px" },
                    { displayName: "Descripcion Corta", field: "ShortDescription", enableCellEdit: true, cellClass: "ngGridCell", headerClass: "ngGridHeader", width: "250px" },
                    { displayName: "Descripcion", field: "Description", enableCellSelection: true, enableCellEdit: true, cellClass: "ngGridCell", headerClass: "ngGridHeader" },
                    { displayName: "", sortable: false, cellClass: "ngGridCell", headerClass: "ngGridHeader", width: "30px", cellTemplate: gridService.deleteFunctionTemplate("deleteBasCode") }
                ];


                m.totalServerItems = 0;
                m.pagingOptions = {
                    pageSizes: [pageSize, pageSize * 2, pageSize * 4],
                    pageSize: pageSize,
                    currentPage: 1
                };

                m.gridOptions = {
                    data: "Model.existingCodes",
                    columnDefs: columnDefs,
                    enablePaging: false,
                    showFooter: false,
                    enableRowSelection: false,
                    enableColumnResize: true,
                    enableCellSelection: true,
                    pagingOptions: m.pagingOptions,
                    sortInfo: { fields: ['Id'], directions: ['asc'] }
                };

                $scope.$on('ngGridEventEndCellEdit', function (event) {
                    var basCode = event.targetScope.row.entity;
                    basService.updateCode(basCode)
                        .then(function () {
                            getExistingCodes(m.fieldDefinition.Code, true);
                        }, function (data) {
                            getExistingCodes(m.fieldDefinition.Code, true);
                            m.error = data.Error;
                            m.errorVisible = true;
                        });
                });

                $scope.deleteBasCode = function (basCode) {
                    dialogService.confirmationDialog("Eliminar Codigo", "Desea eliminar \"" + basCode.Description + '"?')
                        .then(function () {
                            return basService.deleteCode(basCode.Id);
                        })
                        .then(function () {
                            getExistingCodes(m.fieldDefinition.Code, true);
                        });
                }
            })();


            getAllElementTypes();

            $scope.elementTypeChanged = function () {
                m.fieldDefinition = m.elementType.FieldDefinitions[0];
                getExistingCodes(m.fieldDefinition.Code, true);
            }

            $scope.fieldChanged = function () {
                getExistingCodes(m.fieldDefinition.Code, true);
            }


            function getAllElementTypes() {
                return basService.getAllElementTypes()
                    .then(function (data) {
                        m.elementTypes = data;
                        m.elementType = data[0];
                        m.fieldDefinition = m.elementType.FieldDefinitions[0];
                        getExistingCodes(m.fieldDefinition.Code, true);
                    });
            }

            function getExistingCodes(fieldCode, nocache) {
                m.errorVisible = false;
                return basService.getAllCodesByField(fieldCode, nocache)
                    .then(function (data) {
                        m.existingCodes = data;
                    });
            }


        }
    ])
    .controller('Bas.ElementDetails.Controller', [
        '$scope', '$stateParams', 'Utils', 'BasService',
            function($scope, $stateParams, utils, basService) {

                $scope.Model = {};
                var m = $scope.Model;

                m.units = ["pz", "u", "m"];
                m.saved = false;
                m.error = "";
                m.errorVisible = false;

                var elementId = $stateParams.id;
                if (elementId != 0)
                    m.editMode = true;
                else
                    m.editMode = false;


                if (m.editMode) {
                    $scope.RootModel.Title = "Modificacion de Elementos";

                    getAllElementTypes().
                        then(function(types) {
                            return getElement(elementId);
                        })
                        .then(function(element) {
                            m.elementType = utils.findById(m.elementTypes, element.TypeId);
                            loadFields();
                        });


                    function getElement(elementId) {
                        return basService.getElement(elementId)
                            .then(function(data) {
                                m.element = data;
                                return data;
                            });
                    }
                } else {
                    $scope.RootModel.Title = "Alta de Elementos";
                    getAllElementTypes().
                        then(function(types) {
                            m.elementType = types[0];
                            return getEmptyElement(m.elementType.Id);
                        })
                        .then(function() {
                            loadFields();
                        });


                    $scope.elementTypeChanged = function() {
                        getEmptyElement(m.elementType.Id)
                            .then(function() {
                                loadFields();
                            });
                    }

                    function getEmptyElement(typeId) {
                        return basService.getEmptyElement(typeId)
                            .then(function(data) {
                                m.element = data;
                                m.element.Unit = m.units[0];
                                return data;
                            });
                    }

                }


                $scope.getAutosuggestBasCode = getAutosuggestBasCode;
                $scope.basCodeSelected = basCodeSelected;
                $scope.basCodeChanged = basCodeChanged;
                $scope.getElementCode = getElementCode;
                $scope.getElementDescription = getElementDescription;
                $scope.canSave = canSave;
                $scope.save = save;
                $scope.isValidNumber = utils.isValidNumber;
                $scope.isValidInt = utils.isValidInt;

                function save() {
                    m.errorVisible = false;
                    m.saved = false;
                    m.element.FullCode = getElementCode();
                    basService.saveElement(m.element)
                        .then(function(data) {
                            m.element = data;
                            m.element.Id = 0;
                            pristineForm();
                            m.saved = true;
                        }, function(data) {
                            m.error = data.Error;
                            m.errorVisible = true;
                        });
                }

                function canSave() {
                    if (!$scope.elementForm.$dirty)
                        return false;
                    if (utils.isNullOrUndefined(m.element))
                        return false;
                    if ($scope.elementForm.$invalid)
                        return false;
                    for (var i in m.element.Fields) {
                        if (m.element.Fields[i].BasCodeId == 0)
                            return false;
                    }
                    return true;
                }

                function pristineForm() {
                    $scope.elementForm.$setPristine();
                }

                function getElementCode() {
                    var code = "";
                    if (utils.isNullOrUndefined(m.fields))
                        return code;

                    for (var i in m.fields) {
                        var f = m.fields[i];
                        var subcode;
                        if (utils.isNullOrUndefined(f.basCode)) {
                            subcode = new Array(f.fieldDefinition.Length).join("-");
                        } else {
                            subcode = f.basCode.Code;
                        }
                        code += subcode;
                    }
                    return code;
                }

                function getElementDescription() {
                    var desc = "";
                    if (utils.isNullOrUndefined(m.fields))
                        return desc;
                    for (var i in m.fields) {
                        var f = m.fields[i];
                        if (!utils.isNullOrUndefined(f.basCode)) {
                            desc += f.basCode.Description + ", ";
                        }
                    }
                    return desc.substr(0, desc.length - 2);
                }


                function getAllElementTypes() {
                    return basService.getAllElementTypes()
                        .then(function(data) {
                            m.elementTypes = data;
                            return data;
                        });
                }

                function loadFields() {
                    m.fields = [];
                    var fieldCodes = [];

                    for (var i = 0; i < m.elementType.FieldDefinitions.length; i++) {
                        var fieldDef = m.elementType.FieldDefinitions[i];

                        var field = {};
                        field.fieldDefinition = fieldDef;
                        m.fields[i] = field;

                        fieldCodes[i] = fieldDef.Code;
                    }

                    basService.getAllCodesByFields(fieldCodes)
                        .then(function(data) {
                            for (var i in data) {
                                m.fields[i].basCodes = data[i];
                                var basCodeId = m.element.Fields[i].BasCodeId;
                                if (m.fields[i].basCodes.length === 1) {
                                    m.fields[i].basCode = m.fields[i].basCodes[0];
                                    m.element.Fields[i].BasCodeId = m.fields[i].basCode.Id;
                                }
                                else
                                    m.fields[i].basCode = utils.findById(m.fields[i].basCodes, basCodeId);
                            }
                        });
                }


                function getAutosuggestBasCode(field) {
                    var filteredBasCodes = field.basCodes.filter(function(basCode) {
                        return basCode.Description.toLowerCase().indexOf(field.searchTerm.toLowerCase()) !== -1;
                    });
                    return filteredBasCodes;
                };

                function basCodeSelected(field) {
                    field.basCode = field.basCodes.filter(function(basCode) {
                        return basCode.Description.indexOf(field.searchTerm) !== -1;
                    })[0];
                    field.searchTerm = "";
                    basCodeChanged(field);
                };

                function basCodeChanged(field) {
                    if(!utils.isNullOrUndefined(field))
                        m.element.Fields[field.fieldDefinition.Order].BasCodeId = field.basCode.Id;
                };

            }
        ])

    .controller('Bas.ElementList.Controller',
    ['$scope', 'Utils', 'DialogService', 'BasService', 'UserService',
        function($scope, utils, dialogService, basService, userService) {

            $scope.RootModel.Title = "Lista de Elementos";
            $scope.Model = {};

            var m = $scope.Model;
            m.pageInfo = { totalItems: 0, itemsPerPage: 50, currentPage: 1 };
            m.filter = { FullCode: "", Description: "", Skip: 0, Take: m.pageInfo.itemsPerPage };

            getAllElementTypes().then(function() {
                getAllElements();
            });


            $scope.filterChanged = filterChanged;
            $scope.pageChanged = pageChanged;
            $scope.clearFilter = clearFilter;
            $scope.deleteElement = deleteElement;
            $scope.canDelete = canDelete;
            $scope.canEdit = canEdit;
            $scope.getSisepcCatalog = getSisepcCatalog;

            function getSisepcCatalog() {
                var filter = angular.copy(m.filter);
                filter.Skip = null;
                filter.Take = null;
                basService.getSisepcCatalog(filter);
            }


            function canDelete() {
                return userService.isInRole("BasAdmin");
            }

            function canEdit() {
                return userService.isInRole("BasWrite");
            }

            function pageChanged() {
                m.filter.Skip = (m.pageInfo.currentPage - 1) * m.pageInfo.itemsPerPage;
                getAllElements();
            }

            function filterChanged() {
                m.pageInfo.currentPage = 1;
                m.filter.Skip = 0;
                getAllElements();
            }

            function clearFilter() {
                m.filter.Description = "";
                m.filter.FullCode = "";
                filterChanged();
            }

            function deleteElement(element) {
                dialogService.confirmationDialog("Eliminar Elemento", "Desea eliminar  " + element.FullCode + "?<br>" + element.FamilyDescription )
                    .then(function() {
                        return basService.deleteElement(element.Id);
                    })
                    .then(function() {
                        getAllElements();
                    });
            }


            function getAllElementTypes() {
                return basService.getAllElementTypes()
                    .then(function(data) {
                        m.elementTypes = data;
                        m.elementTypes.unshift({ Id: 0, Name: 'Todos' });
                        m.filter.TypeId = m.elementTypes[0].Id;
                    });
            }

            function getAllElements() {
                return basService.getAllElements(m.filter)
                    .then(function(data) {
                        m.pageInfo.totalItems = data.Count;
                        m.elements = data.Elements;
                    });
            }

        }
    ])
    
    ;