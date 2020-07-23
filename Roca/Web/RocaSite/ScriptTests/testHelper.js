var TestHelper = function () {

    var specialties = [
        { Id: 1, Name: 'Instrumentacion', Abbreviation: 'I' },
        { Id: 2, Name: 'Piping', Abbreviation: 'T' }
    ];

    var users = [
        { Id: 1, UserName: "mperez", FullName: "Mongo Perez", Specialties: specialties, IsAdmin: true, IsSuperAdmin: true, Mail: "mperez@odebrecht.com", LongUserName: 'ODEBRECHT\mperez', Domain: "ODEBRECHT" },
        { Id: 2, UserName: "jlopez", FullName: "Juan Lopez", Specialties: [specialties[0]], IsAdmin: false, IsSuperAdmin: false, Mail: "jlopez@odebrecht.com", LongUserName: 'ODEBRECHT\jlopez', Domain: "ODEBRECHT" },
        { Id: 3, UserName: "lider", FullName: "Jose Lider", Specialties: [specialties[0]], IsAdmin: false, IsSuperAdmin: false, Mail: "lider@odebrecht.com", LongUserName: 'ODEBRECHT\lider', Domain: "ODEBRECHT" }
    ];

    var subprojects1 = [
        { Id: 11, Name: "Subproyecto 1_1", Code: "Sp11", ShortName: "Subp11", ParentIs: 1, Subprojects: [], ParentName: "Proyecto 1", ParentCode: "P1", ParentShortName: "Pro1" },
        { Id: 12, Name: "Subproyecto 1_2", Code: "Sp12", ShortName: "Subp12", ParentIs: 1, Subprojects: [], ParentName: "Proyecto 1", ParentCode: "P1", ParentShortName: "Pro1" }
    ];

    var subprojects2 = [
        { Id: 21, Name: "Subproyecto 2_1", Code: "Sp21", ShortName: "Subp21", ParentIs: 2, Subprojects: [], ParentName: "Proyecto 2", ParentCode: "P2", ParentShortName: "Pro2" },
        { Id: 22, Name: "Subproyecto 2_2", Code: "Sp22", ShortName: "Subp22", ParentIs: 2, Subprojects: [], ParentName: "Proyecto 2", ParentCode: "P2", ParentShortName: "Pro2" }
    ];


    var projects = [
        { Id: 1, Name: "Proyecto 1", Code: "P1", ShortName: "Pro1", ParentIs: null, Subprojects: subprojects1, ParentName: null, ParentCode: null, ParentShortName: null },
        { Id: 2, Name: "Proyecto 2", Code: "P2", ShortName: "Pro2", ParentIs: null, Subprojects: subprojects2, ParentName: null, ParentCode: null, ParentShortName: null }
    ];


    var document = { Id: 5, ProjectId: 11, SpecialtyId: 2, TypeId: 1, DocNumber: 'IGIO-12345678', Tilte: 'Diagrama de Cucamonas' };
    
    var items = [
        {Id: 10, TimeSheetId: 1, SubprojectId: 21, TaskId: null, DocumentId: document.Id, Document: document, Hours: 8},
        {Id: 11, TimeSheetId: 1, SubprojectId: 22, TaskId: 1, DocumentId: null, Document: null, Hours: 16}
    ];

    var today = new Date();
    today = new Date(today.getFullYear(), today.getMonth(), today.getDate());
    var nextWeek = new Date(today.getTime() + 1000 * 60 * 60 * 24 * 7);

    var timeSheetDtos = [
        { Id: 1, ControlDateDate: today, ControlDate: '/Date(' + today.getTime() + ')/', LeaderId: 3, UserId: 1, Status: 1, Items: items },
        { Id: 1, ControlDateDate: nextWeek, ControlDate: '/Date(' + nextWeek.getTime() + ')/', LeaderId: 3, UserId: 1, Status: 1, Items: [] }
    ];

    var docTypes = [
        { Id: 1, Type: 'tipoDoc', Code: 'RM', Value: 'RM' },
        { Id: 2, Type: 'tipoDoc', Code: 'LM', Value: 'LM' }
    ];

    var tasks = [
        { Id: 11, Type: 'caTarea', Code: 't1', Value: 'Tarea Cucamona' },
        { Id: 12, Type: 'caTarea', Code: 't2', Value: 'Tareas del cole' }
    ];

    var tsDetailsOptions = {
        Projects: projects,
        DocTypes: docTypes,
        Tasks: tasks,
        Leaders: [users[2]],
        Specialties: specialties
    };

    var toQueryString = function (obj) {
        var param = '?';
        for (var p in obj) {
            if (obj.hasOwnProperty(p) && obj[p] != null) {
                param += encodeURIComponent(p) + "=" + encodeURIComponent(obj[p]) + "&";
            }
        }
        return param.substr(0, param.length - 1);
    };


    return {
        getSpecialties: function() { return angular.copy(specialties); },
        getUsers: function() { return angular.copy(users); },
        getProjects: function() { return angular.copy(projects); },
        getTimeSheetDtos: function () {
             return angular.copy(timeSheetDtos);
        },
        getTsDetailsOptions: function () { return angular.copy(tsDetailsOptions); },
        toQueryString: toQueryString
    };
};
