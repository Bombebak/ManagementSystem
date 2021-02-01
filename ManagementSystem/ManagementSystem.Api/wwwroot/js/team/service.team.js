var teamService = (function () {

    var self = this;
    var _modelService = modelService;
    var _validationService = validationService;

    self.populateModal = function (teamId, teamParentId) {
        var data = {
            teamId: parseInt(teamId),
            teamParentId: parseInt(teamParentId)
        };
        $.get("/Team/SaveTeam", data).done(function (data) {
            var modalContent = $(".modal .modal-content");
            $(modalContent).html(data);
            self.populateAvailableUsers(teamId);
            $('.modal').modal('show');
            _modelService.init(modalContent);
            _validationService.init(modalContent);

        });
    };

    self.saveTeam = function (url) {
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
            }];

        var data = _modelService.setSimpleProperties(data, properties);
        data.UserEmailsInTeam = self.availableUsersInTeam.value();

        $.ajax({
            type: "POST",
            url: url,
            data: data,
            datatype: "json",
        }).done(self.handleSaveTeamResponse)
            .fail(function (response) {
                console.log('Error: ' + response);
            });
    };

    self.handleSaveTeamResponse = function (response) {
        if (response != null && response.result != null) {
            var result = response.result;
            if (result.items != null && result.items.length > 0) {
                _validationService.addPropertiesValidationMessage(result.items);
            }
            if (result.Success) {

            }
        }
    };
    self.availableUsersInTeam = null;
    self.populateAvailableUsers = function (teamId) {
        return $.ajax({
            type: "GET",
            url: "Team/GetAvailableTeamUsers",
            data: { teamId: teamId },
        }).done(function (response) {
            if (response != null && response.result != null) {
                var result = response.result;
                if (result.data != null && result.data.length > 0) {
                    var selectedUsers = [];
                    $.map(result.data, function (e) {
                        if (e.isSelected) {
                            selectedUsers.push(e.label);
                        }
                    });

                    self.availableUsersInTeam = new SelectPure("#availableUsersInTeam", {
                        options: result.data,
                        value: selectedUsers,
                        multiple: true, // default: false
                        autocomplete: true, // default: false
                        icon: "glyphicon glyphicon-remove"
                    });
                }
            }
        })
            .fail(function (response) {
                console.log('Error: ' + response);
            });
    };

    return {
        populateModal,
        saveTeam,
        populateAvailableUsers
    };
}());
