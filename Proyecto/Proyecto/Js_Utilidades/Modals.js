﻿
//LLamado de modales para los modulos

// -------- inicio Personas --------
function RegistrarPersona() {
    $("#contenido").load("/Personas/PersonaAdd");
} 
function ModificarPersona(id) { 
    $("#contenido").load("/Personas/PersonasUpdt/" + id);
} // -------- fin Personas --------


// -------- inicio notas rapidas --------
function CrearNotaRapida() {
    $("#contenido").load("/NotasRapidas/NotasRapidasAdd");
}
function ModificarNotaRapida(id) {
    $("#contenido").load("/NotasRapidas/NotasRapidasUpdt/" + id);
}
function ModificarPanelInformaivo() {
    $("#contenido").load("/NotasRapidas/PanelInformativoUpdt");
}

// -------- fin notas rapidas --------

// -------- inicio Cursos --------
function CrearCurso() {
    $("#contenido").load("/Cursos/CursosAdd");
}
function ModificarCurso(id) {
    $("#contenido").load("/Cursos/CursosUpdt/" + id);
}
// -------- fin notas rapidas --------

function VisualizarInforme() { 
    
    $('#MyModaInicio').modal('show');
    $("#ContentInicio").load("/Inicio/PanelInformativo");
   
}