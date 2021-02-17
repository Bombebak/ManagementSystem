var messageService = (function () {

    var self = this;
    var _modelService = modelService;
    var _validationService = validationService;
    var _fileService = uploadFilesService;

    self.openSaveMessage = function (container, messageId, taskId) {
        $.get('/Message/SaveMessage', { id: messageId, taskId: taskId}, function (response) {
            $(container).html(response);
        });
    };    

    self.saveMessage = function (container, files) {
        _modelService.init(container);
        var properties = [
            {
                'PropertyName': 'Id',
                'Id': 'Id'
            },
            {
                'PropertyName': 'TaskId',
                'Id': 'TaskId'
            },            
            {
                'PropertyName': 'Message',
                'Id': 'Message'
            }];

        var formData = new FormData();

        formData = _fileService.addExistingAndNewFilesToFormData(formData, files, container);

        var data = _modelService.setSimpleProperties(data, properties);     

        formData.append("Id", data.Id);
        formData.append("TaskId", data.TaskId);
        formData.append("Message", data.Message);

        $.ajax({
            type: "POST",
            url: "/Message/SaveMessage",
            data: formData,
            processData: false,
            contentType: false,
        }).done(function (result) {
        })
        .fail(function (response) {
            console.log('Error: ' + response);
        });
    };

    self.handleSaveMessageResponse = function (response) {
        if (response != null && response.result != null) {
            var result = response.result;
            if (result.items != null && result.items.length > 0) {
                _validationService.addPropertiesValidationMessage(result.items);
            }
            if (result.Success) {

            }
        }
    };

    self.openConfirmDeleteMessageModal = function (messageId) {
        $.get('/Message/DeleteMessage', { id: messageId }, function (result) {
            $(".modal").addClass("show").find(".modal-content").html(result);
        });
    };

    self.deleteMessage = function (container) {

        var actionUrl = $(container).find('form').attr('action');
        _modelService.init(container.find('.modal-content'));
        var properties = [
            {
                'PropertyName': 'Id',
                'Id': 'Id'
            }];

        var data = _modelService.setSimpleProperties(data, properties);   

        $.ajax({
            type: "POST",
            url: actionUrl,
            data: data,
            datatype: "json",
        }).done(self.handleDeleteMessageResponse)
            .fail(function (response) {
                console.log('Error: ' + response);
            });
    };

    self.handleDeleteMessageResponse = function (result) {

    };
    

    return {
        openSaveMessage,
        openConfirmDeleteMessageModal,
        saveMessage,
        deleteMessage
    };
}());
