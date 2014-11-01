(function() {
	ko.bindingHandlers.ko_tokenInput = {
		init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
			var self = this;
			self.settings = allBindingsAccessor().settings;

			var items = valueAccessor();

			if (ko.isComputed(items))
				items = items();

			element.isUpdating = false;

			$(element).tokenInput(this.settings.url, $.extend(settings, $$.ui.settings.tokenInputDefault, {
				onAdd: function (item) {
					if (!element.isUpdating) {
						element.isUpdating = true;
						try {
							items.push(mapItem(item));
						} finally {
							element.isUpdating = false;
						}
					}
				},
				onDelete: function (item) {
					if (!element.isUpdating) {
						element.isUpdating = true;
						try {
							items.remove(mapItem(item));
						} finally {
							element.isUpdating = false;
						}
					}
				}
			}));

			function mapItem(item) {
				if (self.settings.customMapping)
					return self.settings.customMapping.to(item);
				return item;
			}

		},
		update: function (element, valueAccessor, allBindingsAccessor, viewModel) {
			var self = this;
			self.settings = allBindingsAccessor().settings;

			var params = ko.toJS(valueAccessor());
			if (!element.isUpdating) {
				element.isUpdating = true;
				try {
					$(element).tokenInput("clear");
					for (var i = 0; i < params.length; i++) {
						$(element).tokenInput("add", unmapItem(params[i]));
					}
				} finally {
					element.isUpdating = false;
				}
			}

			function unmapItem(item) {
				if (self.settings.customMapping)
					return self.settings.customMapping.from(item);
				return item;
			}
		}
	};

})();

