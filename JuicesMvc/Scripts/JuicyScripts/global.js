(function ($$) {

	// namespaces
	$$.defs = $$.defs || {};
	$$.ui = $$.ui || {};
	$$.dtos = $$.dtos || {};
	$$.utils = $$.utils || {};

	$$.appBase = $('#appBase').attr('href');
	$$.appUrl = function (relativePath) {
		if (relativePath.length > 0 && relativePath[0] != '/')
			relativePath = $$.appBase + relativePath;
		return relativePath;
	};

	$$.debounce = function (callback, threshold) {
		var timeout;
		return function () {
			var context = this, params = arguments;
			window.clearTimeout(timeout);
			timeout = window.setTimeout(function () {
				callback.apply(context, params);
			}, threshold);
		};
	};

	$$.throttle = function (callback, threshold) {
		var timeout;
		return function () {
			if (timeout) return;
			var context = this, params = arguments;
			timeout = window.setTimeout(function () {
				callback.apply(context, params);
				timeout = null;
			}, threshold);
		};
	};

	$$.htmlEncode = function (value) {
		return value ? $('<div/>').text(value).html() : '';
	};

	$$.htmlDecode = function (value) {
		return value ? $('<div/>').html(value).text() : '';
	};

	// extend JSON.parse to be able to parse dates in UNIX format
	var parseDate = function (value) {
		var result = new Date(value);
		return result;
	};

	var stringifyDate = function (date) {
		return Date.UTC(
		  date.getFullYear(),
		  date.getMonth(),
		  date.getDate(),
		  date.getHours(),
		  date.getMinutes(),
		  date.getSeconds(),
		  date.getMilliseconds());
	};

	/*var _parse = JSON.parse;
	JSON.parse = function (text) {
		return _parse(text, function (key, value) {
			if (typeof value === "string") {
				if (value.slice(0, 6) === "/Date(" && value.slice(-2) === ")/")
					return parseDate(+value.slice(6, -2));
			}
			return value;
			//        var a;
			//        if (typeof value === 'string') {
			//          a = /^(\d{4})-(\d{2})-(\d{2})T(\d{2}):(\d{2}):(\d{2}(?:\.\d*)?)Z$/.exec(value);
			//          if (a)
			//            return new Date(Date.UTC(+a[1], +a[2] - 1, +a[3], +a[4], +a[5], +a[6]));
			//        }
			//        return value;
		});
	};

	var _stringify = JSON.stringify;
	JSON.stringify = function (object, spacer) {
		return _stringify(object, function (key, value) {
			if (this[key] instanceof Date) {
				var date = this[key];
				return "\/Date(" + stringifyDate(date) + ")\/";
			}
			return value;
		}, spacer);
	};*/

	$$.beautifyJSON = function (json, spacer) {
		if (undefined == json || null == json)
			throw "Empty or null json";

		var jsonObj;
		var jsonObjType = Object.prototype.toString.call(json);

		switch (jsonObjType) {
			case '[object String]':
				jsonObj = JSON.parse(json);
				break;
			case '[object Object]':
				jsonObj = json;
				break;
			default:
				throw "Unknown json type : " + jsonObjType;
		}


		if (undefined == spacer || null == spacer)
			spacer = 2;

		return JSON.stringify(jsonObj, spacer);
	};
	/*
		jQuery.parseJSON = function (json) {
			return JSON.parse(json);
		};*/

	String.prototype.strToJson = function () {
		return JSON.parse(this);
	};

	Array.prototype.remove = function (predicate) {
		for (var i = this.length - 1; i >= 0; i--) {
			if (predicate(this[i])) {
				this.splice(i, 1);
			}
		}
	};

	Array.prototype.select = function (selector) {
		var result = [];
		for (var i = 0; i < this.length; i++) {
			result.push(selector(this[i]));
		}
		return result;
	};

	Array.prototype.where = function (predicate) {
		var result = [];
		for (var i = 0; i < this.length; i++) {
			if (predicate(this[i])) {
				result.push(this[i]);
			}
		}
		return result;
	};

	Array.prototype.find = function (predicate) {
		for (var i = 0; i < this.length; i++) {
			if (predicate(this[i])) {
				return this[i];
			}
		}
		return null;
	};

	Array.prototype.each = function (action) {
		for (var i = 0; i < this.length; i++) {
			action(this[i]);
		}
	};

	Array.prototype.orderBy = function (direction, comparator) {
		return (direction.toLowerCase() === 'asc') ? this.orderByAsc(comparator) : this.orderByDesc(comparator);
	};

	Array.prototype.orderByAsc = function (comparator) {
		this.sort(comparator);
		return this;
	};

	Array.prototype.orderByDesc = function (comparator) {
		this.sort(comparator).reverse();
		return this;
	};

	Array.prototype.take = function (count) {
		var i = 0;
		var result = [];
		while (i < count && i < this.length) {
			result.push(this[i]);
			i++;
		}
		return result;
	};

	Array.prototype.min = function () {
		var min = undefined;
		if (this.length > 0) {
			min = this[0];
			for (var i = 1; i < this.length; i++) {
				if (this[i] < min)
					min = this[i];
			}
		}
		return min;
	};

	Array.prototype.max = function () {
		var max = undefined;
		if (this.length > 0) {
			max = this[0];
			for (var i = 1; i < this.length; i++) {
				if (this[i] > max)
					max = this[i];
			}
		}
		return max;
	};

	Array.prototype.avg = function () {
		var avg = undefined;
		if (this.length > 0) {
			avg = this.sum() / this.length;
		}
		return avg;
	};

	Array.prototype.sum = function () {
		var sum = undefined;
		if (this.length > 0) {
			sum = this[0];
			for (var i = 1; i < this.length; i++)
				sum += this[i];
		}
		return sum;
	};

	Array.prototype.count = function (predicate) {
		if (!predicate)
			return this.length;
		var count = 0;
		for (var i = 0; i < this.length; i++) {
			if (predicate(this[i]))
				count++;
		}
		return count;
	};

	// took here http://stackoverflow.com/questions/7616461/generate-a-hash-from-string-in-javascript-jquery
	String.prototype.getHashCode = function () {
		var hash = 0, i, c;
		if (this.length == 0) return hash;
		for (i = 0; i < this.length; i++) {
			c = this.charCodeAt(i);
			hash = ((hash << 5) - hash) + c;
			hash = hash & hash; // Convert to 32bit integer
		}
		return hash;
	};

	$$.formatNum = function (number, format) {
		number = parseFloat(number);

		if (!isFinite(number)) // return NaN in case number is not a Number
			return number;

		if (format == undefined)
			format = '';

		var strNumber = number + '';

		var numPointPos = strNumber.indexOf('.');
		var strNumLeft = (numPointPos > 0) ? strNumber.substring(0, numPointPos) : strNumber;
		var strNumRight = (numPointPos > 0) ? strNumber.substring(numPointPos + 1) : '';

		var fmtPointPos = format.indexOf('.');
		var strFmtLeft = (fmtPointPos > 0) ? format.substring(0, fmtPointPos) : format;
		var strFmtRight = (fmtPointPos > 0) ? format.substring(fmtPointPos + 1) : '';

		var strLeft = doFormat(formatter(strNumLeft, strFmtLeft, false));
		var strRight = doFormat(formatter(strNumRight, strFmtRight, true));

		return (strRight.length > 0) ? strLeft + '.' + strRight : strLeft;

		function doFormat(frmtr) {
			while (frmtr.nextFmtChar()) {
				var f = frmtr.currFmtChar();
				if (f == '#') {
					if (frmtr.nextNumChar()) {
						frmtr.pushNumChar();
					}
				} else if (f == '0') {
					if (frmtr.nextNumChar())
						frmtr.pushNumChar();
					else
						frmtr.pushFmtChar();
				} else {
					if (frmtr.hasNumChar())
						frmtr.pushFmtChar();
				}
			}
			return frmtr.getResult();
		}

		function formatter(strNum, strFmt, dir) {
			var result = [],
				i = dir ? -1 : strFmt.length,
				j = dir ? -1 : strNum.length;

			var push = function (c) {
				if (dir)
					result.push(c);
				else
					result.splice(0, 0, c); // insert at 0 pos
			};

			return {
				nextFmtChar: function () {
					i += dir ? 1 : -1;
					return dir ? i < strFmt.length : i >= 0;
				},
				nextNumChar: function () {
					j += dir ? 1 : -1;
					return dir ? j < strNum.length : j >= 0;
				},
				hasNumChar: function () {
					return dir ? j < strNum.length : j >= 0;
				},
				currFmtChar: function () { return strFmt[i]; },
				pushNumChar: function () { push(strNum[j]); },
				pushFmtChar: function () { push(strFmt[i]); },
				getResult: function () { return result.join(''); }
			};
		}
	};
}(window.$$ = window.$$ || {}));