

$('#modalBusquedaPolicia').on('show.bs.modal', function (e) {
	var bookId = $(e.relatedTarget).data('book-id');
	document.getElementsByName("agregarPolicia")[0].setAttribute("id", bookId);
});

$('#modalBusquedaPersona').on('show.bs.modal', function (e) {
	var bookId = $(e.relatedTarget).data('book-id');
	document.getElementsByName("agregarPersona")[0].setAttribute("id", bookId);
});


if (document.getElementById("idTipoTestigo") != null) {
	window.addEventListener("load", function () {
		var tipoRequisito = document.getElementById("idTipoTestigo")
		var tipoSeleccion = tipoRequisito.value;
		if (tipoSeleccion == 1) {
			document.getElementById("idOficialTestigoCampo").style.display = 'block';
			document.getElementById("idPersonaTestigoCampo").style.display = 'none';
		} else if (tipoSeleccion == 2) {
			document.getElementById("idOficialTestigoCampo").style.display = 'none';
			document.getElementById("idPersonaTestigoCampo").style.display = 'block';
		} else {
			document.getElementById("idOficialTestigoCampo").style.display = 'none';
			document.getElementById("idPersonaTestigoCampo").style.display = 'none';
		}
	})
	document.getElementById("idTipoTestigo").addEventListener("change", function () {
		var tipoRequisito = document.getElementById("idTipoTestigo")
		var tipoSeleccion = tipoRequisito.value;
		if (tipoSeleccion == 1) {
			document.getElementById("idOficialTestigoCampo").style.display = 'block';
			document.getElementById("idPersonaTestigoCampo").style.display = 'none';
		} else if (tipoSeleccion == 2) {
			document.getElementById("idOficialTestigoCampo").style.display = 'none';
			document.getElementById("idPersonaTestigoCampo").style.display = 'block';
		}
		else {
			document.getElementById("idOficialTestigoCampo").style.display = 'none';
			document.getElementById("idPersonaTestigoCampo").style.display = 'none';
		}
	});
}

if (document.getElementById("idFormaDeVenta") != null) {
	window.addEventListener("load", function () {
		var tipoRequisito = document.getElementById("idFormaDeVenta")
		var tipoSeleccion = tipoRequisito.value;
		if (tipoSeleccion == 1) {
			document.getElementById("placaVehiculo").style.display = 'block';
		}
	})
	document.getElementById("idFormaDeVenta").addEventListener("change", function () {
		var tipoRequisito = document.getElementById("idFormaDeVenta")
		var tipoSeleccion = tipoRequisito.value;
		if (tipoSeleccion == 1) {
			document.getElementById("placaVehiculo").style.display = 'block';
		} else {
			document.getElementById("placaVehiculo").style.display = 'none';
		}
	});
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



if (document.getElementById("tipoActaSeleccionado") != null) {
	window.addEventListener("load", function () {
		var tipoRequisito = document.getElementById("tipoActaSeleccionado")
		var tipoSeleccion = tipoRequisito.value;
		if (tipoSeleccion == 1) {
			document.getElementById("consecutivoActaHallazgo").style.display = 'block';
			document.getElementById("consecutivoActaDecomiso").style.display = 'none';
		} else if (tipoSeleccion == 2) {
			document.getElementById("consecutivoActaHallazgo").style.display = 'none';
			document.getElementById("consecutivoActaDecomiso").style.display = 'block';
		}
	});
}


