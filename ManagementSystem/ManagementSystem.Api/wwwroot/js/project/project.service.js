
(function () {
    var _modelService = modelService;
    var _validationService = validationService;

    $('.edit-project').on('click', function () {
        var id = $(this).attr('data-id');
        $.get("/Project/SaveProject", { projectId: parseInt(id) }).done(function (data) {
            var modalContent = $(".modal .modal-content");
            $(modalContent).html(data);
            $('.modal').modal('show');
            _modelService.init(modalContent);
            _validationService.init(modalContent);
        });
    });

    $('body').on('click', '#saveProjectBtn', function (e) {
        e.preventDefault();

        var modal = $(this).parents('.modal');
        var actionUrl = $(modal).find('form').attr('action');

        var properties = [
            {
                'PropertyName': 'Id',
                'Id': 'Id'
            },
            {
                'PropertyName': 'Name',
                'Id': 'Name'
            },
            ];

        var data = _modelService.setSimpleProperties(data, properties);

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



