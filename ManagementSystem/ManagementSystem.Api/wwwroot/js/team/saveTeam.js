
(function () {
    var _teamService = teamService;

    $('.add-team').on('click', function () {
        var id = $(this).attr('data-id');
        _teamService.populateModal(null, id);
    });

    $('.edit-team').on('click', function () {
        var id = $(this).attr('data-id');
        _teamService.populateModal(id, null);
    });

    $('body').on('click', '#saveTeamBtn', function (e) {
        e.preventDefault();

        var modal = $(this).parents('.modal');
        var actionUrl = $(modal).find('form').attr('action');

        _teamService.saveTeam(actionUrl);
    });
}());



