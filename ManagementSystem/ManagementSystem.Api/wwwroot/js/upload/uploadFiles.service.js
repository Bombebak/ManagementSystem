var uploadFilesService = (function () {

	var self = this;
	var _container = null;

    self.populateListWithFiles = function (container, files) {
        if (files == null) files = [];
		$.map(files, function (ele) {
			try {
				container.append(createFileItem(ele));
			} catch (e) {
				console.error(e);
			}
		});
    };

    self.fileInputHasChanged = function (newFiles, existingFiles, container) {
        if (newFiles == null) {
            newFiles = [];
        }

        for (var i = 0, l = newFiles.length; i < l; i++) {
            var file = newFiles[i];
            existingFiles.push(file);
            var currentItems = $(container).attr('data-items');
            currentItems += file.lastModified + ','
            $(container).attr('data-items', currentItems);
        }

        return existingFiles;
    };

    self.deleteFile = function (id, files, container) {
        var isExisting = $(container).attr('data-isexisting');
        if (isExisting == null) {
            files = $.grep(files, function (e) {
                if (e.lastModified != id) return e;
            });
        }
        $(container).remove(); 
        return files;
    };

    self.mergeExistingAndNewFiles = function (oldFiles, container) {
        if (oldFiles == null) oldFiles = [];
        var files = [];
        var items = $(container).find('.uploadable-files-list').attr('data-items');
        if (items != null) {
            var itemsArr = items.split(',');
            $.map(itemsArr, function (e) {
                $.map(oldFiles, function (ee) {
                    if (ee.lastModified == e && e != '') {
                        files.push(ee);
                    }
                });
            });
        }
        return files;
    };

    self.addExistingAndNewFilesToFormData = function (formData, files, container) {
        if (files != null) {
            for (var i = 0; i != files.length; i++) {
                formData.append("Files", files[i]);
            }
        }

        var existingFiles = $(container).find('.uploadable-files-list div[data-isexisting="true"]');
        if (existingFiles != null) {
            for (var i = 0; i != existingFiles.length; i++) {
                var itemId = $(existingFiles[i]).attr('data-id');
                formData.append("ExistingFiles[" + i + "].Id", itemId);
            }
        }

        return formData;
    };

    function createFileItem(file) {
        var reader = new FileReader();

        var parentDiv = document.createElement('div');
        parentDiv.className = "d-flex align-items-center mb-2";

        var previewDiv = document.createElement('div');
        previewDiv.style.width = "15%";
        if (file.type.indexOf('image') > 0) {
            var img = document.createElement('img');
            img.width = 50;
            img.height = 50;
            reader.onload = function (e) {
                img.src = e.target.result;
            };
            reader.readAsDataURL(file);
            previewDiv.appendChild(img);
        }
        else {
            var fileIcon = document.createElement('i');
            fileIcon.style.fontSize = '3em';
            fileIcon.className = "fa";
            var iconClass = ' fa-file';
            if (file.type.indexOf('excel') > 0 || file.type.indexOf('spreadsheet') > 0) {
                iconClass = " fa-file-excel-o";
            }
            else if (file.type.indexOf('word') > 0) {
                iconClass = " fa-file-word-o";
            }
            else if (file.type.indexOf('presentation') > 0 || file.type.indexOf('powerpoint') > 0) {
                iconClass = " fa-file-powerpoint-o";
            }
            else if (file.type.indexOf('pdf') > 0) {
                iconClass = " fa-file-pdf-o";
            }            
            fileIcon.className += iconClass;
            previewDiv.appendChild(fileIcon);
        }
        
        parentDiv.appendChild(previewDiv);

        var nameDiv = document.createElement('div');
        nameDiv.style.width = "400px";
        nameDiv.style.whiteSpace = "nowrap";
        nameDiv.style.overflow = "hidden";
        nameDiv.style.textOverflow = "ellipsis";
        var nameSpan = document.createElement('span');
        nameSpan.textContent = file.name;
        nameDiv.appendChild(nameSpan);
        parentDiv.appendChild(nameDiv);

        var buttonDiv = document.createElement('div');
        buttonDiv.style.width = "10%";
        buttonDiv.className = "d-flex justify-content-end";
        var anchorTag = document.createElement('a');
        anchorTag.dataset.id = file.lastModified;
        anchorTag.className = "btn btn-danger delete-file";
        var anchorTagIcon = document.createElement('i');
        anchorTagIcon.className = "fa fa-remove";
        anchorTag.appendChild(anchorTagIcon);
        buttonDiv.appendChild(anchorTag);
        parentDiv.appendChild(buttonDiv);

        return parentDiv;
    };

	return {
        populateListWithFiles,
        fileInputHasChanged,
        deleteFile,
        mergeExistingAndNewFiles,
        addExistingAndNewFilesToFormData
	};
}());
