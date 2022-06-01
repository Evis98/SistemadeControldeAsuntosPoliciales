using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FrontEnd.Models.ViewModels
{
    public class PoliciaViewModel  /*: AuditoriaViewModel*/
    {
        public int IdPolicia { get; set; }

        [Required]
        [StringLength(25, ErrorMessage = "Número de cédula excede los 25 caracteres.")]
        [Display(Name = "Número de Cédula")]
        public string Cedula { get; set; }
  
        [Required]
        [Display(Name = "Tipo de Cédula")]
        public int TipoCedula { get; set; }
        [Display(Name = "Tipo de Cédula")]
        public string VistaTipoCedula { get; set; }

        [Required]
        [StringLength(150, ErrorMessage = "Nombre excede los 150 caracteres.")]
        [Display(Name = "Nombre Completo")]
        public string Nombre { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Nacimiento")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaNacimiento { get; set; }
        [Display(Name = "Edad")]
        public string Edad { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Correo electrónico excede los 100 caracteres.")]
        [EmailAddress(ErrorMessage = "Escriba una dirección de correo válida.")]
        [Display(Name = "Correo Electrónico")]
        public string CorreoElectronico { get; set; }

        [Required]
        [StringLength(250, ErrorMessage = "Dirección exacta excede los 250 caracteres.")]
        [Display(Name = "Dirección Exacta")]
        public string Direccion { get; set; }

        [Required]
        [StringLength(9, ErrorMessage = "Escriba un número de teléfono con 8 digitos.", MinimumLength = 9)]
        [Display(Name = "Teléfono Celular")]
        public string TelefonoCelular { get; set; }

        [StringLength(9, ErrorMessage = "Escriba un número de teléfono con 8 digitos.", MinimumLength = 9)]
        [Display(Name = "Teléfono de Casa (Opcional)")]
        public string TelefonoCasa { get; set; }

        [Required]
        [StringLength(150, ErrorMessage = "Nombre excede los 150 caracteres.")]
        [Display(Name = "Nombre Contacto de Emergencia")]
        public string ContactoEmergencia { get; set; }

        [Required]
        [StringLength(9, ErrorMessage = "Escriba un número de teléfono con 8 digitos.", MinimumLength = 9)]
        [Display(Name = "Teléfono Contacto de Emergencia")]
        public string TelefonoEmergencia { get; set; }
        public int Estado { get; set; }
        [Display(Name = "Estado")]
        public string VistaEstado { get; set; }

        public IEnumerable<SelectListItem> TiposCedula { get; set; }
    }
}