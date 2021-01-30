var modelService = (function () {

	var self = this;
	var _container = null;

	self.init = function (container) {
		_container = container;
	};	

	self.setSimpleProperties = function (obj, properties) {
		if (obj == null) obj = {};
		if (properties == null) return obj;
		$.map(properties, function (e) {
			var target = $(_container).find('#' + e.Id);
			var value = $(target).val();
			obj = self.setProperty(obj, e.PropertyName, value);
		});
		return obj;
	};

	self.setOptionProperties = function (obj, properties) {
		if (obj == null) obj = {};
		if (properties == null) return obj;
		$.map(properties, function (e) {
			var target = $(_container).find('#' + e.Id);
			var value = [];
			$.map(target.find('option'), function (e) {
				value.push({
					'Value': e.value,
					'Label': e.text
				});
			});
			obj = self.setProperty(obj, e.PropertyName, value);
		});
		return obj;
	};

	self.setProperty = function (obj, property, value) {
		if (obj[property] == null) {
			obj[property] = value;
		}
		return obj;
	};

	self.setDatepickerProperties = function (properties) {
		if (properties == null) return;
		$.map(properties, function (e) {
			var target = $(_container).find('#' + e);
			$(target).datepicker();
		});
	};

	return {
		init,
		setSimpleProperties,
		setOptionProperties,
		setDatepickerProperties
	};
}());
