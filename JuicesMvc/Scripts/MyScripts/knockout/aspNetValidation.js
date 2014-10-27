// depends on knockout

// <div data-bind="foreach: Items">
// <input type="text" data-bind="value: FirstName, aspnetIndexedName: { name: 'Items', property: 'FirstName', index: Line }" />
// </div>

ko.bindingHandlers.aspnetIndexedName = {
	update: function(element, valueAccessor, allBindings, viewModel, bindingContext) {
		var name, property, index;
		var bindings = allBindings().aspnetIndexedName;

		// If just a string, parse it as "name.property".  For primitive values, property is empty.
		if (bindings === undefined) {
			name = ko.unwrap(valueAccessor());
			property = undefined;
			//index = bindingContext.$index;
		} else if (typeof bindings === "string") {
			var parts = bindings.split(".");
			name = parts[0];
			property = parts[1];
			//index = bindingContext.$index();
		} else {
			name = bindings.name;
			property = bindings.property;
			index = bindings.index || bindingContext.$index();
		}

		var fullname = index === undefined ? name : name + '[' + index + ']';
		if (property)
			fullname = fullname + '.' + property;
		var $elem = $(element);

		$elem.attr({ name: fullname, id: fullname });

		// Is there a modelstate error?
		var modelStateProperty = bindings.modelState || 'modelState';
		var modelState = ko.unwrap(bindingContext.$data[modelStateProperty]);
		var errors;
		if (modelState && modelState[fullname] && (errors = modelState[fullname].Errors).length) {
			$elem.parent().addClass("has-error");
			var errorTemplate = "";
			for (var i = 0; i < errors.length; i++) {
				errorTemplate += "<h5>" + errors[i].ErrorMessage + "</h5>";
			}
			$elem.tooltip({html: true, title: errorTemplate});
		} else {
			$elem.parent().removeClass("has-error");
		}

	}
};

// Populates an ASP.Net MVC validation control that is rendered within a foreach: binding.  The
// data-valmsg-for attribute is given the name of the input control, and the validation
// message is inserted.
// The validation messages must be serialized into the viewmodel.
//
// Example markup:
// <div data-bind="foreach: Items">
//   <input type="text" data-bind="value: FirstName, aspnetIndexedName: 'Items.FirstName' }" />
//   <span class="control-label" data-bind="aspnetIndexedValidation: 'Items.FirstName'}" />
// </div>
//
// Example view model:
// function MyViewModel {
//   var self = this;
//   ...
//   // Serialize the model state to capture the validation messages.
//   self.modelState = @Html.Raw(Newtonsoft.Json.JsonConvert.SerializeObject(ViewData.ModelState));
// }

///TODO: Check if if works at all. And someone would call that a library, lel :)
ko.bindingHandlers.aspnetIndexedValidation = {
	update: function (element, valueAccessor, allBindings, viewModel, bindingContext) {

		var name, index, property;

		var bindings = allBindings().aspnetIndexedValidation;

		// If parameters are just a string, it's the name
		if (typeof bindings === "string") {
			var parts = bindings.split(".");
			name = parts[0];
			property = parts[1];
			index = bindingContext.$index();
		}
		else {
			name = bindings.name;
			property = bindings.property;
			index = bindings.index || bindingContext.$index();
		}

		var fullname = name + '[' + index + ']';
		if (property)
			fullname = fullname + '.' + property;

		var $elem = $(element);

		$elem.attr({ 'data-valmsg-for': fullname });
		$elem.attr({ 'data-valmsg-replace': true });

		var modelStateProperty = bindings.modelState || 'modelState';

		// Insert initial error message
		var modelState = bindingContext.$root[modelStateProperty];
		if (modelState && modelState[fullname] && modelState[fullname].Errors.length) {
			$elem.text(modelState[fullname].Errors[0].ErrorMessage);
			$elem.addClass("field-validation-error");
		}
		else {
			$elem.addClass("field-validation-valid");
		}
	},
};