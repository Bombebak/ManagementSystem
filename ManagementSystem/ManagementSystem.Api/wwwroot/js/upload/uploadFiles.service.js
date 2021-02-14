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

    function createFileItem(file) {
        var reader = new FileReader();

        var parentDiv = document.createElement('div');
        parentDiv.className = "d-flex align-items-center mb-2";

        var previewDiv = document.createElement('div');
        previewDiv.style.width = "15%";
        console.log(file);
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
        populateListWithFiles
	};
}());
