var subjectObject = {
    "Detalle de Requisito": { },
    "Tipo de Requisito": { "Carné": [], "Capacitación": [] }
}
var variosInput = document.getElementById('busqueda');
var topicInput = document.getElementById('tipoRequisito');

window.onload = function () {

    document.getElementById("tipoRequisito").style.display = "none";
  
    var subjectSel = document.getElementById("filtroSeleccionado");
    var topicSel = document.getElementById("tipoRequisito");
    for (var x in subjectObject) {
        subjectSel.options[subjectSel.options.length] = new Option(x, x);
    }
    subjectSel.onchange = function () {
       topicSel.length = 1;
        for (var y in subjectObject[this.value]) {
            topicSel.options[topicSel.options.length] = new Option(y, y);
        }
    }
      

}

function ocultarRequisito() {
 
    var aux1 = document.getElementById("tipoRequisito");
    var aux2 = document.getElementById("filtroSeleccionado");
    var aux3 = document.getElementById('busqueda');
    if (aux2.options[aux2.selectedIndex].value === "Tipo de Requisito") {
        aux1.style.display = "block";
        aux3.style.display = "none"
    } else {
        aux1.style.display = "none";
        aux3.style.display = "block"
    }
}

function ocultarBitacora() {

    var aux8 = document.getElementById("estadoActualBitacora");
    var aux9 = document.getElementById("filtroSeleccionado");
    var aux10 = document.getElementById('busqueda');
    if (aux9.options[aux9.selectedIndex].value === "Estado de bitácora") {
        aux8.style.display = "block";
        aux10.style.display = "none"
    } else {
        aux8.style.display = "none";
        aux10.style.display = "block"
    }
}
function ocultarFechaNacimiento() {
    var aux4 = document.getElementById("busquedaFechaInicio");
    var aux7 = document.getElementById("busquedaFechaFinal");
    var aux5 = document.getElementById("filtroSeleccionado");
    var aux6 = document.getElementById('busqueda');
   
    if (aux5.options[aux5.selectedIndex].value === "Fecha Nacimiento") {
        aux4.style.display = "block";
        aux7.style.display = "block";
        aux6.style.display = "none"
    } else {
        aux4.style.display = "none";
        aux7.style.display = "none";
        aux6.style.display = "block"
    }
}

function ocultarFecha() {
    var aux2 = document.getElementById("tiposRequisito");
    var aux3 = document.getElementById("fechaVencimiento");
    if (aux2.val() == "Capacitación") {
        aux3.style.display = "block"
    } else {
        aux3.style.display = "none"
    }
}

uploadField.onchange = function () {
    if (this.files[0].size > 4194304) {
        error.textContent = "El peso de la imagen es mayor a 4 MB."
        error.style.color = "red"
        this.value = "";
    } else {
        error.textContent = ""
    }
}
uploadField2.onchange = function () {
    if (this.files[0].size > 4194304) {
        error.textContent = "El peso de la imagen es mayor a 4 MB."
        error.style.color = "red"
        this.value = "";
    } else {
        error.textContent = ""
    }
}
