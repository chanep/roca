angular.module('app').factory('DialogService', ['$modal', function($modal) {

        var confirmationDialog = function(title, question, fields) {
            var modalInstance = $modal.open({
                    templateUrl: 'ScriptsApp/templates/confirmationDialog.html',
                    controller: 'ConfirmationDialogController',
                    resolve: {
                        title: function() { return title; },
                        question: function() { return question; },
                        fields: function() { return fields; }
                    }
                }
            );

            return modalInstance.result;
        }


        return {
            confirmationDialog: confirmationDialog
        };
    }]
);