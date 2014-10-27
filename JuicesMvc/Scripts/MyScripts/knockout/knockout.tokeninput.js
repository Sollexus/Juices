ko.bindingHandlers.ko_tokenInput = {
	init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
		this.settings = allBindingsAccessor().settings;
		var self = this;
		this.items = valueAccessor();
		element.isUpdating = false;

		$(element).tokenInput(this.settings.url, $.extend(settings, $$.ui.settings.tokenInputDefault, {
			onAdd: function (item) {
				if (!element.isUpdating) {
					element.isUpdating = true;
					try {
						self.items.push(item);
					} finally {
						element.isUpdating = false;
					}
				}
			},
			onDelete: function (item) {
				if (!element.isUpdating) {
					element.isUpdating = true;
					try {
						self.items.remove(item);
					} finally {
						element.isUpdating = false;
					}
				}
			}
		}));

		this.items.subscribe(function (data) {
		});
	},
	update: function (element, valueAccessor, allBindingsAccessor, viewModel) {
		var params = ko.toJS(valueAccessor());
		if (!element.isUpdating) {
			element.isUpdating = true;
			try {
				$(element).tokenInput("clear");
				for (var i = 0; i < params.length; i++) {
					$(element).tokenInput("add", params[i]);
				}
			} finally {
				element.isUpdating = false;
			}
		}
	}
};