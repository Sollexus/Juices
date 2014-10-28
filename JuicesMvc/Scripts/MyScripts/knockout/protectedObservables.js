ko.protectedObservable = function (initialValue) {
	//private variables

	var isArr = initialValue instanceof Array;

	var actualValue = isArr ? ko.observableArray(initialValue) : ko.observable(initialValue),
        tempValue = isArr ? actualValue : initialValue;

	//computed observable that we will return
	var result = ko.computed({
		//always return the actual value
		read: function () {
			return isArr ? actualValue() : actualValue;
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