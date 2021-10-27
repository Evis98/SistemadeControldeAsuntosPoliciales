using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FrontEnd.Models.ViewModels
{
    public class PoliciaViewModel
    {
        public int IdPolicia { get; set; }
        [Required]
        [StringLength(25)]
        [Display(Name = "Número de Cédula")]
        public string Cedula { get; set; }
        [Required]
        [Display(Name = "Tipo de Cédula")]
        public int TipoCedula { get; set; }
        [Required]
        [StringLength(150)]
        [Display(Name = "Nombre Completo")]
        public string Nombre { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Nacimiento")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaNacimiento { get; set; }
        [Required]
        [StringLength(100)]
        [EmailAddress]
        [Display(Name = "Correo Electrónico")]
        public string CorreoElectronico { get; set; }
        [Required]
        [StringLength(250)]
        [Display(Name = "Dirección Exacta")]
        public string Direccion { get; set; }
        [Required]
        [StringLength(9)]
        [Display(Name = "Teléfono Celular")]
        public string TelefonoCelular { get; set; }
        [Required]
        [StringLength(9)]
        [Display(Name = "Teléfono de Casa")]
        public string TelefonoCasa { get; set; }
        [Required]
        [StringLength(150)]
        [Display(Name = "Nombre del Contacto de Emergencia")]
        public string ContactoEmergencia { get; set; }
        [Required]
        [StringLength(9)]
        [Display(Name = "Teléfono del Contacto de Emergencia")]
        public string TelefonoEmergencia { get; set; }
        public IEnumerable<SelectListItem> TiposCedula { get; set; }
        public int? Estado { get; set; }
    }
}