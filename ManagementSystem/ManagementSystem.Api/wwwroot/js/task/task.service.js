
(function () {
    var _modelService = modelService;
    var _validationService = validationService;
    var _userService = userService;
    var _selectPureService = selectPureService;

    $('.add-task').on('click', function () {
        var id = $(this).attr('data-id');
        populateModal(null, id);
    });

    $(document).on('click', '#tasklist-items a.edit-task', function () {
        var id = $(this).attr('data-id');
        populateModal(id, null);
    });

    $(document).on('click', '#tasklist-items .column-clickable', function () {
        var parent = $(this).parent().parent().parent();
        $(parent).toggleClass('task-open');
        var id = $(this).parent().attr('data-id');
        var target = $(parent).find('.table-item-content');
        var xhr = $.get("Task/LoadSelectedTask", { 'taskId': id }, function (response) {
            $(target).find('.table-item-selected-task').html(response);
        })
        .done(function () {

        })
        .fail(function () {
            alert("error");
        })
        .always(function () {
        });
    });

    function populateModal(taskId, taskParentId) {
        var data = {
            taskId: parseInt(taskId),
            taskParentId: parseInt(taskParentId)
        };
        $.get("/Task/SaveTask", data).done(function (data) {
            initializeDataInModal(data);
            populateAvailableUsers(taskId);
        });
    };

    function initializeDataInModal(data) {
        var modalContent = $(".modal .modal-content");
        $(modalContent).html(data);
        $('.modal').modal('show');
        _modelService.init(modalContent);
        _validationService.init(modalContent);
    };

    var taskWithUsers = null;
    function populateAvailableUsers(taskId) {
        $.when(_userService.getUsersByTaskId(taskId)).then(function (result) {
            var data = result.result.data;
            if (data != null) {
                var selectedUsers = [];
                $.map(data, function (e) {
                    if (e.isSelected) {
                        selectedUsers.push(e.label);
                    }
                });
                taskWithUsers = _selectPureService.initializeSelectPure("#taskSave_taskWithUsers", data, selectedUsers)
            }
        });
    };

    $('body').on('click', '#saveTaskBtn', function (e) {
        e.preventDefault();

        var modal = $(this).parents('.modal');
        var actionUrl = $(modal).find('form').attr('action');

        var properties = [
            {
                'PropertyName': 'Id',
                'Id': 'Id'
            },
            {
                'PropertyName': 'ParentId',
                'Id': 'ParentId'
            },
            {
                'PropertyName': 'Name',
                'Id': 'Name'
            },
            {
                'PropertyName': 'Description',
                'Id': 'Description'
            },
            {
                'PropertyName': 'ProjectId',
                'Id': 'ProjectId'
            },
            {
                'PropertyName': 'SprintId',
                'Id': 'SprintId'
            }];

        var optionDropdowns = [
            {
                'PropertyName': 'Projects',
                'Id': 'ProjectId'
            },
            {
                'PropertyName': 'Sprints',
                'Id': 'SprintId'
            }];

        var data = _modelService.setSimpleProperties(data, properties);
        data = _modelService.setOptionProperties(data, optionDropdowns);
        data.TaskUsers = taskWithUsers.value();

        $.ajax({
            type: "POST",
            url: actionUrl,
            data: data,
            datatype: "json",
        }).done(function (response) {
            if (response != null && response.result != null) {
                var result = response.result;
                if (result.items != null && result.items.length > 0) {
                    _validationService.addPropertiesValidationMessage(result.items);
                }
                if (result.Success) {

                }
            }
        }).fail(function (response) {
            console.log('Error: ' + response);
        });
    });


}());



