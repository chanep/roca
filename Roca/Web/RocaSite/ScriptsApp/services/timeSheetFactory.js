angular.module('app').factory('TimeSheetFactory', ['Utils', function(utils) {
        var createTimeSheet = function(timeSheetDto) {
            var MAX_HOURS = 40;
            var CLOSED = 2;
            var self = {};
            angular.extend(self, timeSheetDto);

            (function init() {
                var dateStr = utils.aspDateToStr(self.ControlDate, 'yyyy-MM-dd');
                var date = new Date(dateStr);
                self.ControlDateDate = new Date(date.getTime() + 60000 * date.getTimezoneOffset()); //si no se suma el offfset, da el dia anterior por el timezone
                self.Items = utils.orderBy(self.Items, 'Id');
            })();

            self.getDto = getDto;
            self.getDocItems = getDocItems;
            self.getTaskItems = getTaskItems;
            self.getTotalTaskHours = getTotalTaskHours;
            self.getTotalDocHours = getTotalDocHours;
            self.getTotalHours = getTotalHours;
            self.addEmptyDoc = addEmptyDoc;
            self.addEmptyTask = addEmptyTask;
            self.deleteItem = deleteItem;
            self.isControlDateValid = isControlDateValid;
            self.isProjectValid = isProjectValid;
            self.isItemAndHourValid = isItemAndHourValid;
            self.isItemValid = isItemValid;
            self.isHourValid = isHourValid;
            self.isTimeSheetValid = isTimeSheetValid;
            self.canModify = canModify;



            return self;

            function getDto() {
                var dto = {
                    ControlDate: self.ControlDateDate,
                    Id: self.Id,
                    LeaderId: self.LeaderId,
                    SpecialtyId: self.SpecialtyId,
                    UserId: self.UserId,
                    Status: self.Status,
                    Items: []
                };

                for (var i in self.Items) {
                    var item = self.Items[i];
                    var auxItem = {
                        Id: item.Id,
                        TimeSheetId: item.TimeSheetId,
                        SubprojectId: item.SubprojectId,
                        DocumentId: item.DocumentId,
                        TaskId: item.TaskId,
                        Hours: item.Hours
                    };
                    dto.Items.push(auxItem);
                }
                return dto;
            }


            function getDocItems() {
                var docs = [];
                angular.forEach(self.Items, function(item) {
                    if (item.DocumentId !== null) docs.push(item);
                }, docs);
                return docs;
            }

            function getTaskItems() {
                var tasks = [];
                angular.forEach(self.Items, function(item) {
                    if (item.TaskId !== null) tasks.push(item);
                }, tasks);
                return tasks;
            }

            function getTotalTaskHours() {
                var items = getTaskItems();
                var hh = 0;
                for (var index in items) {
                    if (isItemValid(items[index])) hh += items[index].Hours;
                }
                return hh;
            }

            function getTotalDocHours() {
                var items = getDocItems();
                var hh = 0;
                for (var index in items) {
                    if (isItemValid(items[index])) hh += items[index].Hours;
                }
                return hh;
            }

            function getTotalHours() { return getTotalDocHours() + getTotalTaskHours(); }

            function createEmptyItem() {
                var item = {
                    Id: 0,
                    TimeSheetId: self.Id,
                    SubprojectId: 0,
                    SubprojectParentId: 0,
                    TaskId: null,
                    DocumentId: null,
                    Document: null,
                    Hours: 0
                }
                copyPreviousProject(item);
                return item;
            }

            function addEmptyDoc() {
                var item = createEmptyItem();
                item.Document = { Id: 0, TypeId: 0, DocNumber: '', Title: '' };
                item.DocumentId = 0;
                self.Items.push(item);
            }

            function addEmptyTask() {
                var item = createEmptyItem();
                item.TaskId = 0;
                self.Items.push(item);
            }

            function deleteItem(item) {
                for (var i = 0; i < self.Items.length; i++)
                    if (self.Items[i] === item) {
                        self.Items.splice(i, 1);
                        break;
                    }
            }

            function copyPreviousProject(item) {
                if (self.Items.length > 0) {
                    for (var i = self.Items.length - 1; i >= 0; i--) {
                        var lastItem = self.Items[i];
                        if (lastItem.SubprojectParentId != 0) {
                            item.SubprojectId = lastItem.SubprojectId;
                            item.Subprojects = lastItem.Subprojects;
                            item.SubprojectParentId = lastItem.SubprojectParentId;
                            return;
                        }
                    }
                }
            }

            function isControlDateValid() {
                var date = self.ControlDateDate.getTime();
                if (isNaN(date)) return false;
                var maxDate = new Date();
                maxDate.setTime(maxDate.getTime() + 1000 * 60 * 60 * 24 * 28);
                if (date > maxDate) return false;
                var minDate = new Date(2014, 1, 1);
                if (date < minDate) return false;
                return true;
            }

            function isProjectValid(item) {
                if (utils.isNullOrUndefined(item.SubprojectId) || item.SubprojectId == 0) return false;
                return true;
            }

            function isItemAndHourValid(item) {
                if (!isItemValid(item)) return false;
                if (!isHourValid(item.Hours)) return false;
                if (item.Hours < 1) return false;
                return true;
            }

            function isItemValid(item) {
                if (utils.isNullOrUndefined(item.SubprojectId) || item.SubprojectId == 0) return false;
                if (item.TaskId == 0) return false;
                if (item.DocumentId == 0) return false;
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
                if (!isControlDateValid()) return false;
                if (utils.isNullOrUndefined(self.LeaderId) || self.LeaderId == 0) return false;
                var totalHours = getTotalHours();
                if (totalHours > MAX_HOURS || totalHours == 0) return false;
                return true;
            }

            function canModify() {
                if (self.isEmptyTimeSheet) return false;
                if (self.Status == CLOSED) return false;
//                var date = self.ControlDateDate.getTime();
//                var minDate = new Date();
//                minDate.setTime(minDate.getTime() - (1000 * 60 * 60 * 24 * 29));
//                if (date < minDate) return false;
                return true;
            }


        };


        var emptyTimeSheetDto =  {
            Id : 0,
            ControlDate : '/Date(' + (new Date()).getTime() + ')/',
            LeaderId : 0,
            UserId : 0,
            Status : 1, //open
            Items : [],
            isEmptyTimeSheet: true
        };

        var createEmptyTimeSheet = function() {
            return createTimeSheet(emptyTimeSheetDto);
        }

        return {
            createTimeSheet: createTimeSheet,
            createEmptyTimeSheet: createEmptyTimeSheet
        };
    }
]);