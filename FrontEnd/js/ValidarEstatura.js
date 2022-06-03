var zChar = new Array(' ', '(', ')', '-', '.');
var maxheightlength = 4;
var estaturavalor1;
var estaturavalor2;
var cursorposition;

function ParseForValue1(object) {
	estaturavalor1 = convertir(object.value, zChar);
}
function ParseForValue2(object) {
	estaturavalor2 = convertir(object.value, zChar);
}

function abajo(object, e) {
	if (e) {
		e = e
	} else {
		e = window.event
	}
	if (e.which) {
		var keycode = e.which
	} else {
		var keycode = e.keyCode
	}

	ParseForValue1(object)

	if (keycode >= 48) {
		ValidateHeight(object)
	}
}

function arriba(object, e) {
	if (e) {
		e = e
	} else {
		e = window.event
	}
	if (e.which) {
		var keycode = e.which
	} else {
		var keycode = e.keyCode
	}
	ParseForValue2(object)
}

function GetCursorPosition() {

	var t1 = estaturavalor1;
	var t2 = estaturavalor2;
	var bool = false
	for (i = 0; i < t1.length; i++) {
		if (t1.substring(i, 1) != t2.substring(i, 1)) {
			if (!bool) {
				cursorposition = i
				bool = true
			}
		}
	}
}

function ValidateHeight(object) {

	var p = object.value;

	p = p.replace(/[^\d]*/gi, "")

	if (p.length <= 1) {
		object.value = p
	}
	else if (p.length > 1) {
		l30 = p.length;
		p30 = p.substring(0, 1);
		p30 = p30 + "."

		p31 = p.substring(1, l30);
		pp = p30 + p31;

		object.value = pp.substring(0, maxheightlength);
	}

	GetCursorPosition()

	if (cursorposition >= 0) {
		if (cursorposition == 0) {
			cursorposition = 2
		} else if (cursorposition <= 2) {
			cursorposition = cursorposition + 1
		} else if (cursorposition <= 5) {
			cursorposition = cursorposition + 2
		} else if (cursorposition == 6) {
			cursorposition = cursorposition + 2
		} else if (cursorposition == 7) {
			cursorposition = cursorposition + 4
			e1 = object.value.indexOf(')')
			e2 = object.value.indexOf('-')
			if (e1 > -1 && e2 > -1) {
				if (e2 - e1 == 4) {
					cursorposition = cursorposition - 1
				}
			}
		} else if (cursorposition < 11) {
			cursorposition = cursorposition + 3
		} else if (cursorposition == 11) {
			cursorposition = cursorposition + 1
		} else if (cursorposition >= 12) {
			cursorposition = cursorposition
		}

		var txtRange = object.createTextRange();
		txtRange.moveStart("character", cursorposition);
		txtRange.moveEnd("character", cursorposition - object.value.length);
		txtRange.select();
	}

}

function convertir(sStr, sChar) {
	if (sChar.length == null) {
		zChar = new Array(sChar);
	}
	else zChar = sChar;

	for (i = 0; i < zChar.length; i++) {
		sNewStr = "";

		var iStart = 0;
		var iEnd = sStr.indexOf(sChar[i]);

		while (iEnd != -1) {
			sNewStr += sStr.substring(iStart, iEnd);
			iStart = iEnd + 1;
			iEnd = sStr.indexOf(sChar[i], iStart);
		}
		sNewStr += sStr.substring(sStr.lastIndexOf(sChar[i]) + 1, sStr.length);

		sStr = sNewStr;
	}

	return sNewStr;
}

