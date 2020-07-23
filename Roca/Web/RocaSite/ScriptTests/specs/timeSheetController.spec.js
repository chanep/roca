///<reference path="../references.js"/>


describe("TimeSheet", function () {
    var timeSheetService, 
        $httpBackend,
        $rootScope,
        utils,
        $controller,
        controller,
        scope,
        detailsOptions, dto, date, user, specialty, ts;
    var helper = TestHelper();

    //para poder debuggear con resharper haciendo reload de la pagina
    //jasmine.getEnv().currentRunner_.finishCallback = function () { };

    beforeEach(function () {

        angular.mock.module('app');

        angular.mock.inject(function (_$rootScope_, _$controller_, _$httpBackend_, _Utils_, _TimeSheetService_) {
            $rootScope = _$rootScope_;
            $controller = _$controller_;      
            $httpBackend = _$httpBackend_;
            utils = _Utils_;
            timeSheetService = _TimeSheetService_;
        });

        dto = helper.getTimeSheetDtos()[1];
        date = dto.ControlDateDate;
        user = helper.getUsers()[0];
        specialty = user.Specialties[0];
        var getByDateParams = { controlDate: date.getTime(), specialtyId: specialty.Id };
        detailsOptions = helper.getTsDetailsOptions();

        $httpBackend.expect("GET", "TimeSheet/GetDetailsOptions" + helper.toQueryString({ timeSheetId: null })).respond(detailsOptions);
        $httpBackend.expect("GET", "TimeSheet/GetByDate" + helper.toQueryString(getByDateParams)).respond(dto);
        $httpBackend.when("GET", "TimeSheet/GetLast" + helper.toQueryString({ specialtyId: specialty.Id })).respond(null);

        scope = $rootScope.$new();
        scope.RootModel = {};

        utils.getNowTime = function() { return date; };
        controller = $controller('TimeSheet.Details.Controller', {
            $scope: scope,
            $stateParams: { id: 0 },
            utils: utils,
            timeSheetService: timeSheetService
        });

        $httpBackend.flush();
        $httpBackend.verifyNoOutstandingExpectation();

        ts = scope.Model.TimeSheet;
    });

    it("should load timesheet", function () {
        expect(scope.Model.Options).toEqual(detailsOptions);
        expect(scope.Model.TimeSheet.UserId).toEqual(dto.UserId);
        expect(scope.Model.TimeSheet.ControlDate).toEqual(dto.ControlDate);
    });

    it("should add doc item", function () {
        var prevCount = ts.Items.length;
        scope.addEmptyDoc();
        expect(ts.Items.length).toEqual(prevCount + 1);
        var doc = ts.Items[ts.Items.length - 1];
        expect(doc.DocumentId).toEqual(0);
        expect(doc.TaskId).toBeNull();
    });

    it("should add task item", function () {
        var prevCount = ts.Items.length;
        scope.addEmptyTask();
        expect(ts.Items.length).toEqual(prevCount + 1);
        var doc = ts.Items[ts.Items.length - 1];
        expect(doc.TaskId).toEqual(0);
        expect(doc.DocumentId).toBeNull();
    });


    

});