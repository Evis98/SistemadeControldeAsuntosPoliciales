using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FrontEnd.Models.ViewModels
{
    public class ActaDeObservacionPolicialViewModel
    {
        public int IdActaDeObservacionPolicial { get; set; }

        [Display(Name = "Número de Folio")]
        [StringLength(30)]
        public string NumeroFolio { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DateValidationObservacion(ErrorMessage = "Fecha ingresada invalida")]
        [Display(Name = "Fecha del Suceso")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "Hora del Suceso")]
        public DateTime Hora { get; set; }

        [Required]
        [Display(Name = "Persona Interesada")]
        public string  IdInteresado { get; set; }

        [Display(Name = "Persona Interesada")]
        public string VistaPersonaInteresada { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Condición excede los 150 caracteres.")]
        [Display(Name = "En su condición de")]
        public string CondicionInteresado { get; set; }

        [Required]
        [Display(Name = "Distrito")]
        public int Distrito { get; set; }
        [Display(Name = "Distrito")]
        public string VistaTipoDistrito { get; set; }

        [Required]
        [StringLength(250, ErrorMessage = "Otras Señas exacta excede los 250 caracteres.")]
        [Display(Name = "Dirección")]
        public string Direccion { get; set; }

        [Display(Name = "Observaciones")]

        [StringLength(5000)]
        public string Observaciones { get; set; }

        [Display(Name = "Oficial Actuante")]
        [Required]
        public string OficialActuante { get; set; }
        [Display(Name = "Oficial Actuante")]
        public string VistaOficialActuante { get; set; }

        [Display(Name = "Oficial Acompañante")]
        [Required]
        public string OficialAcompanante { get; set; }
        [Display(Name = "Oficial Acompañante")]
        public string VistaOficialAcompanante { get; set; }
        public IEnumerable<SelectListItem> Distritos { get; set; }

    }
}
    public class DateValidationObservacion : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime todayDate = Convert.ToDateTime(value);
            return todayDate <= DateTime.Now;
        }
    }
