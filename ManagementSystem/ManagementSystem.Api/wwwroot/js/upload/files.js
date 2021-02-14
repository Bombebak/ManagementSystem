
(function () {
    var _fileService = uploadFilesService;

    $(document).on('change', '.uploadable-files', function () {
        var files = $(this).prop('files');
        if (files != null) {
            var container = $(this).parent().parent().next();
            _fileService.populateListWithFiles(container, files);
        }
    });    

}());



