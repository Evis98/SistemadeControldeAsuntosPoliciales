
var subjectObject = {
    "Detalle de Requisito": {
    },
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
        //empty Chapters- and Topics- dropdowns

        topicSel.length = 1;
        //display correct values


        for (var y in subjectObject[this.value]) {
            topicSel.options[topicSel.options.length] = new Option(y, y);
        }
    }

}

function ocultar() {
    var aux = document.getElementById("tipoRequisito");
    var aux2 = document.getElementById("filtroSeleccionado");
    var aux3 = document.getElementById('busqueda');
    if (aux2.options[aux2.selectedIndex].value === "Tipo de Requisito") {
        aux.style.display = "block";
        aux3.style.display = "none"
    } else {
        aux.style.display = "none";
        aux3.style.display = "block"
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
