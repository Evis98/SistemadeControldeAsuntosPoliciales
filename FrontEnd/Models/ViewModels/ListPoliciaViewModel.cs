using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FrontEnd.Models.ViewModels
{
    
    public class ListPoliciaViewModel
    {
        public int IdPolicia { get; set; }
        public int Estado { get; set; }

        [Display(Name = "Número de Cédula")]
        public string Cedula { get; set; }

        [Display(Name = "Tipo de Cédula")]
        public string TipoCedula { get; set; }

        [Display(Name = "Nombre Completo")]
        public string Nombre { get; set; }

        [Display(Name = "Fecha de Nacimiento")]
        public string FechaNacimiento { get; set; }

        [Display(Name = "Correo Electrónico")]
        public string CorreoElectronico { get; set; }

        [Display(Name = "Dirección Exacta")]
        public string Direccion { get; set; }

        [Display(Name = "Teléfono Celular")]
        public string TelefonoCelular { get; set; }

        [Display(Name = "Teléfono de Casa")]
        public string TelefonoCasa { get; set; }

        [Display(Name = "Nombre del Contacto de Emergencia")]
        public string ContactoEmergencia { get; set; }

        [Display(Name = "Teléfono del Contacto de Emergencia")]
        public string TelefonoEmergencia { get; set; }

        [Display(Name = "Estado")]
        public string DescripcionEstado { get; set; }

        [Display(Name = "Edad")]
        public string Edad { get; set; }



    }
}