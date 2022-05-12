
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

$('#quitaPolicia').on('show.bs.modal', function (e) {
	var bookId = $(e.relatedTarget).data('book-id');
	document.getElementsByName("quitarPolicia")[0].setAttribute("id", bookId);
});

$('#modalBusqueda').on('show.bs.modal', function (e) {
	var bookId = $(e.relatedTarget).data('book-id');
	document.getElementsByName("agregarPolicia")[0].setAttribute("id", bookId);
});

$('#modalBusquedaPersona').on('show.bs.modal', function (e) {
	var bookId = $(e.relatedTarget).data('book-id');
	document.getElementsByName("agregarPersona")[0].setAttribute("id", bookId);
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

if (document.getElementById("TipoRequisito") != null) {
	document.getElementById("TipoRequisito").addEventListener("change", function () {
		var tipoRequisito = document.getElementById("TipoRequisito")
		var tipoSeleccion = tipoRequisito.value;
		if (tipoSeleccion == 2) {
			document.getElementById("FechaVencimiento").style.display = 'none';
		} else {
			document.getElementById("FechaVencimiento").style.display = 'block';
		}
	});
}

if (document.getElementById("uploadfile") != null) {
	var uploadField = document.getElementById("uploadfile");
	uploadField.onchange = function () {
		if (this.files[0].size > 4194304) {
			error.textContent = "Filesize too big"
			error.style.color = "red"
			this.value = "";
		} else {
			error.textContent = ""
		}
	}
}

if (document.getElementById("busquedaFechaFinalB")) {
	var fechainicio = document.getElementById("busquedaFechaInicioB");
	var fechafinal = document.getElementById("busquedaFechaFinalB");
	fechafinal.onchange = function () {
		if (fechainicio.value > fechafinal.value) {
			errorFecha.textContent = "La fecha de inicio no puede ser mayor a la fecha de final"
			errorFecha.style.color = "red"
			this.value = "";
		} else {
			errorFecha.textContent = ""
		}
	}
}

if (document.getElementById("busquedaFechaFinalH")) {
	var fechainicio = document.getElementById("busquedaFechaInicioH");
	var fechafinal = document.getElementById("busquedaFechaFinalH");
	fechafinal.onchange = function () {
		if (fechainicio.value > fechafinal.value) {
			errorFechaH.textContent = "La fecha de inicio no puede ser mayor a la fecha de final"
			errorFechaH.style.color = "red"
			this.value = "";
		} else {
			errorFechaH.textContent = ""
		}
	}
}



if (document.getElementById("busquedaFechaFinalP")) {
	var fechainicio = document.getElementById("busquedaFechaInicioP");
	var fechafinal = document.getElementById("busquedaFechaFinalP");
	fechafinal.onchange = function () {
		if (fechainicio.value > fechafinal.value) {
			errorFechaP.textContent = "La fecha de inicio no puede ser mayor a la fecha de final"
			errorFechaP.style.color = "red"
			this.value = "";
		} else {
			errorFechaP.textContent = ""
		}
	}
}

//if (document.getElementById("NOfendido3")) {
//	var auxO3 = document.getElementById("NOfendido3");
//	var auxO2= document.getElementById("NOfendido2");
//	auxO3.onchange = function () {
//		if (auxO3.value != null) {
//			auxO2.setAttribute('required', '');
//			auxO3.setAttribute('required', '');
//			//errorFechaP.textContent = "La fecha de inicio no puede ser mayor a la fecha de final"
//			//errorFechaP.style.color = "red"
//			//this.value = "";
//		//} else {
//		//	/*errorFechaP.textContent = ""*/
//		//}
//	}
//}

//function parteValidacionesPersonas() {
//	/*  var auxO1 = document.getElementById("busquedaFechaInicioB");*/
//	var auxO2 = document.getElementById("idOfendido2");
//	var auxO3 = document.getElementById("idOfendido3");
//	//var auxO4 = document.getElementById("textFechaFinal");
//	//var auxO5 = document.getElementById("estadoBitacora");
//	//var auxT1 = document.getElementById("filtroSeleccionado");
//	//var auxT2 = document.getElementById('busqueda');
//	if (auxO3.value != null) {
//		auxO2.setAttribute('required', '');
//		auxO3.setAttribute('required', '');
//		//auxO2.removeAttribute('required');
//		//auxiB.removeAttribute('required');
//		//auxiBT.style.display = "none";
//		//auxiAT.style.display = "none";
//		//aux8.style.display = "block";
//		//aux10.style.display = "none"
//	}
//	//else if (aux9.options[aux9.selectedIndex].value === "Fecha de creación") {
//	//    aux8.style.display = "none";
//	//    aux10.style.display = "none";
//	//    auxiB.setAttribute('required', '');
//	//    auxiA.setAttribute('required', '');
//	//    auxiB.style.display = "block";
//	//    auxiA.style.display = "block"
//	//    auxiBT.style.display = "block";
//	//    auxiAT.style.display = "block"
//	//} else {
//	//    auxiB.style.display = "none";
//	//    auxiA.style.display = "none";
//	//    auxiA.removeAttribute('required');
//	//    auxiB.removeAttribute('required');
//	//    aux8.style.display = "none";
//	//    auxiBT.style.display = "none";
//	//    auxiAT.style.display = "none";
//	//    aux10.style.display = "block"
//	//}
//}
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

function solonumeros(e) {
	key = e.keyCode || e.which;
	teclado = String.fromCharCode(key).toUpperCase();
	numeros = "1234567890";
	especiales = "8-37-38-46-164";
	teclado_especial = false;
	for (var i in especiales) {
		if (key == especiales[i]) {
			teclado_especial = true; break;
		}
	}
	if (numeros.indexOf(teclado) == -1 && !teclado_especial) {
		return false;
	}
}

function sololetrasnumerossimbolos(e) {
	if (document.getElementById("Mayuscula") != null) {
		var Mayuscula = document.getElementById("Mayuscula");
		Mayuscula.addEventListener("input", function (event) {
			this.value = this.value.toUpperCase();
		});
	}
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
$('#modalBusquedaActaDecomiso').on('show.bs.modal', function (e) {
	var bookId = $(e.relatedTarget).data('book-id');
	document.getElementsByName("agregarActaDesomiso")[0].setAttribute("id", bookId);
});
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
if (document.getElementById("InventarioSeleccion") != null) {
	document.getElementById("InventarioSeleccion").addEventListener("change", function () {
		var tipoActa = document.getElementById("InventarioSeleccion")
		var tipoSeleccion = tipoActa.value;
		if (tipoSeleccion == 2) {
			document.getElementById("decomisoLayout").style.display = 'none';
			document.getElementById("referenciaArticulos").style.display = 'none';
			document.getElementById("NumInventario").style.display = 'block';

		} else {
			document.getElementById("decomisoLayout").style.display = 'block';
			document.getElementById("referenciaArticulos").style.display = 'block';
			document.getElementById("NumInventario").style.display = 'none';
		}
	});
}

