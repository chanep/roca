///<reference path="../references.js"/>


describe("TimeSheet", function () {
    var timeSheetFactory;
    var timeSheetDto;
    var timeSheet;

    //para poder debuggear con resharper haciendo reload de la pagina
    //jasmine.getEnv().currentRunner_.finishCallback = function () { };

    beforeEach(function () {

        angular.mock.module('app');

        angular.mock.inject(function(_TimeSheetFactory_) {
            timeSheetFactory = _TimeSheetFactory_;
        });

        timeSheetDto = createDto();

        timeSheet = timeSheetFactory.createTimeSheet(timeSheetDto);

    });

    it("should get only doc items", function () {

        var docItems = timeSheet.getDocItems();
        expect(docItems.length).toEqual(1);
        expect(docItems[0].TaskId).toBeNull();
        expect(docItems[0].DocumentId).not.toBeNull();
        expect(docItems[0].Document).toBeTruthy();
    });


    it("should get only task items", function () {

        var taskItems = timeSheet.getTaskItems();
        expect(taskItems.length).toEqual(1);
        expect(taskItems[0].TaskId).not.toBeNull();
        expect(taskItems[0].DocumentId).toBeNull();
        expect(taskItems[0].Document).toBeFalsy();
    });

    it("should get total item hours", function() {

        var totalHours = timeSheet.getTotalHours();
        var actualTotalHours = 0;
        for (var i in timeSheet.Items) {
            actualTotalHours += timeSheet.Items[i].Hours;
        }
        expect(totalHours).toEqual(actualTotalHours);

    });

    it("should get total task item hours", function () {

        var items = timeSheet.getTaskItems();
        var hours = timeSheet.getTotalTaskHours();
        var actualHours = 0;
        for (var i in items) {
            actualHours += items[i].Hours;
        }
        expect(hours).toEqual(actualHours);

    });

    it("should get total doc item hours", function () {

        var items = timeSheet.getDocItems();
        var hours = timeSheet.getTotalDocHours();
        var actualHours = 0;
        for (var i in items) {
            actualHours += items[i].Hours;
        }
        expect(hours).toEqual(actualHours);

    });

    it("should add an empty Doc Item", function () {
        var prevDocs = angular.copy(timeSheet.getDocItems());
        timeSheet.addEmptyDoc();
        var docs = timeSheet.getDocItems();
        var emptyDoc = docs[docs.length - 1];
        expect(docs.length).toEqual(prevDocs.length + 1);
        expect(emptyDoc.DocumentId).toEqual(0);
    });

    it("should add an empty Task Item", function () {
        var prevTasks = angular.copy(timeSheet.getTaskItems());
        timeSheet.addEmptyTask();
        var tasks = timeSheet.getTaskItems();
        var emptyTask = tasks[tasks.length - 1];
        expect(tasks.length).toEqual(prevTasks.length + 1);
        expect(emptyTask.TaskId).toEqual(0);
    });

    it("should delete specific Item", function () {
        var first = timeSheet.Items[0];
        var second = timeSheet.Items[1];
        var prevItems = angular.copy(timeSheet.Items);
        timeSheet.deleteItem(first);
        expect(timeSheet.Items.length).toEqual(prevItems.length - 1);
        expect(timeSheet.Items[0].Id).toEqual(second.Id);
    });

    it("should detect invalid ControlDate", function () {
        expect(timeSheet.isControlDateValid()).toEqual(true);
        var date = (new Date(2019,1,1)).getTime();
        var dateAsp = '/Date(' + date + ')/';
        var dto = createDto();
        dto.ControlDate = dateAsp;
        var timeSheet2 = timeSheetFactory.createTimeSheet(dto);
        expect(timeSheet2.isControlDateValid()).toEqual(false);

    });

    it("should detect invalid items", function () {
        var item = timeSheet.Items[0];
        expect(timeSheet.isItemValid(item)).toEqual(true);
        var invalid = angular.copy(item);
        invalid.SubprojectId = 0;
        expect(timeSheet.isItemValid(invalid)).toEqual(false);
        invalid = angular.copy(item);
        invalid.TaskId = 0;
        invalid.DocumentId = 0;
        expect(timeSheet.isItemValid(invalid)).toEqual(false);
    });

    it("should detect invalid item project", function () {
        var item = timeSheet.Items[0];
        expect(timeSheet.isProjectValid(item)).toEqual(true);
        var invalid = angular.copy(item);
        invalid.SubprojectId = 0;
        expect(timeSheet.isProjectValid(invalid)).toEqual(false);
    });

    it("should detect invalid hours", function () {
        var item = timeSheet.Items[0];
        expect(timeSheet.isItemAndHourValid(item)).toEqual(true);
        var invalid = angular.copy(item);
        invalid.Hours = 0;
        expect(timeSheet.isItemAndHourValid(invalid)).toEqual(false);
        invalid.Hours = 41;
        expect(timeSheet.isItemAndHourValid(invalid)).toEqual(false);

    });

    it("should detect invalid timesheet", function () {
        expect(timeSheet.isTimeSheetValid()).toEqual(true);
        var invalid = timeSheet;
        invalid.LeaderId = 0;
        expect(invalid.isTimeSheetValid()).toEqual(false);
        invalid.LeaderId = 3;
        invalid.Items[0].Hours = 41;
        expect(invalid.isTimeSheetValid()).toEqual(false);

        var x = { prop1: 'value1', prop2: true, prop3: 23 };

    });


    function createDto() {
        var dto = {};
        dto.Id = 1;
        var date = (new Date()).getTime();
        dto.ControlDate = '/Date(' + date + ')/';
        dto.LeaderId = 3;
        dto.UserId = 2;
        dto.Status = 1; //open
        var item = {};
        item.Id = 10;
        item.TimeSheetId = 1;
        item.SubprojectId = 4;
        item.TaskId = null;
        item.DocumentId = 5;
        item.Document = { Id: 5, ProjectId: 2, SpecialtyId: 3 };
        item.Hours = 8;
        dto.Items = [];
        dto.Items.push(item);
        item = {};
        item.Id = 11;
        item.TimeSheetId = 1;
        item.SubprojectId = 6;
        item.TaskId = 7;
        item.DocumentId = null;
        item.Hours = 16;
        dto.Items.push(item);
        return dto;
    }

});