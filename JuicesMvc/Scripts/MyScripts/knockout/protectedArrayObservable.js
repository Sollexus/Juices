﻿ko.protectedObservable = function (initialValue) {

	var actualValue = ko.observableArray(initialValue);
	var tempValue = ko.observableArray(initialValue);

	var result = ko.computed({
		read: function () {
			return tempValue;
		},
		write: function (newValue) {
			tempValue = newValue;
		}
	}).extend({ notify: "always" });


	result.commit = function () {
		actualValue = tempValue;
	};

	result.reset = function () {
		tempValue = actualValue();
	};

	result.getTempValue = function () {
		return tempValue;
	};

	return result;
};