
(function () {
    var _messageService = messageService;    

    $(document).on('click', '.save-message', function () {
        var container = $(this).closest(".save-message-container");
        try {
            var files = getFiles(container);
            _messageService.saveMessage(container, files);
        } catch (e) {
            console.error(e);
        }
    });

    $(document).on('click', '.save-open-message', function () {
        var id = $(this).attr('data-id');
        var parentId = $(this).attr('data-parentid');
        var taskId = $(this).attr('data-taskid');
        $('.reply-message-container').empty();
        var container = $(this).parent().parent().parent().find('.reply-message-container')[0];
        var files = getFiles(container);
        try {
            _messageService.openSaveMessage(container, id, parentId, taskId, files);
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

    self.uploadableFiles = [];
    $(document).on('change', '.uploadable-files', function () {
        var files = $(this).prop('files');
        if (files == null) {
            files = [];
        }
        var saveList = $(this).parent().parent().next();

        for (var i = 0, l = files.length; i < l; i++) {
            var file = files[i];
            self.uploadableFiles.push(file);
            var currentItems = $(saveList).attr('data-items');
            currentItems += file.lastModified + ','
            $(saveList).attr('data-items', currentItems);
        }
    });    

    $(document).on('click', '.delete-file', function () {
        var id = $(this).attr('data-id');
        var isExisting = $(this).parent().parent().attr('data-isexisting');
        try {
            if (isExisting == null) {
                self.uploadableFiles = $.grep(self.uploadableFiles, function (e) {
                    if (e.lastModified != id) return e;
                });
            }
            $(this).parent().parent().remove(); 
        } catch (e) {
            console.error(e);
        }
    });

    function getFiles(container) {
        var files = [];
        var items = $(container).find('.uploadable-files-list').attr('data-items');
        if (items != null) {
            var itemsArr = items.split(',');
            $.map(itemsArr, function (e) {
                $.map(self.uploadableFiles, function (ee) {
                    if (ee.lastModified == e && e != '') {
                        files.push(ee);
                    }
                });
            });
        }
        return files;
    };
    
}());

