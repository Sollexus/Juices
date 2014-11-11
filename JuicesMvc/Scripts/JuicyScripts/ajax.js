(function ($) {

	// number of running ajax requests
	$$.ajaxCallsCount = 0;

	$$.ajax = function (options) {
		var _onDone;
		var _onFail;
		var _onAlways;
		var _$loader;
		var _$container;

		return {
			dom: function (loader, container) {
				_$loader = $(loader);
				_$container = $(container);
				return this;
			},
			done: function (onDone) {
				_onDone = onDone;
				return this;
			},
			fail: function (onFail) {
				_onFail = onFail;
				return this;
			},
			always: function (onAlways) {
				_onAlways = onAlways;
				return this;
			},
			run: function (exclusive) {
				if (exclusive && $$.ajaxCallsCount > 0)
					return this;

				var processDom = function (showLoader) {
					if (showLoader) {
						if (_$container)
							_$container.hide();
						if (_$loader)
							_$loader.show();
					} else {
						if (_$loader)
							_$loader.hide();
						if (_$container)
							_$container.show();
					}
				};

				var showError = function (options, jqXHR) {
					if (jqXHR.status != 200 && jqXHR.responseText) {
						var matches = jqXHR.responseText.match(RegExp('<!--([\\s\\S]+)?-->'));
						$$.error(jqXHR.status + ' ' + jqXHR.statusText,
						  $$.format('[Url]:{0} {1} \n\n {2}', options.type, options.url, ((matches.length > 0) ? $.trim(matches[1]) : "")));
					}
				};

				processDom(true);
				$$.ajaxCallsCount++;

				var oldComplete = options.complete;
				var oldSuccess = options.success;
				var oldErrors = options.error;

				options.complete = function (jqXHR, textStatus) {
					$$.ajaxCallsCount--;
					var completes = (oldComplete instanceof Array) ? oldComplete : [oldComplete];
					completes.push(_onAlways);
					$.each(completes, function (idx, complete) {
						if (complete && $.isFunction(complete))
							complete.call(this, jqXHR, textStatus);
					});
				};
				options.success = function (data, textStatus, jqXHR) {
					processDom(false);
					var successes = (oldSuccess instanceof Array) ? oldSuccess : [oldSuccess];
					successes.push(_onDone);
					$.each(successes, function (idx, success) {
						if (success && $.isFunction(success))
							success.call(this, data, textStatus, jqXHR);
					});
				};
				options.error = function (jqXHR, textStatus, errorThrown) {
					var errors = (oldErrors instanceof Array) ? oldErrors : [oldErrors];
					errors.push(_onFail);
					$.each(errors, function (idx, error) {
						if (error && $.isFunction(error))
							error.call(this, jqXHR, textStatus, errorThrown);
					});
					// TODO add more error handling code, separate server and client errors
					showError(options, jqXHR);
				};

				return $.ajax(options);
			}
		};
	};

	$$.post = function (url, data, jsonData) {
		return $$.ajax({ type: 'POST', url: url, data: (jsonData) ? { dto: JSON.stringify(data) } : data });
	};

	$$.get = function (url, data, dataAsDto) {
		return $$.ajax({ type: 'GET', url: url, dataType: 'json', data: (dataAsDto) ? { dto: JSON.stringify(data) } : data });
	};

	$$.postJson = function (url, data) {
		return $$.ajax({ type: 'POST', url: url, contentType: "application/json", data: JSON.stringify(data) });
	};

	$$.getJson = function (url, data, cache) {
		return $$.ajax({ type: 'GET', url: url, cache: cache, contentType: "application/json", data: JSON.stringify(data) });
	};

}(jQuery));
