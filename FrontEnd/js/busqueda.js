var subjectObject = {
    "Detalle de Requisito": {
    },
    "Tipo de Requisito": { "Carné": [], "Capacitacion": [] }
}
var variosInput = document.getElementById('search');
var topicInput = document.getElementById('tipoRequisito');

window.onload = function () {
    document.getElementById("tipoRequisito").style.display = "none";
    var subjectSel = document.getElementById("searchBy");
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
    var aux2 = document.getElementById("searchBy");
    var aux3 = document.getElementById('search');
    if (aux2.options[aux2.selectedIndex].value === "Tipo de Requisito") {

        aux.style.display = "block";
        aux3.style.display = "none"
    } else {

        aux.style.display = "none";
        aux3.style.display = "block"
    }

}

