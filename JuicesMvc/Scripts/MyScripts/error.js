(function ($) {
	$$.error = function (title, message) {
		console.log(arguments);
		var error = $('#error');
		if (error.length == 0) {
			$('body').append(
			  '<div id="overlay"></div>' +
			  '<div id="error">' +
				'<div class="error-content">' +
				  '<div class="error-title" title=""></div>' +
				  '<div class="error-stack" title=""><pre><code></code></pre></div>' +
				  '<div class="error-footer">Press F5 to refresh browser page.</div>' +
				'</div>' +
			  '</div>');
			error = $('#error');
			$('#error .error-title').click(function () { $$.utils.selectText(this); });
			$('#error .error-stack pre').click(function () { $$.utils.selectText(this); });
		}
		$('#error .error-title').text(title).attr('title', title);
		$('#error .error-stack pre code').text(message);

		$('#overlay').show();
		error.show();
	};
}(jQuery));
