function ocultarRequisito() {
 
    var aux1 = document.getElementById("tipoRequisito");
    var aux2 = document.getElementById("filtroSeleccionado");
    var aux3 = document.getElementById('busqueda');
    if (aux2.options[aux2.selectedIndex].textContent === "Tipo de Requisito") {
        aux1.style.display = "block";
        aux3.style.display = "none"
    } else {
        aux1.style.display = "none";
        aux3.style.display = "block"
    }
}
function ocultarFechaAuditoriaPolicia() {
    var auxaP = document.getElementById("busquedaFechaInicioP");
    var auxbP = document.getElementById("busquedaFechaFinalP");
    var auxcP = document.getElementById("filtrosSeleccionado");
    var auxFITP = document.getElementById("textFechaInicioP");
    var auxFFTP = document.getElementById("textFechaFinalP");
    var auxBFTP = document.getElementById("tiposAccion");
    if (auxcP.options[auxcP.selectedIndex].textContent === "Fecha") {
        auxaP.setAttribute('required', '');
        auxbP.setAttribute('required', '');
        auxFITP.style.display = "block";
        auxFFTP.style.display = "block";
        auxaP.style.display = "block";
        auxbP.style.display = "block";
        auxBFTP.style.display = "none";
    }
    else {
        auxaP.removeAttribute('required');
        auxbP.removeAttribute('required');
        auxaP.style.display = "none";
        auxbP.style.display = "none";
        auxFFTP.style.display = "none";
        auxFITP.style.display = "none";
        auxBFTP.style.display = "block";

    }
}

function ocultarFechaParte() {
    var auxQ = document.getElementById("busquedaFechaInicioP");
    var auxW = document.getElementById("busquedaFechaFinalP");
    var auxc = document.getElementById("filtroSeleccionado");
    var auxFIT = document.getElementById("textFechaInicioP");
    var auxFFT = document.getElementById("textFechaFinalP");
    var auxd = document.getElementById('busqueda');
    if (auxc.options[auxc.selectedIndex].value === "Fecha") {
        auxQ.setAttribute('required', '');
        auxW.setAttribute('required', '');
        auxFIT.style.display = "block";
        auxFFT.style.display = "block";
        auxQ.style.display = "block";
        auxW.style.display = "block";
        auxd.style.display = "none"
    } else {
        auxQ.removeAttribute('required');
        auxW.removeAttribute('required');
        auxQ.style.display = "none";
        auxW.style.display = "none";
        auxFFT.style.display = "none";
        auxFIT.style.display = "none";
        auxd.style.display = "block"
    }
}



function ocultarFechaActa() {
    var auxa = document.getElementById("busquedaFechaInicioH");
    var auxb = document.getElementById("busquedaFechaFinalH");
    var auxc = document.getElementById("filtroSeleccionado");
    var auxFIT = document.getElementById("textFechaInicioH");
    var auxFFT = document.getElementById("textFechaFinalH");
    var auxd = document.getElementById('busqueda');
    if (auxc.options[auxc.selectedIndex].textContent === "Fecha") {
        auxa.setAttribute('required', '');
        auxb.setAttribute('required', '');
        auxFIT.style.display = "block";
        auxFFT.style.display = "block";
        auxa.style.display = "block";
        auxb.style.display = "block";
        auxd.style.display = "none"
    }
    else {
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
    if (aux9.options[aux9.selectedIndex].value === "Estado de Bitácora") {
        auxiB.style.display = "none";
        auxiA.style.display = "none";
        auxiA.removeAttribute('required');
        auxiB.removeAttribute('required');
        auxiBT.style.display = "none";
        auxiAT.style.display = "none";
        aux8.style.display = "block";
        aux10.style.display = "none"
    } 
    else if (aux9.options[aux9.selectedIndex].value === "Fecha de Creación") {
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
