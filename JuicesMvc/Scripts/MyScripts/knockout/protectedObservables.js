ko.protectedObservable = function (initialValue) {
	//private variables

	var actualValue = ko.observable(initialValue);
	var tempValue = initialValue;

	//computed observable that we will return
	var result = ko.computed({
		//always return the actual value
		read: function () {
			actualValue();
		},
		//stored in a temporary spot until commit
		write: function (newValue) {
			tempValue = newValue;
		}
	}).extend({ notify: "always" });


	//if different, commit temp value
	result.commit = function () {
		if (tempValue !== actualValue()) {
			actualValue(tempValue);
		}
	};

	//force subscribers to take original
	result.reset = function () {
		actualValue.valueHasMutated();
		tempValue = actualValue();   //reset temp value
	};

	result.getTempValue = function () {
		return tempValue;
	};

	return result;
};