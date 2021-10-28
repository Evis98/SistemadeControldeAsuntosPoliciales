
var zChar = new Array(' ', '(', ')', '-', '.');
var maxphonelength = 9;
var phonevalue1;
var phonevalue2;
var cursorposition;

function ParseForNumber1(object) {
	phonevalue1 = ParseChar(object.value, zChar);
}
function ParseForNumber2(object) {
	phonevalue2 = ParseChar(object.value, zChar);
}

$('#btnSave').click(function () {
	$('#show').modal('hide');
});

function backspacerUP(object, e) {
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

	ParseForNumber1(object)

	if (keycode >= 48) {
		ValidatePhone(object)
	}
}

function backspacerDOWN(object, e) {
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
	ParseForNumber2(object)
}

function GetCursorPosition() {

	var t1 = phonevalue1;
	var t2 = phonevalue2;
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

function ValidatePhone(object) {

	var p = phonevalue1

	p = p.replace(/[^\d]*/gi, "")

	if (p.length < 4) {
		object.value = p
	}
	else if (p.length >= 4) {

		l30 = p.length;
		p30 = p.substring(0, 4);
		p30 = p30 + "-"

		p31 = p.substring(4, l30);
		pp = p30 + p31;



		object.value = pp.substring(0, maxphonelength);
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

function ParseChar(sStr, sChar) {
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


for (i = 0; i < document.querySelectorAll('.inputphone').length; i++) {
	document.getElementsByClassName('inputphone')[i].onkeydown = function (e) {
		backspacerDOWN(this, e);
	}

	document.getElementsByClassName('inputphone')[i].onkeyup = function (e) {
		backspacerUP(this, e);
	}
}


var TNombre = document.getElementById("TNombre");

TNombre.addEventListener("input", function (event) {
	this.value = this.value.toUpperCase();
});

var TContacto = document.getElementById("TContacto");

TContacto.addEventListener("input", function (event) {
	this.value = this.value.toUpperCase();
});

DetalleE.addEventListener("form-control", function (event) {
	this.value = this.value.toUpperCase();
});

function sololetras(e) {

	key = e.keyCode || e.which;
	teclado = String.fromCharCode(key).toUpperCase();
	letras = " ABCDEFGHIJKLMNÑOPQRSTUVWXYZÁÉÍÓÚ";
	especiales = "8-37-38-46-164";
	teclado_especial = false;
	for (var i in especiales) {
		if (key == especiales[i]) {
			teclado_especial = true; break;
		}
	}
	if (letras.indexOf(teclado) == -1 && !teclado_especial) {
		return false;
	}
}
function sololetrassimbolos(e) {

	key = e.keyCode || e.which;
	teclado = String.fromCharCode(key).toUpperCase();
	letras = " ABCDEFGHIJKLMNÑOPQRSTUVWXYZÁÉÍÓÚ,.-";
	especiales = "8-37-38-46-164";
	teclado_especial = false;
	for (var i in especiales) {
		if (key == especiales[i]) {
			teclado_especial = true; break;
		}
	}
	if (letras.indexOf(teclado) == -1 && !teclado_especial) {
		return false;
	}
}

function sololetrasnumerossimbolos(e) {
	key = e.keyCode || e.which;
	teclado = String.fromCharCode(key).toUpperCase();
	letras = " ABCDEFGHIJKLMNÑOPQRSTUVWXYZÁÉÍÓÚ,.-1234567890";
	especiales = "8-37-38-46-164";
	teclado_especial = false;
	for (var i in especiales) {
		if (key == especiales[i]) {
			teclado_especial = true; break;
		}
	}
	if (letras.indexOf(teclado) == -1 && !teclado_especial) {
		return false;
	}
}

function sololetrasnumeros(e) {
	key = e.keyCode || e.which;
	teclado = String.fromCharCode(key).toUpperCase();
	letras = " ABCDEFGHIJKLMNÑOPQRSTUVWXYZÁÉÍÓÚ1234567890";
	especiales = "8-37-38-46-164";
	teclado_especial = false;
	for (var i in especiales) {
		if (key == especiales[i]) {
			teclado_especial = true; break;
		}
	}
	if (letras.indexOf(teclado) == -1 && !teclado_especial) {
		return false;
	}
}
function observacionesN() {
	document.getElementById("observacion").defaultValue = "N/A";
}

function aparecerPadre() {
	var checkBox = document.getElementById("checkboxPadre");
	var text = document.getElementById("nombrePadre");
	if (checkBox.checked == true) {
		text.style.display = "block";
	} else {
		text.style.display = "none";
	}
}

function aparecerMadre() {
	var checkBox = document.getElementById("checkboxMadre");
	var text = document.getElementById("nombreMadre");
	if (checkBox.checked == true) {
		text.style.display = "block";
	} else {
		text.style.display = "none";
	}
}

function aparecerObservaciones() {
	var checkBox = document.getElementById("checkboxObservaciones");
	var text = document.getElementById("Obs");
	if (checkBox.checked == true) {
		text.style.display = "block";
	} else {
		text.style.display = "none";
	}
}

function aparecerTatuajes() {
	var checkBox = document.getElementById("checkboxTatuajes");
	var text = document.getElementById("nombreTatuajes");
	if (checkBox.checked == true) {
		text.style.display = "block";
	} else {
		text.style.display = "none";
	}
}