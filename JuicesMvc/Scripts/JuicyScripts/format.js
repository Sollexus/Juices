(function () {

	var defaultNumberFormatterTemplates =
	[
	  ["", function (obj) { return simpleNumberFormat(obj, "0"); }]
	, [null, function (obj) { return simpleNumberFormat(obj, "0"); }]
	, ["D", function (obj) { return simpleNumberFormat(obj, "0000"); }]
	, ["D2", function (obj) { return simpleNumberFormat(obj, "00"); }]
	, ["D3", function (obj) { return simpleNumberFormat(obj, "000"); }]
	, ["D4", function (obj) { return simpleNumberFormat(obj, "0000"); }]
	, ["D5", function (obj) { return simpleNumberFormat(obj, "00000"); }]
	, ["D6", function (obj) { return simpleNumberFormat(obj, "000000"); }]
	, ["D7", function (obj) { return simpleNumberFormat(obj, "0000000"); }]
	, ["D8", function (obj) { return simpleNumberFormat(obj, "00000000"); }]
	, ["D9", function (obj) { return simpleNumberFormat(obj, "000000000"); }]
	, ["F", function (obj) { return simpleNumberFormat(obj, "####.00"); }]
	, ["F0", function (obj) { return simpleNumberFormat(obj, "####"); }]
	, ["F1", function (obj) { return simpleNumberFormat(obj, "####.0"); }]
	, ["F3", function (obj) { return simpleNumberFormat(obj, "####.000"); }]
	, ["F4", function (obj) { return simpleNumberFormat(obj, "####.0000"); }]
	, ["F5", function (obj) { return simpleNumberFormat(obj, "####.00000"); }]
	, ["P", function (obj) { return simpleNumberFormat(obj, "#.####%"); }]
	, ["$C", function (obj) { return simpleNumberFormat(obj, "$ #,###.00"); }]
	, ["C$", function (obj) { return simpleNumberFormat(obj, "#,###.00 $"); }]
	, ["K", function (obj) { return formatNumberThousands(obj, "#,###"); }]
	, ["K1", function (obj) { return formatNumberThousands(obj, "#,###.0"); }]
	, ["M", function (obj) { return formatNumberMillions(obj, "#,##0"); }]
	, ["M1", function (obj) { return formatNumberMillions(obj, "#,##0.0"); }]
	, ["X", function (obj) { return formatNumberToHex(obj, 0); }]
	, ["X2", function (obj) { return formatNumberToHex(obj, 2); }]
	, ["X3", function (obj) { return formatNumberToHex(obj, 3); }]
	, ["X4", function (obj) { return formatNumberToHex(obj, 4); }]
	, ["X5", function (obj) { return formatNumberToHex(obj, 5); }]
	, ["X6", function (obj) { return formatNumberToHex(obj, 6); }]
	, ["X7", function (obj) { return formatNumberToHex(obj, 7); }]
	, ["X8", function (obj) { return formatNumberToHex(obj, 8); }]
	, ["X9", function (obj) { return formatNumberToHex(obj, 9); }]
	];

	var defaultBooleanFormatterTemplates =
  [
	 ["", function (v) { return "" + v; }]
	, [null, function (v) { return "" + v; }]
	, ["T", function (v) { return v ? "True" : "False"; }]
	, ["t", function (v) { return v ? "true" : "false"; }]
	, ["D", function (v) { return v ? "1" : "0"; }]
  ];

	var defaultDateFormatterTemplates =
  [
	 ["", function (obj) { return dateFormat(obj, "ddd MMM dd yyyy HH:mm:ss"); }]
	, [null, function (obj) { return dateFormat(obj, "ddd MMM dd yyyy HH:mm:ss"); }]
	, ["ShortDate", function (obj) { return dateFormat(obj, "M/d/yy"); }]
	, ["MediumDate", function (obj) { return dateFormat(obj, "MMM d, yyyy"); }]
	, ["LongDate", function (obj) { return dateFormat(obj, "MMMM d, yyyy"); }]
	, ["FullDate", function (obj) { return dateFormat(obj, "dddd, MMMM d, yyyy"); }]
	, ["ShortTime", function (obj) { return dateFormat(obj, "h:mm tt"); }]
	, ["MediumTime", function (obj) { return dateFormat(obj, "h:mm:ss tt"); }]
	, ["LongTime", function (obj) { return dateFormat(obj, "h:mm:ss tt"); }]
	, ["IsoDate", function (obj) { return dateFormat(obj, "yyyy-MM-dd"); }]
	, ["IsoTime", function (obj) { return dateFormat(obj, "HH:mm:ss"); }]
	, ["IsoDateTime", function (obj) { return dateFormat(obj, "yyyy-MM-ddTHH:mm:ss"); }]
	, ["IsoUtcDateTime", function (obj) { return dateFormat(obj, "UTC:yyyy-MM-ddTHH:mm:ssZ"); }]
  ];

	var ObjectFormatterOptions = {
		UnknownTypeHandler: function (objType) {
			throw "Unknown type : " + objType;
		},
		TypesFormatters:
	  [
		{
			ObectType: "[object Number]"
		  , DefaultFormatter: function (obj, tmpl) { return simpleNumberFormat(obj, tmpl); }
		  , FormattersPreset: defaultNumberFormatterTemplates
		},
		{
			ObectType: "[object Date]"
		  , DefaultFormatter: function (obj, tmpl) { return dateFormat(obj, tmpl); }
		  , FormattersPreset: defaultDateFormatterTemplates
		},
		{
			ObectType: "[object Boolean]"
		  , DefaultFormatter: function (obj, tmpl) { return "" + obj; }
		  , FormattersPreset: defaultBooleanFormatterTemplates
		},
		{
			ObectType: "[object Array]"
		  , DefaultFormatter: function (obj, tmpl) { return formatArray(tmpl, obj); }
		  , FormattersPreset: null
		},
		{
			ObectType: "[object String]"
		  , DefaultFormatter: function (obj, tmpl) { return obj; }
		  , FormattersPreset: null
		},
		{
			ObectType: "[object Object]"
		  , DefaultFormatter: function (obj, tmpl) { return "" + obj; }
		  , FormattersPreset: null
		}
	  ]
	};

	// Internationalization strings
	var dateFormatOptions = {
		defaultDateFormatMask: "ddd MMM dd yyyy HH:mm:ss"
	, shortDayNames: ["Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat"]
	, fullDayDames: ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"]
	, shortMonthNames: ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"]
	, fullMonthNames: ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"]
	, tokenAllowedChars: /d{1,4}|M{1,4}|m{1,4}|y{1,4}|([HhMsTt])\1?|[LloSZ]|"[^"]*"|'[^']*'/g
	, dateFormattersOld: {
		dddd: function (date) { return dateFormatOptions.fullDayDames[date.getDay()]; },
		ddd: function (date) { return dateFormatOptions.shortDayNames[date.getDay()]; },
		dd: function (date) { return padObject(date.getDate(), 2); },
		d: function (date) { return date.getDate(); },
		hh: function (date) { var hours = date.getHours(); return (hours == 0) ? 12 : (hours <= 12) ? padObject(hours, 2) : padObject(hours - 12, 2); },
		h: function (date) { var hours = date.getHours(); return (hours == 0) ? 12 : (hours <= 12) ? hours : hours - 12; },
		HH: function (date) { return padObject(date.getHours(), 2); },
		H: function (date) { return date.getHours(); },
		mm: function (date) { return padObject(date.getMinutes(), 2); },
		m: function (date) { return date.getMinutes(); },
		MMMM: function (date) { return dateFormatOptions.fullMonthNames[date.getMonth()]; },
		MMM: function (date) { return dateFormatOptions.shortMonthNames[date.getMonth()]; },
		MM: function (date) { return padObject(date.getMonth() + 1, 2); },
		M: function (date) { return date.getMonth() + 1; },
		ss: function (date) { return padObject(date.getSeconds(), 2); },
		s: function (date) { return date.getSeconds(); },
		tt: function (date) { return (date.getHours() < 12) ? "AM" : "PM"; },
		t: function (date) { return (date.getHours() < 12) ? "A" : "P"; },
		yyyy: function (date) { return date.getFullYear(); },
		yy: function (date) { return date.getFullYear().toString().slice(-2); },
		ffff: function (date) { return date.getMilliseconds(); },
		fff: function (date) { return date.getMilliseconds().toString().substr(0, 3); },
		ff: function (date) { return date.getMilliseconds().toString().substr(0, 2); },
		f: function (date) { return date.getMilliseconds().toString().substr(0, 1); },
		FFFF: function (date) { return ifZeroStr(date.getMilliseconds().toString(), ''); },
		FFF: function (date) { return ifZeroStr(date.getMilliseconds().toString().substr(0, 3), ''); },
		FF: function (date) { return ifZeroStr(date.getMilliseconds().toString().substr(0, 2), ''); },
		F: function (date) { return ifZeroStr(date.getMilliseconds().toString().substr(0, 1), ''); }
	}
	};

	var defaultFormatNumberOptions = {
		format: "#,###.00",
		decimalSeparatorAlwaysShown: false,
		nanForceZero: true,
		round: true,
		groupSeparators: ", " //comma and space
	};

	function format(templateStr) {
		try {
			if (0 == arguments.length)
				throw "Empty function parameters";


			if (1 == arguments.length) {
				try {
					return formatArgument("0", [arguments[0]]);
				} catch (err) {
					return err;
				}
			}

			var f = Object.prototype.toString.call(arguments[0]) !== "[object String]"
			  ? arguments[0].toString()
			  : arguments[0];

			if (2 == arguments.length && !f.contains("{") && !f.contains("}")) {
				var arg = arguments[1];
				f = (null == f || "" == f)
					  ? "0"
					  : "0:" + f;

				try {
					return formatArgument(f, [arg]);
				} catch (err) {
					return err;
				}

			}

			var formatStr = arguments[0];

			if (null == formatStr)
				throw "Empty string template";

			//copy of arguments
			var args = Array();
			for (var arg_idx = 1; arg_idx < arguments.length; arg_idx++) {
				args[arg_idx - 1] = arguments[arg_idx];
			}

			var len = formatStr.length;
			var pos = 0;
			var ch = "";
			var ret = "";
			var argumentFormat = "";
			var isArgumentFormat = false;
			var isArgumentFormatEnd = false;

			while (true) {
				while (pos < len) {
					ch = formatStr[pos];

					pos++;

					if ("}" == ch) {
						var isEscape = false;
						if (pos < len && formatStr[pos] == '}') {
							isEscape = true;
							pos++;
						}
						//else
						//	throw "Invalid format " + format;

						if (!isEscape)
							isArgumentFormatEnd = true;
					}

					if ("{" == ch) {
						if (pos < len && "{" == formatStr[pos])
							pos++;
						else {
							argumentFormat = "";
							isArgumentFormat = true;
							pos--;
							break;
						}
					}

					if (isArgumentFormatEnd) {
						var tmp;
						try {
							tmp = formatArgument(argumentFormat, args);
						}
						catch (err) {
							tmp = err;
						}
						ret += tmp;
						argumentFormat = "";
						isArgumentFormat = isArgumentFormatEnd = false;
					}
					else {
						if (isArgumentFormat)
							argumentFormat += ch;
						else
							ret += ch;
					}
				}
				if (pos == len)
					break;

				pos++;
			}

			return ret;
		}
		catch (err) {
			return err + "." + templateStr;
		}
	}

	function formatArgument(formatStr, formatArguments) {
		var index;
		var indexRaw = "";
		var len = formatStr.length;
		var pos = 0;
		var ch;
		var formatOptions = null;
		var isIndex = true;

		while (pos < len) {
			ch = formatStr[pos];

			if (":" == ch) {
				isIndex = false;
				formatOptions = "";
				pos++;
				continue;
			}

			if (isIndex) {
				if ("0123456789".indexOf(ch) == -1)
					throw "Invalid format argument";

				indexRaw += ch;
			} else {//is formatOptions 
				formatOptions += ch;
			}

			pos++;
		}
		index = parseInt(indexRaw);

		if (index >= formatArguments.length)
			throw "Incorrect index " + index;

		var obj = formatArguments[index];
		var objType = Object.prototype.toString.call(obj);

		var frm = getFormatterByObjType(objType);

		if (null == frm)
			return ObjectFormatterOptions.UnknownTypeHandler(objType);

		var preset_fn = getDefaultFormatterTemplate(formatOptions, frm.FormattersPreset, null);
		if (null != preset_fn && Object.prototype.toString.call(preset_fn) === "[object Function]")
			return preset_fn(obj);

		return frm.DefaultFormatter(obj, formatOptions);
	}

	function simpleNumberFormat(obj, formatStr) {
		var numberFormatOptions = { format: formatStr };
		return formatNumber(obj, numberFormatOptions);
	}

	function getFormatterByObjType(objType) {
		for (var i = 0; i < ObjectFormatterOptions.TypesFormatters.length; i++) {
			var el = ObjectFormatterOptions.TypesFormatters[i];
			if (el.ObectType == objType)
				return el;
		}
		return null;
	}

	function getDefaultFormatterTemplate(tmpl, templates, defaultValue) {
		if (undefined == templates || null == templates)
			return defaultValue || tmpl;

		var ret = null;
		for (var i = 0; i < templates.length; i++) {
			var el = templates[i];
			if (el[0] == tmpl) {
				ret = el[1];
				break;
			}
		}

		if (null != ret)
			return ret;

		return defaultValue || tmpl;
	}

	function formatNumberThousands(obj, tmpl) {
		var n = parseInt(obj);
		if (isNaN(n))
			throw "incorrect number " + obj;

		var n_abs = Math.abs(n);

		if (n_abs < 1000)
			return n;
		else if (n_abs > 1000)
			return simpleNumberFormat(n / 1000, tmpl) + "K";

		return null;
	}

	function formatNumberMillions(obj, tmpl) {
		var n = parseInt(obj);
		if (isNaN(n))
			throw "incorrect number " + obj;

		var n_abs = Math.abs(n);

		if (n_abs < 100000)
			return n;
		else if (n_abs > 100000)
			return simpleNumberFormat(n / 1000000, tmpl) + "M";

		return null;
	}

	function formatNumberToHex(obj, padN) {
		var n = parseInt(obj);
		padN = padN || 0;

		if (isNaN(n))
			throw "incorrect number " + obj;

		if (n < 0)
			n = 0xFFFFFFFF + n + 1;

		return padObject(n.toString(16).toUpperCase(), padN);
	}

	function formatArray(formatStr, obj) {
		var separator = formatStr;
		if (separator == null) separator = ", ";
		return obj.join(separator);
	}

	function extendFormatNumberOptions(options) {
		options = options || defaultFormatNumberOptions;

		if (undefined == options.format || null == options.format)
			options.format = defaultFormatNumberOptions.format;

		if (undefined == options.decimalSeparatorAlwaysShown || null == options.decimalSeparatorAlwaysShown)
			options.decimalSeparatorAlwaysShown = defaultFormatNumberOptions.decimalSeparatorAlwaysShown;

		if (undefined == options.nanForceZero || null == options.nanForceZero)
			options.nanForceZero = defaultFormatNumberOptions.nanForceZero;

		if (undefined == options.round || null == options.round)
			options.round = defaultFormatNumberOptions.round;

		return options;
	}

	/**
	* First parses a string and reformats it with the given options.
	* 
	* @param {Object} numberString
	* @param {Object} options
	*/
	function formatNumber(numberString, options) {
		options = extendFormatNumberOptions(options);

		var validFormat = "0#-,.";

		// strip all the invalid characters at the beginning and the end
		// of the format, and we'll stick them back on at the end
		// make a special case for the negative sign "-" though, so 
		// we can have formats like -$23.32
		var prefix = "";
		var negativeInFront = false;
		for (var i = 0; i < options.format.length; i++) {
			if (validFormat.indexOf(options.format.charAt(i)) == -1)
				prefix = prefix + options.format.charAt(i);
			else
				if (i == 0 && options.format.charAt(i) == '-') {
					negativeInFront = true;
					continue;
				}
				else
					break;
		}

		var suffix = "";
		for (i = options.format.length - 1; i >= 0; i--) {
			if (validFormat.indexOf(options.format.charAt(i)) == -1)
				suffix = options.format.charAt(i) + suffix;
			else
				break;
		}

		options.format = options.format.substring(prefix.length);
		options.format = options.format.substring(0, options.format.length - suffix.length);

		// now we need to convert it into a number
		//while (numberString.indexOf(group) > -1) 
		//	numberString = numberString.replace(group, '');
		//var number = new Number(numberString.replace(dec, ".").replace(neg, "-"));
		var number = new Number(numberString);

		return _formatNumber(number, options, suffix, prefix, negativeInFront);
	};

	/**
	* Formats a Number object into a string, using the given formatting options
	* 
	* @param {Object} numberString
	* @param {Object} options
	*/
	function _formatNumber(number, options, suffix, prefix, negativeInFront) {
		options = extendFormatNumberOptions(options);

		var dec = ".";
		var group = ",";
		var neg = "-";

		var forcedToZero = false;
		if (isNaN(number)) {
			if (options.nanForceZero == true) {
				number = 0;
				forcedToZero = true;
			} else
				return null;
		}

		// special case for percentages
		if (suffix == "%")
			number = number * 100;

		var returnString = "";
		if (options.format.indexOf(".") > -1) {
			var decimalPortion = dec;
			var decimalFormat = options.format.substring(options.format.lastIndexOf(".") + 1);

			// round or truncate number as needed
			if (options.round == true)
				number = new Number(_roundNumber(number, decimalFormat.length));
			else {
				var numStr = number.toString();
				numStr = numStr.substring(0, numStr.lastIndexOf('.') + decimalFormat.length + 1);
				number = new Number(numStr);
			}

			var decimalValue = number % 1;
			var decimalString = new String(_roundNumber(decimalValue, decimalFormat.length));
			decimalString = decimalString.substring(decimalString.lastIndexOf(".") + 1);

			for (var i = 0; i < decimalFormat.length; i++) {
				if (decimalFormat.charAt(i) == '#' && decimalString.charAt(i) != '0') {
					decimalPortion += decimalString.charAt(i);
					continue;
				} else if (decimalFormat.charAt(i) == '#' && decimalString.charAt(i) == '0') {
					var notParsed = decimalString.substring(i);
					if (notParsed.match('[1-9]')) {
						decimalPortion += decimalString.charAt(i);
						continue;
					} else
						break;
				} else if (decimalFormat.charAt(i) == "0")
					decimalPortion += decimalString.charAt(i);
			}
			returnString += decimalPortion;
		} else
			number = Math.round(number);

		var ones = Math.floor(number);
		if (number < 0)
			ones = Math.ceil(number);

		var onesFormat = "";
		if (options.format.indexOf(".") == -1)
			onesFormat = options.format;
		else
			onesFormat = options.format.substring(0, options.format.indexOf("."));

		var onePortion = "";
		if (!(ones == 0 && onesFormat.substr(onesFormat.length - 1) == '#') || forcedToZero) {
			// find how many digits are in the group

			for (var j = 0; j < defaultFormatNumberOptions.groupSeparators.length; j++) {
				if (onesFormat.lastIndexOf(defaultFormatNumberOptions.groupSeparators[j]) != -1) {
					group = defaultFormatNumberOptions.groupSeparators[j];
					break;
				}
			}

			var oneText = new String(Math.abs(ones));
			var groupLength = 9999;
			//if (onesFormat.lastIndexOf(",") != -1)
			if (onesFormat.lastIndexOf(group) != -1)
				//groupLength = onesFormat.length - onesFormat.lastIndexOf(",") - 1;
				groupLength = onesFormat.length - onesFormat.lastIndexOf(group) - 1;
			var groupCount = 0;
			for (var i = oneText.length - 1; i > -1; i--) {
				onePortion = oneText.charAt(i) + onePortion;
				groupCount++;
				if (groupCount == groupLength && i != 0) {
					onePortion = group + onePortion;
					groupCount = 0;
				}
			}

			// account for any pre-data padding
			if (onesFormat.length > onePortion.length) {
				var padStart = onesFormat.indexOf('0');
				if (padStart != -1) {
					var padLen = onesFormat.length - padStart;

					// pad to left with 0's or group char
					var pos = onesFormat.length - onePortion.length - 1;
					while (onePortion.length < padLen) {
						var padChar = onesFormat.charAt(pos);
						// replace with real group char if needed
						//if (padChar == ',')
						if (padChar == group[0])
							padChar = group;
						onePortion = padChar + onePortion;
						pos--;
					}
				}
			}
		}

		if (!onePortion && onesFormat.indexOf('0', onesFormat.length - 1) !== -1)
			onePortion = '0';

		returnString = onePortion + returnString;

		// handle special case where negative is in front of the invalid characters
		if (number < 0 && negativeInFront && prefix.length > 0)
			prefix = neg + prefix;
		else if (number < 0)
			returnString = neg + returnString;

		if (!options.decimalSeparatorAlwaysShown) {
			if (returnString.lastIndexOf(dec) == returnString.length - 1) {
				returnString = returnString.substring(0, returnString.length - 1);
			}
		}
		returnString = prefix + returnString + suffix;
		return returnString;
	}

	function _roundNumber(number, decimalPlaces) {
		var power = Math.pow(10, decimalPlaces || 0);
		var value = String(Math.round(number * power) / power);

		// ensure the decimal places are there
		if (decimalPlaces > 0) {
			var dp = value.indexOf(".");
			if (dp == -1) {
				value += '.';
				dp = 0;
			} else {
				dp = value.length - (dp + 1);
			}

			while (dp < decimalPlaces) {
				value += '0';
				dp++;
			}
		}
		return value;
	}

	/*
	* Date Format 1.0
	*
	* Includes enhancements by Scott Trenda <scott.trenda.net>,
	* Kris Kowal <cixar.com/~kris.kowal/>
	* and Steven Levithan <stevenlevithan.com>
	*
	* Accepts a date, a mask, or a date and a mask.
	* Returns a formatted version of the given date.
	* The date defaults to the current date/time.
	* The mask defaults to dateFormatOptions.defaultDateFormatMask.
	*/
	var dateFormat = function () {
		// Regexes and supporting functions are cached through closure
		return function (date, mask) {
			// You can't provide utc if you skip other args (use the "UTC:" mask prefix)
			if (arguments.length == 1 && Object.prototype.toString.call(date) == "[object String]" && !/\d/.test(date)) {
				mask = date;
				date = undefined;
			}

			// Passing date through Date applies Date.parse, if necessary
			date = date ? new Date(date) : new Date;
			if (isNaN(date)) throw SyntaxError("invalid date");

			mask = String(mask || dateFormatOptions.defaultDateFormatMask);

			// Allow setting the utc argument via the mask
			if (mask.slice(0, 4) == "UTC:") {
				mask = mask.slice(4);
			}

			var _ = dateFormatOptions.dateFormattersOld;

			return mask.replace(dateFormatOptions.tokenAllowedChars, function (maskPart) {
				return maskPart in _ ? _[maskPart](date) : maskPart; //call formatter function for token, or leave token as is (dont remove not token string)
				//return maskPart in _ ? _[maskPart](date) : maskPart.slice(1, maskPart.length - 1);//call formatter function for token, or remove token
			});
		};
	}();

	function padObject(obj, len) {
		var val = ('[object String]' !== Object.prototype.toString.call(obj))
		? obj.toString()
		: obj;
		len = len || 2;

		var res = '';

		for (var i = val.length; i < len; i++) {
			res += '0';
		}
		res += val;
		return res;
	}

	function ifZeroStr(str, value) {
		for (var i = 0; i < str.length; i++) {
			if (str[i] !== '0')
				return str;
		}
		return value;
	}

	$$.format = format;

}());