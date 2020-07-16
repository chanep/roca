angular.module('app').controller('TimeSheet.Details.Controller',
    ['$scope', '$filter', '$stateParams', 'TimeSheetService', function($scope, $filter, $stateParams, timeSheetService) {
            $scope.RootModel.Title = "Carga de Horas";
            $scope.Model = {};
            var m = $scope.Model;
            var MAX_HOURS = 40;
            var CLOSED = 2;

            timeSheetService.getDetailsOptions()
                .then(function (data) {
                    m.Options = data;
                    if ($stateParams.id == 0) {
                        var date = new Date();
                        getTimeSheetByDate(date.getTime());
                    } else {
                        getTimeSheetById($stateParams.id);
                    }
                });



            $scope.specialtyChanged = getTimeSheetByControlDate;
            $scope.dateChanged = getTimeSheetByControlDate;

            $scope.itemProjectChanged = function(item) {
                var p = getById(m.Options.Projects, item.SubprojectParentId);
                item.Subprojects = p.Subprojects;
                item.SubprojectId = item.Subprojects[0].Id;
                if (!isNullOrUndefined(item.Document)) {
                    item.Document.TypeId = 0;
                    item.Document.DocNumber = '';
                    item.Document.Title = '';
                }
            };

            $scope.itemSubprojectChanged = function(item) {
                if (!isNullOrUndefined(item.Document)) {
                    item.Document.TypeId = 0;
                    item.Document.DocNumber = '';
                    item.Document.Title = '';
                }
            };

            $scope.docItemChanged = function (item, field) {
                var value = item.Document[field];
                if (field == 'TypeId') {
                    item.Document.DocNumber = '';
                    item.Document.Title = '';
                }
                if (value === '') {
                    item.Document.TypeId = 0;
                    item.Document.DocNumber = '';
                    item.Document.Title = '';
                }
                var filter = createDocFilter(item);
                return timeSheetService.getDocument(filter)
                    .then(function (data) {
                        if (data !== null) {
                            item.Document = data;
                            item.DocumentId = data.Id;

                        } else {
                            item.Document.Id = 0;
                        }
                    });
            };

            $scope.subprojectVisible = function(item) {
                if (isNullOrUndefined(item.Subprojects)) return true;
                return item.Subprojects.length > 1;
            };


            $scope.disabledDays = function (date, mode) {return (mode === 'day' && date.getDay() !== 5);};

            $scope.toogleHelp = toogleHelp;
            $scope.openDatePicker = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                $scope.Model.DatePickerOpened = true;
            };

            $scope.isControlDateValid = isControlDateValid;
            $scope.isHourValid = isHourValid;
            $scope.isItemValid = isItemValid;
            $scope.isItemAndHourValid = isItemAndHourValid;
            $scope.isProjectValid = isProjectValid;
            $scope.isTimeSheetValid = isTimeSheetValid;

            $scope.getDocItems = getDocItems;
            $scope.getTaskItems = getTaskItems;
            $scope.getTotalDocHours = getTotalDocHours;
            $scope.getTotalTaskHours = getTotalTaskHours;
            $scope.getTotalHours = getTotalHours;
            $scope.getAutosuggestDoc = getAutosuggestDoc;

            $scope.submit = submit;
            $scope.addEmptyDoc = addEmptyDoc;
            $scope.addEmptyTask = addEmptyTask;
            $scope.deleteItem = deleteItem;

            $scope.canCopyPreviousItems = function () { return getTotalHours() == 0; }
            $scope.canModifyControlDate = function () { return ($stateParams.id == 0);}
            $scope.canModify = canModify;
            $scope.canCopyFromPrevious = function() {return getTotalHours() == 0;}
            $scope.copyFromPrevious = copyFromPrevious;



            function loadTimeSheet(data) {
                m.TimeSheet = data;
                var dateStr = $filter('jsonDate')(m.TimeSheet.ControlDate, 'yyyy-MM-dd');
                var date = new Date(dateStr); 
                m.TimeSheet.ControlDateDate = new Date(date.getTime() + 60000 * date.getTimezoneOffset());//si no se suma el offfset, da el dia anterior por el timezone
                for (var i in m.TimeSheet.Items) {
                    var item = m.TimeSheet.Items[i];
                    item.Subprojects = getById(m.Options.Projects, item.SubprojectParentId).Subprojects;
                }
                m.TimeSheet.Items = $filter('orderBy')(m.TimeSheet.Items, 'Id');
                if (isNullOrUndefined(data.LeaderId) || data.LeaderId == 0) {
                    selectLastLeader();
                }
                if (!m.Saved) {
                    if (getDocItems().length == 0) {
                        addEmptyDoc();
                    }
                    if (getTaskItems().length == 0) {
                        addEmptyTask();
                    }
                }
            }

            function getTimeSheetByControlDate() {
                //var aux = m.TimeSheet.ControlDateStr.split('/');
                //var date = new Date(aux[2], aux[1], aux[0]);
                if (isControlDateValid()) {
                    var date = m.TimeSheet.ControlDateDate.getTime();
                    getTimeSheetByDate(date, m.TimeSheet.SpecialtyId);
                }
            }

            function getTimeSheetByDate(date, specialtyId) {
                timeSheetService.getByDate(date, specialtyId)
                    .then(loadTimeSheet);
            }

            function getTimeSheetById(id) {
                timeSheetService.getFull(id)
                    .then(loadTimeSheet);
            }

            function getAutosuggestDoc(item, field) {
                var filter = createDocFilter(item);
                var filtersStr = angular.toJson(filter);
                return timeSheetService.getAutosuggestDoc(field, item.Document[field], filtersStr)
                    .then(function (data) {
                        return $filter('orderBy')(data, 'value');
                    });
            };

            function getDocItems() {
                var docs = [];
                if (angular.isDefined(m.TimeSheet)) {
                    angular.forEach(m.TimeSheet.Items, function (item) {
                        if (item.Document !== null) this.push(item);
                    }, docs);
                }
                return docs;
            }

            function getTaskItems() {
                var tasks = [];
                if (angular.isDefined(m.TimeSheet)) {
                    angular.forEach(m.TimeSheet.Items, function (item) {
                        if (item.TaskId !== null) this.push(item);
                    }, tasks);
                }
                return tasks;
            }

            function getTotalTaskHours() {
                var items = getTaskItems();
                var hh = 0;
                for (var index in items) {
                    if (isItemAndHourValid(items[index])) hh += items[index].Hours;
                }
                return hh;
            }

            function getTotalDocHours() {
                var items = getDocItems();
                var hh = 0;
                for (var index in items) {
                    if (isItemAndHourValid(items[index])) hh += items[index].Hours;
                }
                return hh;
            }

            function getTotalHours() { return getTotalDocHours() + getTotalTaskHours(); }

            function addEmptyItem() {
                var item = {
                    Id: 0,
                    TimeSheetId: m.TimeSheet.Id,
                    SubprojectId: 0,
                    SubprojectParentId: 0,
                    TaskId: null,
                    Document: null,
                    Hours: 0
                }
                copyPreviousProject(item, m.TimeSheet.Items);
                return item;
            }

            function addEmptyDoc() {
                var item = addEmptyItem();
                item.Document = { Id: 0, TypeId: 0, DocNumber: '', Title: '' };
                m.TimeSheet.Items.push(item);
            }

            function addEmptyTask() {
                var item = addEmptyItem();
                item.TaskId = 0;
                m.TimeSheet.Items.push(item);
            }

            function deleteItem(item) {
                for (var i = 0; i < m.TimeSheet.Items.length; i++)
                    if (m.TimeSheet.Items[i] === item) {
                        m.TimeSheet.Items.splice(i, 1);
                        break;
                    }
            }

            function copyPreviousProject(item, prevItems) {
                if (prevItems.length > 0) {
                    for (var i = prevItems.length - 1; i >= 0; i--) {
                        var lastItem = prevItems[i];
                        if (lastItem.SubprojectParentId != 0) {
                            item.SubprojectId = lastItem.SubprojectId;
                            item.Subprojects = lastItem.Subprojects;
                            item.SubprojectParentId = lastItem.SubprojectParentId;
                            return;
                        }
                    }
                }
            }

            function submit() {
                $scope.tsForm.$setPristine();
                var items = [];
                for (var i in m.TimeSheet.Items) {
                    var item = m.TimeSheet.Items[i];
                    if (isItemAndHourValid(item)) items.push(item);
                }
                m.TimeSheet.Items = items;
                timeSheetService.save(m.TimeSheet)
                    .then(function (data) {
                        m.Saved = true;
                        loadTimeSheet(data);
                    });
            };

            function createDocFilter(item) {
                var filter = {
                    DocNumber: item.Document.DocNumber,
                    Title: item.Document.Title,
                    TypeId: item.Document.TypeId,
                    //SpecialtyId: m.TimeSheet.SpecialtyId,
                    SpecialtyId: null,
                    ProjectId: item.SubprojectId,
                };
                return filter;
            }

            function toogleHelp() {
                if (!m.showHelp) {
                    m.showHelp = true;
                    m.TimeSheetHelpTitle = "Datos de la planilla";
                    //m.LeaderHelp = "Elegi tu lider";
                    m.DateHelp = "Elegi la fecha correspondiente al Viernes de la semana que queres cargar. La fecha no se puede apartar demasiado de la fecha actual";

                    m.DocHelpTitle = "Busqueda de documentos";
                    m.DocHelp = "Escribi parte del titulo o codigo del documento";
                    //m.DocProjectHelp = "Primero elegi el proyecto (y subproyecto) del documento";

                    m.TaskHelpTitle = "Carga de Actividades";
                    //m.TaskProjectHelp = "Primero elegi el proyecto (y subproyecto) de la actividad";

                    m.SaveHelpTitle = "Salvar planilla";
                    m.SaveHelp = "Para poder salvar, el lider y la fecha de control deben ser validas y las horas totales deben ser mayores que 0 y no mayores que 40";
                    m.CopyHelpTitle = "Copiar";
                    m.CopyHelp = "Podes copiar los detalles, documentos y actividades de tu ultima planilla. Para poder copiar no debes tener documentos o actividades ya ingresadas";
                } else {
                    m.showHelp = false;
                    m.TimeSheetHelpTitle = "";
                    m.LeaderHelp = "";
                    m.DateHelp = "";

                    m.DocHelpTitle = "";
                    m.DocHelp = "";
                    m.DocProjectHelp = "";

                    m.TaskHelpTitle = "";
                    m.TaskProjectHelp = "";

                    m.SaveHelpTitle = "";
                    m.SaveHelp = "";
                    m.CopyHelpTitle = "";
                    m.CopyHelp = "";
                }
            }

            function canModify() {
                //if ($stateParams.id == 0) return true;
                if (isNullOrUndefined(m.TimeSheet) || isNullOrUndefined(m.TimeSheet.ControlDateDate)) return false;
                if (m.TimeSheet.Status == CLOSED) return false;
                var date = m.TimeSheet.ControlDateDate.getTime();
                var minDate = new Date();
                minDate.setTime(minDate.getTime() - (1000 * 60 * 60 * 24 * 8));
                if (date < minDate) return false;
                return true;
            }

            function copyFromPrevious() {
                timeSheetService.getLast(m.TimeSheet.SpecialtyId)
                    .then(function (data) {
                        if (data != null) {
                            data.ControlDate = m.TimeSheet.ControlDate;
                            loadTimeSheet(data);
                        }
                    });
            };

            function selectLastLeader() {
                timeSheetService.getLast(m.TimeSheet.SpecialtyId)
                    .then(function (data) {
                        if (data != null) {
                            m.TimeSheet.LeaderId = data.LeaderId;
                        }
                    });
            }

            function isControlDateValid() {
                if (angular.isUndefined(m.TimeSheet) || angular.isUndefined(m.TimeSheet.ControlDateDate)) return false;
                var date = m.TimeSheet.ControlDateDate.getTime();
                if (isNaN(date)) return false;
                var maxDate = new Date();
                maxDate.setTime(maxDate.getTime() + 1000 * 60 * 60 * 24 * 14);
                if (date > maxDate) return false;
                var minDate = new Date(2014, 1, 1);
                if (date < minDate) return false;
                return true;
            }

            function isItemAndHourValid(item) {
                if (!isItemValid(item)) return false;
                if (!isHourValid(item.Hours)) return false;
                if (item.Hours < 1) return false;
                return true;
            }

            function isItemValid(item) {
                if (isNullOrUndefined(item.SubprojectId) || item.SubprojectId == 0) return false;
                if (item.TaskId == 0) return false;
                if (item.Document !== null && item.Document.Id == 0) return false;
                return true;
            }


            function isHourValid(hours) {
                if (angular.isUndefined(hours)) return false;
                if (isNaN(hours)) return false;
                if (Math.round(hours) != hours) return false;
                if (hours < 0 || hours > MAX_HOURS) return false;
                return true;
            }

            function isTimeSheetValid() {
                if (isNullOrUndefined(m.TimeSheet)) return false;
                if (!isControlDateValid()) return false;
                if (isNullOrUndefined(m.TimeSheet.LeaderId)) return false;
                var totalHours = getTotalHours();
                if (totalHours > MAX_HOURS || totalHours == 0) return false;
                return true;
            }

            function isProjectValid(item) {
                if (isNullOrUndefined(item.SubprojectId) || item.SubprojectId == 0) return false;
                return true;
            }

            function getById(entities, id) {
                var aux = entities.filter(function(e) {
                    return e.Id == id;
                });
                return aux[0];
            }

            function isNullOrUndefined(obj) {
                if (angular.isUndefined(obj) || obj === null) return true;
                return false;
            }

        }
    ])
    .controller('TimeSheet.List.Controller', [
        '$scope', '$filter', 'TimeSheetService', 'GridService', function($scope, $filter, timeSheetService, gridService) {
            $scope.RootModel.Title = "Carga de Horas";
            $scope.Model = {};
            var m = $scope.Model;
            m.Options = {};
            m.Filter = {};

            (function initGrid() {
                var columnDefs = [
                    { displayName: "Nombre", field: "UserFullName", cellClass: "ngGridCell", headerClass: "ngGridHeader" },
                    { displayName: "Disciplina", field: "SpecialtyName", cellClass: "ngGridCell", headerClass: "ngGridHeader" },
                    { displayName: "Fecha de Control", field: "ControlDate", cellFilter: "jsonDate:'dd/MM/yyyy'", cellClass: "ngGridCell", headerClass: "ngGridHeader" },
                    { displayName: "Lider", field: "LeaderFullName", cellClass: "ngGridCell", headerClass: "ngGridHeader" },
                    { displayName: "", field: "Id", cellClass: "ngGridCell", headerClass: "ngGridHeader", width: "30px", cellTemplate: gridService.viewLinkTemplate("#/TimeSheet/Details/") }
                ];

                m.gridOptions = {
                    data: "Model.TimeSheets",
                    enableRowSelection: false,
                    enableHighlighting: true,
                    columnDefs: columnDefs
                };

            })();

            timeSheetService.getListOptions()
                .then(function(data) {
                    var users = $filter('orderBy')(data.Users, 'FullName');
                    if (users.length > 1) {
                        users.unshift({ Id: 0, FullName: "Todos" });
                    }
                    m.Options.Users = users;
                    m.Filter.UserId = m.Options.Users[0].Id;

                    var specialties = $filter('orderBy')(data.Specialties, 'Name');
                    if (specialties.length > 1) {
                        specialties.unshift({ Id: 0, Name: "Todas" });
                    }
                    m.Options.Specialties = specialties;
                    m.Filter.SpecialtyId = m.Options.Specialties[0].Id;

                    getList();
                });

            $scope.filterChanged = function() {
                getList();
            }

            function getList() {
                timeSheetService.getAll(m.Filter)
                    .then(function(data) {
                        m.TimeSheets = data;
                });
            }


        }
    ])
    .controller('TimeSheet.Status.Controller', [
        '$scope', 'TimeSheetService', 'GridService', function($scope, timeSheetService, gridService) {
            $scope.RootModel.Title = "Carga de Horas - Status de planillas";
            $scope.Model = {};
            var m = $scope.Model;
            var OPEN = 1;
            var CLOSED = 2;

            m.CloseAll = false;
            m.Saved = false;

            m.Filter = {};
            var today = new Date();
            m.Filter.ToDate = new Date(today.getFullYear(), today.getMonth(), today.getDate());
            m.Filter.FromDate = new Date(m.Filter.ToDate.getTime() - 1000*60*60*24*6);
        
            (function initGrid() {
                var columnDefs = [
                    { displayName: "Nombre", field: "UserFullName", cellClass: "ngGridCell", headerClass: "ngGridHeader" },
                    { displayName: "Disciplina", field: "SpecialtyName", cellClass: "ngGridCell", headerClass: "ngGridHeader" },
                    { displayName: "Fecha de Control", field: "ControlDate", cellFilter: "jsonDate:'dd/MM/yyyy'", cellClass: "ngGridCell", headerClass: "ngGridHeader" },
                    { displayName: "Lider", field: "LeaderFullName", cellClass: "ngGridCell", headerClass: "ngGridHeader" },
                    { displayName: "Cerrada", field: "Status", sortable: false, cellClass: "ngGridCell", headerClass: "ngGridHeader", width: "60px", cellTemplate: gridService.boolEditableCellTemplate(CLOSED, OPEN , "statusChanged") }
                ];

                m.gridOptions = {
                    data: "Model.TimeSheets",
                    enableRowSelection: false,
                    enableHighlighting: true,
                    columnDefs: columnDefs
                };

            })();


            getList();

            $scope.openDatePicker = function ($event, openFlag) {
                $event.preventDefault();
                $event.stopPropagation();
                m[openFlag] = true;
            };

            $scope.statusChanged = function (entity) {
                m.Saved = false;
                refreshCloseAllValue();
            };

            $scope.closeAllChanged = function() {
                m.Saved = false;
                m.SomeClosed = false;
                updateTimeSheetsStatus();
            }

            $scope.filterChanged = function() {
                m.Saved = false;
                getList();
            }

            $scope.save = updateAll;

            function updateTimeSheetsStatus() {
                var status = 1;
                if (m.CloseAll) status = 2;
                for (var i in m.TimeSheets) {
                    var t = m.TimeSheets[i];
                    t.Status = status;
                }
            }

            function refreshCloseAllValue() {
                var anyClosed = false;
                var anyOpen = false;
                m.SomeClosed = false;

                for (var i in m.TimeSheets) {
                    var t = m.TimeSheets[i];
                    if (t.Status == CLOSED) anyClosed = true;
                    if (t.Status == OPEN) anyOpen = true;
                    if (anyOpen && anyClosed) {
                        m.SomeClosed = true;
                        break;
                    }
                }

                if (anyClosed) {
                    m.CloseAll = true;
                } else {
                    m.CloseAll = false;
                }
            }

            function updateAll() {
                timeSheetService.updateAll(m.TimeSheets)
                    .then(function() {
                        m.Saved = true;
                        getList();
                });
            }

            function getList() {
                timeSheetService.getAll(m.Filter)
                    .then(function(data) {
                        m.TimeSheets = data;
                        refreshCloseAllValue();
                });
            }

        }
    ])
    .controller('TimeSheet.Defaulters.Controller', [
        '$scope', '$filter', 'TimeSheetService', 'GridService', function($scope, $filter, timeSheetService, gridService) {
            $scope.RootModel.Title = "Carga de Horas - Morosos";
            $scope.Model = {};
            var m = $scope.Model;
            m.Defaulters = [];
            m.MailToAll = "";

            (function initGrid() {
                var columnDefs = [
                    { displayName: "Nombre", field: "User.FullName", cellClass: "ngGridCell", headerClass: "ngGridHeader" },
                    { displayName: "Mail", field: "User.Mail", cellClass: "ngGridCell", headerClass: "ngGridHeader", cellTemplate: gridService.mailLinkTemplate() },
                    { displayName: "Ultima Carga", field: "LastLoad", cellFilter: "jsonDate:'dd/MM/yyyy'", cellClass: "ngGridCell", headerClass: "ngGridHeader" }
                ];

                m.gridOptions = {
                    data: "Model.Defaulters",
                    enableRowSelection: false,
                    enableHighlighting: true,
                    columnDefs: columnDefs,
                    sortInfo: { fields: ['User.FullName'], directions: ['asc'] }
                };

            })();


            timeSheetService.getDefaulters()
                .then(function(data) {
                    m.Defaulters = data;
                    refreshMailToAll();
                });

            function refreshMailToAll() {
                if (m.Defaulters.length < 1) {
                    m.MailToAll = "";
                } else {
                    var href = "mailto:";
                    for (var i in m.Defaulters) {
                        var d = m.Defaulters[i];
                        href += d.User.Mail + ';';
                    }
                    href += '?subject=Carguen%20las%20horas';
                    m.MailToAll = href;
                }

            }

        }
    ])
    .controller('TimeSheet.Reports.Controller', [
        '$scope', 'TimeSheetService', 'GridService', function($scope, timeSheetService, gridService) {
            $scope.RootModel.Title = "Carga de Horas - Reportes";
            $scope.Model = {};
            var m = $scope.Model;

            m.Report = 'ByDoc';
            var columnDefsSet = {};

            m.Filter = {};
            var today = new Date();
            m.Filter.ToDate = new Date(today.getFullYear(), today.getMonth(), today.getDate());
            m.Filter.FromDate = new Date(m.Filter.ToDate.getTime() - 1000*60*60*24*6);
        

            var reportLoader = {
                ByDoc: loadByDocReport,          
                ByTask: loadByTaskReport,
                BySpecialty: loadBySpecialtyReport,
                ByProject: loadByProjectReport
            };

            (function initGrid() {
                columnDefsSet.ByDoc = [
                    { displayName: "Proyecto", field: "Document.Project.ParentShortName", cellClass: "ngGridCell", headerClass: "ngGridHeader", width: "90px" },
                    { displayName: "Subproyecto", field: "Document.Project.ShortName", cellClass: "ngGridCell", headerClass: "ngGridHeader", width: "90px" },
                    { displayName: "Disciplina", field: "Document.SpecialtyName", cellClass: "ngGridCell", headerClass: "ngGridHeader", width: "95px" },
                    { displayName: "Codigo", field: "Document.DocNumber", cellClass: "ngGridCell", headerClass: "ngGridHeader", width: "210px" },
                    { displayName: "Titulo", field: "Document.Title", cellClass: "ngGridCell", headerClass: "ngGridHeader" },
                    { displayName: "HH", field: "Hours", cellClass: "ngGridCell", headerClass: "ngGridHeader", width: "60px" }
                ];

                columnDefsSet.ByTask = [
                    { displayName: "Proyecto", field: "Subproject.ParentShortName", cellClass: "ngGridCell", headerClass: "ngGridHeader", width: "90px" },
                    { displayName: "Subproyecto", field: "Subproject.ShortName", cellClass: "ngGridCell", headerClass: "ngGridHeader", width: "90px" },
                    { displayName: "Actividad", field: "Task.Value", cellClass: "ngGridCell", headerClass: "ngGridHeader" },
                    { displayName: "HH", field: "Hours", cellClass: "ngGridCell", headerClass: "ngGridHeader", width: "60px" }
                ];

                columnDefsSet.BySpecialty = [
                    { displayName: "Proyecto", field: "Subproject.ParentShortName", cellClass: "ngGridCell", headerClass: "ngGridHeader" },
                    { displayName: "Subproyecto", field: "Subproject.ShortName", cellClass: "ngGridCell", headerClass: "ngGridHeader" },
                    { displayName: "Disciplina", field: "Specialty.Name", cellClass: "ngGridCell", headerClass: "ngGridHeader" },
                    { displayName: "HH", field: "Hours", cellClass: "ngGridCell", headerClass: "ngGridHeader", width: "60px" }
                ];

                columnDefsSet.ByProject = [
                    { displayName: "Proyecto", field: "Subproject.ParentShortName", cellClass: "ngGridCell", headerClass: "ngGridHeader" },
                    { displayName: "Subproyecto", field: "Subproject.ShortName", cellClass: "ngGridCell", headerClass: "ngGridHeader" },
                    { displayName: "HH", field: "Hours", cellClass: "ngGridCell", headerClass: "ngGridHeader", width: "60px" }
                ];

                m.columnDefs = columnDefsSet[m.Report];

                m.gridOptions = {
                    data: "Model.Data",
                    enableRowSelection: false,
                    enableHighlighting : true,
                    columnDefs: 'Model.columnDefs',
                    sortInfo: { fields: ['Hours'], directions: ['desc'] },
                    plugins: [new ngGridCsvExportPlugin()]
                };

            })();

            reportLoader[m.Report]();

            $scope.openDatePicker = function ($event, openFlag) {
                $event.preventDefault();
                $event.stopPropagation();
                m[openFlag] = true;
            };

            $scope.filterChanged = function() {
                reportLoader[m.Report]();
            }

            $scope.reportChanged = function () {              
                reportLoader[m.Report]();
            }

            function loadByDocReport() {
                timeSheetService.getByDocReport(m.Filter)
                .then(function (data) {
                    updateColumnDefs();
                    m.Data = data;
                });
            }

            function loadByTaskReport() {
                timeSheetService.getByTaskReport(m.Filter)
                .then(function (data) {
                    updateColumnDefs();
                    m.Data = data;
                });
            }

            function loadBySpecialtyReport() {
                timeSheetService.getBySpecialtyReport(m.Filter)
                .then(function (data) {
                    updateColumnDefs();
                    m.Data = data;
                });
            }

            function loadByProjectReport() {
                timeSheetService.getByProjectReport(m.Filter)
                .then(function (data) {
                    updateColumnDefs();
                    m.Data = data;
                });
            }

            function updateColumnDefs() {
                m.columnDefs = columnDefsSet[m.Report];
            }

        }
    ]);