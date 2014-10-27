//
(function ($$, $, undefined) {
	$$.array = {};
	$$.ui = {};
	$$.ui.settings = {};

	$$.ui.confirm = function (message, success, title) {
		bootbox.dialog({
			message: message,
			title: title || "Сочные продукты",
			buttons: {
				success: {
					label: "Да",
					className: "btn-success",
					callback: function () {
						success();
					}
				},
				danger: {
					label: "Нет",
					className: "btn-defaut",
				}
			}
		});
	};

	$$.ui.settings.tokenInputDefault = {
		minChars: 3,
		queryParam: 'q',
		propertyToSearch: 'Name',
		hintText: 'Введите название',
		noResultsText: 'Нет результатов',
		searchingText: 'Поиск',
		preventDuplicates: true,
		tokenValue: 'Id'
	};

	$$.array.remove = function (arr, el) {
		var index;
		if ((index = arr.index(el)) > -1)
			arr.splice(index, 1);
	};

	$$.appBase = $('#appBase').attr('href');
	$$.appUrl = function (relativePath) {
		if (relativePath.length > 0 && relativePath[0] != '/')
			relativePath = $$.appBase + relativePath;
		return relativePath;
	};

})(window.$$ = window.$$ || {}, jQuery);