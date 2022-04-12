using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace FrontEnd.Models.ViewModels
{
    public class ActaDecomisoViewModel
    {

        public int IdActaDecomiso { get; set; }

        [Display(Name = "Número de Folio")]
        [StringLength(30)]
        public string NumeroFolio { get; set; }        

        [Required]
        [DataType(DataType.Date)]
        [DateValidationDecomiso(ErrorMessage = "Fecha ingresada invalida")]
        [Display(Name = "Fecha del Suceso")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "Hora del Suceso")]
        public DateTime Hora { get; set; }

        [Required]
        [StringLength(150, ErrorMessage = "Nombre excede los 150 caracteres.")]
        [Display(Name = "Nombre de a quién se le decomisa")]
        public string NombreDecomisado { get; set; }

        [Required]
        [StringLength(25, ErrorMessage = "Número de cédula excede los 25 caracteres.")]
        [Display(Name = "Número de Cédula")]
        public string CedulaDecomisado { get; set; }

        [Required]
        [StringLength(9, ErrorMessage = "Escriba un número de teléfono con 8 digitos.", MinimumLength = 9)]
        [Display(Name = "Teléfono")]
        public string TelefonoDecomisado { get; set; }

        [Required]
        [Display(Name = "Estado Civil")]
        public int EstadoCivil { get; set; }
        [Display(Name = "Estado Civil")]
        public string VistaTipoEstadoCivil { get; set; }

        [Required]
        [StringLength(250, ErrorMessage = "Dirección exacta excede los 250 caracteres.")]
        [Display(Name = "Dirección")]
        public string DireccionDecomisado { get; set; }

        [Required]
        [StringLength(250, ErrorMessage = "Dirección exacta excede los 250 caracteres.")]
        [Display(Name = "Lugar del procedimiento")]
        public string LugarDelProcedimiento { get; set; }

        [Display(Name = "Inventario")]
        [Required]
        [StringLength(5000)]
        public string Inventario { get; set; }

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
        [Display(Name = "Oficial Acompalante")]
        public string VistaOficialAcompanante { get; set; }
        [Display(Name = "Supervisor")]
        [Required]
        public string Supervisor { get; set; }
        [Display(Name = "Supervisor")]
        public string VistaPoliciaSupervisor { get; set; }

        public IEnumerable<SelectListItem> EstadosCivil { get; set; }

    }
    public class DateValidationDecomiso : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime todayDate = Convert.ToDateTime(value);
            return todayDate <= DateTime.Now;
        }
    }
}