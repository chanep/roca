angular.module('app').factory('FileService',
     function () {
         var downloadFile = function (responseData) {
            var data = responseData;
            var file = new Blob([data.data], {
                type: data.headers('Content-Type')
            });
            //trick to download store a file having its URL
            var fileUrl = URL.createObjectURL(file);
            var a = document.createElement('a');
            a.href = fileUrl;
            a.target = '_blank';
            var fileName = data.headers('Content-Disposition').match(/filename[^;=\n]*=((['"]).*?\2|[^;\n]*)/i)[1];
            a.download = fileName;
            document.body.appendChild(a);
            a.click();
        };

        return {
            downloadFile: downloadFile
        };
    }
);