var taskSearchService = (function () {

    var self = this;
    var _modelService = modelService;
    var _userService = userService;
    var _selectPureService = selectPureService;

    self.init = function () {
        _modelService.init($('#taskSearchCriterias'));
        self.populateAvailableUsers();
    };

    self.searchTasksRequest = null;
    self.searchTasks = function () {       
        $('#tasklist-spinner').toggleClass('hide');
        $('#tasklist-items').html('');        

        var properties = [
            {
                'PropertyName': 'ProjectId',
                'Id': 'ProjectId'
            },
            {
                'PropertyName': 'SprintId',
                'Id': 'SprintId'
            },
            {
                'PropertyName': 'SearchText',
                'Id': 'SearchText'
            }];

        var data = _modelService.setSimpleProperties(data, properties);
        data.UserEmails = self.taskWithUsers.value();

        self.searchTasksRequest = $.post("Task/SearchTasks", { 'criterias': data }, function (response) {
            self.handleSearchTasksResponse(response);
        })
        .done(function () {
            
        })
        .fail(function () {
            alert("error");
        })
        .always(function () {
        });

        //if (self.searchTasksRequest != null) {
        //    self.searchTasksRequest.abort();
        //}
    };

    self.handleSearchTasksResponse = function (response) {
        if (response != null) {
            $('#tasklist').html(response);
        }
    };

    self.taskWithUsers = null;
    self.populateAvailableUsers = function () {
        $.when(_userService.getUsersByTaskId(null)).then(function (result) {
            var data = result.result.data;
            if (data != null) {
                var selectedUsers = [];
                $.map(data, function (e) {
                    if (e.isSelected) {
                        selectedUsers.push(e.label);
                    }
                });
                self.taskWithUsers = _selectPureService.initializeSelectPure("#taskSearch_taskWithUsers", data, selectedUsers, self.taskUsersChanged)
            }
        });        
    };

    self.taskUsersChanged = function () {
        if (self.taskWithUsers == null) return;
        try {
            self.searchTasks();
        } catch (e) {
            console.error(e);
        }
    };

    return {
        init,
        searchTasks
    };
}());
