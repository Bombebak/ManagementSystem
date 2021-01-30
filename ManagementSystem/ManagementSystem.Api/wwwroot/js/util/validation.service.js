var validationService = (function () {

	var self = this;
	var _container = null;

	self.init = function (container) {
		_container = container;
	};

	self.addPropertiesValidationMessage = function (validationItems) {
		if (validationItems == null) return;
		$.map(validationItems, function (e) {
			$(_container).find('#' + e.fieldName).next('span.text-danger').text(e.message);
		});
	};

	return {
		init,
		addPropertiesValidationMessage,
	};
}());
