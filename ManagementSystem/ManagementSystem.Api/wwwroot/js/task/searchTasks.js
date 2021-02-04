
(function () {
    var _taskSearchService = taskSearchService;

    _taskSearchService.init();

    $('.js-tasks-search-criteria').on('change', function () {
        try {
            _taskSearchService.searchTasks();
        } catch (e) {
            console.error(e);
        }
    });    

    var timer = 0;
    $('#SearchText').on('change paste keyup', function () {
        if (this.value.length > 0 && this.value.length < 3) {
            return;
        }
        clearTimeout(timer);
        timer = setTimeout(function () {
            try {
                _taskSearchService.searchTasks();
            } catch (e) {
                console.error(e);
            }
        }, 1000);
        
    }); 
    
}());



