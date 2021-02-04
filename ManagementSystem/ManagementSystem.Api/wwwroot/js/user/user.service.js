var userService = (function () {

    var self = this;    


    self.getUsersByTeamId = function (teamId) {
        return $.ajax({
            type: "GET",
            url: "User/GetAvailableUsersInTeam",
            data: { teamId: teamId },
        }).done(function (response) {
            if (response != null && response.result != null) {
                var result = response.result;
                if (result != null) {
                    return result.data;
                }
                return null;
            }
        })
        .fail(function (response) {
            console.log('Error: ' + response);
        });
    };

    self.getUsersByTaskId = function (taskId) {
        return $.ajax({
            type: "GET",
            url: "User/GetAvailableUsersInTask",
            data: { taskId: taskId },
        }).done(function (response) {
            if (response != null && response.result != null) {
                var result = response.result;
                if (result != null) {
                    return result.data;
                }
                return null;
            }
        })
            .fail(function (response) {
                console.log('Error: ' + response);
            });
    };

    return {
        getUsersByTeamId,
        getUsersByTaskId
    };
}());
