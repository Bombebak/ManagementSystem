
(function () {
    var _messageService = messageService;    
    var _fileService = uploadFilesService;

    $(document).on('click', '.save-message', function () {
        var container = $(this).closest(".save-message-container");
        try {
            var files = _fileService.mergeExistingAndNewFiles(uploadableFiles, container);
            _messageService.saveMessage(container, files);
        } catch (e) {
            console.error(e);
        }
    });

    $(document).on('click', '.save-open-message', function () {
        var id = $(this).attr('data-id');
        var taskId = $(this).attr('data-taskid');
        $('.reply-message-container').empty();
        var container = $(this).parent().parent().parent().find('.reply-message-container')[0];
        var files = _fileService.mergeExistingAndNewFiles(uploadableFiles, container);
        try {
            _messageService.openSaveMessage(container, id, taskId, files);
        } catch (e) {
            console.error(e);
        }
    });

    $(document).on('click', '.delete-open-message-modal', function () {
        var id = $(this).attr('data-id');
        try {
            _messageService.openConfirmDeleteMessageModal(id);
        } catch (e) {
            console.error(e);
        }
    });

    $(document).on('click', '#deleteMessageBtn', function (e) {
        e.preventDefault();
        var container = $(this).parents('.modal');
        try {
            _messageService.deleteMessage(container);
        } catch (e) {
            console.error(e);
        }
    });

    var uploadableFiles = [];
    $(document).on('change', '.save-message-container .uploadable-files', function () {
        var files = $(this).prop('files');
        var saveList = $(this).parent().parent().next();
        try {
            uploadableFiles = _fileService.fileInputHasChanged(files, uploadableFiles, saveList);
        } catch (e) {
            console.error(e);
        }
    });    

    $(document).on('click', '.save-message-container .delete-file', function () {
        var id = $(this).attr('data-id');
        try {
            uploadableFiles = _fileService.deleteFile(id, uploadableFiles, $(this).parent().parent()); 
        } catch (e) {
            console.error(e);
        }
    });

    
}());

