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
function ocultarFechaActa() {
    var auxa = document.getElementById("busquedaFechaInicioH");
    var auxb = document.getElementById("busquedaFechaFinalH");
    var auxc = document.getElementById("filtroSeleccionado");
    var auxFIT = document.getElementById("textFechaInicioH");
    var auxFFT = document.getElementById("textFechaFinalH");
    var auxd = document.getElementById('busqueda');
    if (auxc.options[auxc.selectedIndex].value === "Fecha") {
        auxa.setAttribute('required', '');
        auxb.setAttribute('required', '');
        auxFFT.style.display = "none";
        auxFFT.style.display = "none";
        auxa.style.display = "block";
        auxb.style.display = "block";
        auxd.style.display = "none"
    } else {
        auxa.removeAttribute('required');
        auxb.removeAttribute('required');
        auxa.style.display = "none";       
        auxb.style.display = "none";
        auxFFT.style.display = "none";
        auxFIT.style.display = "none";
        auxd.style.display = "block"
    }
}

function ocultarBitacora() {
    var auxiB = document.getElementById("busquedaFechaInicioB");
    var auxiA = document.getElementById("busquedaFechaFinalB");
    var auxiBT = document.getElementById("textFechaInicio");
    var auxiAT = document.getElementById("textFechaFinal");
    var aux8 = document.getElementById("estadoBitacora");
    var aux9 = document.getElementById("filtroSeleccionado");
    var aux10 = document.getElementById('busqueda');
    if (aux9.options[aux9.selectedIndex].value === "Estado de bitácora") {
        auxiB.style.display = "none";
        auxiA.style.display = "none";
        auxiA.removeAttribute('required');
        auxiB.removeAttribute('required');
        auxiBT.style.display = "none";
        auxiAT.style.display = "none";
        aux8.style.display = "block";
        aux10.style.display = "none"
    } 
    else if (aux9.options[aux9.selectedIndex].value === "Fecha de creación") {
        aux8.style.display = "none";
        aux10.style.display = "none";
        auxiB.setAttribute('required', '');
        auxiA.setAttribute('required', '');
        auxiB.style.display = "block";
        auxiA.style.display = "block"
        auxiBT.style.display = "block";
        auxiAT.style.display = "block"
    } else {
        auxiB.style.display = "none";
        auxiA.style.display = "none";
        auxiA.removeAttribute('required');
        auxiB.removeAttribute('required');
        aux8.style.display = "none";
        auxiBT.style.display = "none";
        auxiAT.style.display = "none";
        aux10.style.display = "block"
    }
}
//function ocultarFechaNacimiento() {
//    var aux4 = document.getElementById("busquedaFechaInicio");
//    var aux7 = document.getElementById("busquedaFechaFinal");
//    var aux5 = document.getElementById("filtroSeleccionado");
//    var aux6 = document.getElementById('busqueda');

//    if (aux5.options[aux5.selectedIndex].value === "Fecha Nacimiento") {
//        aux4.style.display = "block";
//        aux4.setAttribute('required', '');
//        aux7.setAttribute('required', '');
//        aux7.style.display = "block";
//        aux6.style.display = "none"
//    } else {
//        aux4.style.display = "none";
//        aux4.removeAttribute('required');
//        aux7.removeAttribute('required');
//        aux7.style.display = "none";
//        aux6.style.display = "block"
//    }
//}

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
