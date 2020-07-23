angular.module('app').controller('TimeSheet.Details.Controller',
    ['$scope', '$stateParams', 'Utils', 'TimeSheetService', 'DocumentService', 'DialogService', 
            function($scope, $stateParams, utils, timeSheetService, documentService, dialogService) {
            
            $scope.RootModel.Title = "Carga de Horas";
            $scope.Model = {};
            var m = $scope.Model;
            m.TimeSheet = timeSheetService.createEmptyTimeSheet();

            var timeSheetId = null;
            if ($stateParams.id != 0) timeSheetId = $stateParams.id;
            timeSheetService.getDetailsOptions(timeSheetId)
                .then(function (data) {
                    orderProjects(data.Projects);
                    m.Options = data;
                    if ($stateParams.id == 0) {
                        var date = utils.getNowTime();
                        getTimeSheetByDate(date.getTime(), m.Options.Specialties[0].Id);
                    } else {
                        getTimeSheetById($stateParams.id);
                    }
                });



            $scope.specialtyChanged = getTimeSheetByControlDate;
            $scope.dateChanged = getTimeSheetByControlDate;

            $scope.itemProjectChanged = function(item) {
                var p = utils.findById(m.Options.Projects, item.SubprojectParentId);
                item.Subprojects = p.Subprojects;
                item.SubprojectId = item.Subprojects[0].Id;
                if (!utils.isNullOrUndefined(item.Document)) {
                    item.Document.TypeId = 0;
                    item.Document.DocNumber = '';
                    item.Document.Title = '';
                }
            };

            $scope.itemSubprojectChanged = function(item) {
                if (!utils.isNullOrUndefined(item.Document)) {
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
            };

            $scope.docSelected = function(item) {
                var filter = createDocFilter(item);
                documentService.getDocument(filter)
                    .then(function(data) {
                        if (data !== null) {
                            item.Document = data;
                            item.DocumentId = data.Id;

                        } else {
                            item.Document.Id = 0;
                        }
                    });
            };

            $scope.taskChanged = function(item) {
//                if (item.TaskId == 10001) {
//                    item.Hours = 40;
//                }
            };

            $scope.subprojectVisible = function(item) {
                if (utils.isNullOrUndefined(item.Subprojects)) return true;
                return (item.Subprojects.length > 1 || item.Subprojects[0].ParentShortName != item.Subprojects[0].ShortName);
            };


            $scope.disabledDays = function (date, mode) {return (mode === 'day' && date.getDay() !== 5);};

            $scope.toogleHelp = toogleHelp;
            $scope.openDatePicker = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                $scope.Model.DatePickerOpened = true;
            };

            $scope.isControlDateValid = function() { return m.TimeSheet.isControlDateValid(); };
            $scope.isItemValid = function(item) { return m.TimeSheet.isItemValid(item); };
            $scope.isItemAndHourValid = function(item) { return m.TimeSheet.isItemAndHourValid(item); };
            $scope.isProjectValid = function(item) { return m.TimeSheet.isProjectValid(item); };
            $scope.isTimeSheetValid = function() { return m.TimeSheet.isTimeSheetValid(); };
            $scope.getDocItems = function() { return m.TimeSheet.getDocItems(); };
            $scope.getTaskItems = function() { return m.TimeSheet.getTaskItems(); };
            $scope.getTotalDocHours = function() { return m.TimeSheet.getTotalDocHours(); };
            $scope.getTotalTaskHours = function () { return m.TimeSheet.getTotalTaskHours();};
            $scope.getTotalHours = function () { return m.TimeSheet.getTotalHours();};
            $scope.addEmptyDoc = function () { return m.TimeSheet.addEmptyDoc();};
            $scope.addEmptyTask = function () { return m.TimeSheet.addEmptyTask();};
            $scope.deleteItem = function (item) { return m.TimeSheet.deleteItem(item);};
            $scope.canModify = function () { return m.TimeSheet.canModify();};

            
            $scope.canCopyPreviousItems = function () { return m.TimeSheet.getTotalHours() == 0; }
            $scope.canModifyControlDate = function () { return ($stateParams.id == 0);}           
            $scope.canCopyFromPrevious = function() {return m.TimeSheet.getTotalHours() == 0;}
            $scope.isHourValid = function (hours) { return m.TimeSheet.isHourValid(hours);};
            $scope.copyFromPrevious = copyFromPrevious;
            $scope.getAutosuggestDoc = getAutosuggestDoc;
            $scope.submit = function() {
                //if (m.TimeSheet.UserId == 38 || m.TimeSheet.UserId == 16) submitConfirmation();
                //else submit();
                submit();
            };

            function submitConfirmation() {
                    dialogService.confirmationDialog("Planilla de Horas", "Esta seguro que los documentos no van a venir resometidos esta vez?")
                    .then(function() {
                            if (m.TimeSheet.UserId == 38) {
                                return dialogService.confirmationDialog("Planilla de Horas", "Esta segurisimo??? Bajar el promedio del sector esta penado por la Solid Rock Control Consortium");
                            }
                            else return dialogService.confirmationDialog("Planilla de Horas", "Haga los documentos a conciencia, no le cause disgustos a Anibal");
                            
                        })
                    .then(function() {
                        submit();
                    });
            }

            function loadTimeSheet(data) {
                m.TimeSheet = data;
                for (var i in m.TimeSheet.Items) {
                    var item = m.TimeSheet.Items[i];
                    item.Subprojects = utils.findById(m.Options.Projects, item.SubprojectParentId).Subprojects;
                }
                if (utils.isNullOrUndefined(m.TimeSheet.LeaderId) || m.TimeSheet.LeaderId == 0) {
                    selectLastLeader();
                }
                if (!m.Saved) {
                    if (m.TimeSheet.getDocItems().length === 0 && m.TimeSheet.getTaskItems().length === 0) {
                        m.TimeSheet.addEmptyDoc();
                        m.TimeSheet.addEmptyTask();
                    }
                }
            }

            function getTimeSheetByControlDate() {
                if (m.TimeSheet.isControlDateValid()) {
                    var date = m.TimeSheet.ControlDateDate.getTime();
                    getTimeSheetByDate(date, m.TimeSheet.SpecialtyId);
                }
            }

            function getTimeSheetByDate(date, specialtyId) {
                m.Saved = false;
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
                return documentService.getAutosuggestDoc(field, item.Document[field], filtersStr)
                    .then(function (data) {
                        return utils.orderBy(data, 'value');
                    });
            };

            function submit() {
                $scope.tsForm.$setPristine();
                var items = [];
                for (var i in m.TimeSheet.Items) {
                    var item = m.TimeSheet.Items[i];
                    if (m.TimeSheet.isItemAndHourValid(item)) items.push(item);
                }
                m.TimeSheet.Items = items;
                timeSheetService.save(m.TimeSheet)
                    .then(function (data) {
                        m.Saved = true;
                        loadTimeSheet(data);
                    });
            };

            function orderProjects(projects) {
                for (var i in projects) {
                    var p = projects[i];
                    p.Subprojects = utils.orderBy(p.Subprojects, 'Id');
                }
            }

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


            function copyFromPrevious() {
                timeSheetService.getLast(m.TimeSheet.SpecialtyId)
                    .then(function (data) {
                        if (data != null) {
                            data.ControlDate = m.TimeSheet.ControlDate;
                            data.ControlDateDate = m.TimeSheet.ControlDateDate;
                            data.Id = 0;
                            for (var i in data.Items) {
                                var item = data.Items[i];
                                item.TimeSheetId = 0;
                                item.Id = 0;
                            }
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

        }
    ])
    .controller('TimeSheet.List.Controller', [
        '$scope', '$filter', 'TimeSheetService', 'GridService', function($scope, $filter, timeSheetService, gridService) {
            $scope.RootModel.Title = "Carga de Horas";
            $scope.Model = {};
            var m = $scope.Model;
            m.Options = {};
            m.Filter = {};
            var pageSize = 25;

            (function initGrid() {
                var columnDefs = [
                    { displayName: "Nombre", field: "UserFullName", cellClass: "ngGridCell", headerClass: "ngGridHeader" },
                    { displayName: "Disciplina", field: "SpecialtyName", cellClass: "ngGridCell", headerClass: "ngGridHeader" },
                    { displayName: "Fecha de Control", field: "ControlDate", cellFilter: "jsonDate:'dd/MM/yyyy'", cellClass: "ngGridCell", headerClass: "ngGridHeader" },
                    { displayName: "Lider", field: "LeaderFullName", cellClass: "ngGridCell", headerClass: "ngGridHeader" },
                    { displayName: "", field: "Id", cellClass: "ngGridCell", headerClass: "ngGridHeader", width: "30px", cellTemplate: gridService.viewLinkTemplate("#/TimeSheet/Details/") }
                ];

                m.footerVisible = false;
                m.totalServerItems = 0;
                m.pagingOptions = {
                    pageSizes: [pageSize, pageSize * 2, pageSize * 4],
                    pageSize: pageSize,
                    currentPage: 1
                };

                m.gridOptions = {
                    data: "Model.PagedTimeSheets",
                    enableRowSelection: false,
                    enableHighlighting: true,
                    virtualizationThreshold: 100,
                    enablePaging: true,
                    showFooter: false,
                    footerTemplate: gridService.footerTemplate('Model.footerVisible'),
                    pagingOptions: m.pagingOptions,
                    totalServerItems: 'Model.totalServerItems',
                    columnDefs: columnDefs
                };

                $scope.$watch('Model.pagingOptions', function(newVal, oldVal) {
                        refreshPagedList();
                }, true);

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


            //client side paging
            function refreshPagedList() {
                if (angular.isDefined(m.TimeSheets)) {
                    var po = m.pagingOptions;
                    m.PagedTimeSheets = m.TimeSheets.slice((po.currentPage - 1) * po.pageSize, po.currentPage * po.pageSize);
                }
            }


            function getList() {
                timeSheetService.getAll(m.Filter)
                    .then(function(data) {
                        m.TimeSheets = data;
                        m.totalServerItems = data.length;
                        m.footerVisible = (m.totalServerItems > m.pagingOptions.pageSize);
                        m.pagingOptions.currentPage = 1;
                        refreshPagedList();
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
                    virtualizationThreshold: 100,
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
            m.Data = {};

            var columnDefsSet = {};

            m.Filter = {};
            var today = new Date();
            m.Filter.ToDate = new Date(today.getFullYear(), today.getMonth(), today.getDate());
            m.Filter.FromDate = new Date(m.Filter.ToDate.getTime() - 1000*60*60*24*6);
        

            var reportLoader = {
                ByDoc: loadByDocReport,    
                ByDocUser: loadByDocUserReport,       
                ByTask: loadByTaskReport,
                ByTaskUser: loadByTaskUserReport,
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

                columnDefsSet.ByDocUser = [
                    { displayName: "Proyecto", field: "Document.Project.ParentShortName", cellClass: "ngGridCell", headerClass: "ngGridHeader", width: "90px" },
                    { displayName: "Subproyecto", field: "Document.Project.ShortName", cellClass: "ngGridCell", headerClass: "ngGridHeader", width: "90px" },
                    { displayName: "Disciplina", field: "Document.SpecialtyName", cellClass: "ngGridCell", headerClass: "ngGridHeader", width: "95px" },
                    { displayName: "Persona", field: "UserFullName", cellClass: "ngGridCell", headerClass: "ngGridHeader", width: "130px" },
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

                columnDefsSet.ByTaskUser = [
                    { displayName: "Proyecto", field: "Subproject.ParentShortName", cellClass: "ngGridCell", headerClass: "ngGridHeader", width: "90px" },
                    { displayName: "Subproyecto", field: "Subproject.ShortName", cellClass: "ngGridCell", headerClass: "ngGridHeader", width: "90px" },
                    { displayName: "Persona", field: "UserFullName", cellClass: "ngGridCell", headerClass: "ngGridHeader", width: "130px" },
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

                m.gridOptions = {};
                
                var gridOptionsCommon = {
                    data: "Model.Data",
                    enableRowSelection: false,
                    enableHighlighting : true,
                    virtualizationThreshold: 100,
                    sortInfo: { fields: ['Hours'], directions: ['desc'] },
                    plugins: [new ngGridCsvExportPlugin({containerPanel: '.exportLinkContainer', linkLabel: 'CSV Export (solo en Chrome)'})]
                };

                m.gridOptions.ByDoc = angular.copy(gridOptionsCommon);
                m.gridOptions.ByDoc.columnDefs = columnDefsSet.ByDoc;
                m.gridOptions.ByDocUser = angular.copy(gridOptionsCommon);
                m.gridOptions.ByDocUser.columnDefs = columnDefsSet.ByDocUser;
                m.gridOptions.ByTask = angular.copy(gridOptionsCommon);
                m.gridOptions.ByTask.columnDefs = columnDefsSet.ByTask;
                m.gridOptions.ByTaskUser = angular.copy(gridOptionsCommon);
                m.gridOptions.ByTaskUser.columnDefs = columnDefsSet.ByTaskUser;
                m.gridOptions.BySpecialty = angular.copy(gridOptionsCommon);
                m.gridOptions.BySpecialty.columnDefs = columnDefsSet.BySpecialty;
                m.gridOptions.ByProject = angular.copy(gridOptionsCommon);
                m.gridOptions.ByProject.columnDefs = columnDefsSet.ByProject;

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
                m.Data = [];
                timeSheetService.getByDocReport(m.Filter)
                .then(function (data) {
                    m.Data = data;
                });
            }

            function loadByDocUserReport() {
                m.Data = [];
                timeSheetService.getByDocUserReport(m.Filter)
                .then(function (data) {
                    m.Data = data;
                });
            }

            function loadByTaskReport() {
                m.Data = [];
                timeSheetService.getByTaskReport(m.Filter)
                .then(function (data) {
                    m.Data = data;
                });
            }

            function loadByTaskUserReport() {
                m.Data = [];
                timeSheetService.getByTaskUserReport(m.Filter)
                .then(function (data) {
                    m.Data = data;
                });
            }

            function loadBySpecialtyReport() {
                m.Data = [];
                timeSheetService.getBySpecialtyReport(m.Filter)
                .then(function (data) {
                    m.Data = data;
                });
            }

            function loadByProjectReport() {
                m.Data = [];
                timeSheetService.getByProjectReport(m.Filter)
                .then(function (data) {
                    m.Data = data;
                });
            }

        }
    ])
    
    ;