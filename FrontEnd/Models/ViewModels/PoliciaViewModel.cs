using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FrontEnd.Models.ViewModels
{
    public class PoliciaViewModel
    {
        public int IdPolicia { get; set; }
        [Required]
        [StringLength(25)]
        [Display(Name = "Cédula")]
        public string Cedula { get; set; }
        [Required]
        [Display(Name = "Tipo de Cédula")]
        public int TipoCedula { get; set; }
        [Required]
        [StringLength(150)]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Nacimiento")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Fecha_nacimiento { get; set; }
        [Required]
        [StringLength(100)]
        [EmailAddress]
        [Display(Name = "Correo Electrónico")]
        public string CorreoElectronico { get; set; }
        [Required]
        [StringLength(250)]
        [Display(Name = "Dirección")]
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
        [Display(Name = "Contacto de Emergencia")]
        public string ContactoEmergencia { get; set; }
        [Required]
        [StringLength(9)]
        [Display(Name = "Teléfono de Emergencia")]
        public string TelefonoEmergencia { get; set; }

        public int Estado { get; set; }
    }
}