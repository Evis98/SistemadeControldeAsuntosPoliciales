using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FrontEnd.Models.ViewModels
{

    public class ListPersonaViewModel
    {

        public int IdPersona { get; set; }

        [Display(Name = "Tipo de Identificación")]
        public string TipoIdentificacionPersona { get; set; }

        [Display(Name = "Número de Identificación")]
        public string IdentificacionPersona { get; set; }

        [Display(Name = "Nacionalidad")]
        public string NacionalidadPersona { get; set; }

        [Display(Name = "Nombre Completo")]
        public string NombrePersona { get; set; }

        [Display(Name = "Fecha de Nacimiento")]
        public string FechaNacimientoPersona { get; set; }

        [Display(Name = "Dirección Exacta")]
        public string DireccionExactaPersona { get; set; }

        [Display(Name = "Sexo")]
        public string Sexo { get; set; }

        [Display(Name = "Correo Electrónico")]
        public string CorreoElectronicoPersona { get; set; }

        [Display(Name = "Profesión u Oficio")]
        public string ProfesionUOficioPersona { get; set; }

        [Display(Name = "Lugar de Trabajo")]
        public string LugarTrabajoPersona { get; set; }

        [Display(Name = "Edad")]
        public string EdadPersona { get; set; }

        [Display(Name = "Teléfono celular")]
        public string TelefonoCelularPersona { get; set; }

        [Display(Name = "Teléfono celular")]
        public string TelefonoHabitacionPersona { get; set; }

        [Display(Name = "Teléfono de Trabajo")]
        public string TelefonoTrabajoPersona { get; set; }


    }
}