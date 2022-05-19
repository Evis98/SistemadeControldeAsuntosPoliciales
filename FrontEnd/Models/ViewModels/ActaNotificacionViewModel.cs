using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FrontEnd.Models.ViewModels
{
    public class ActaNotificacionViewModel : AuditoriaViewModel
    {
        public int IdActaDeNotificacion { get; set; }

        [Display(Name = "Número de Folio")]
        [StringLength(30)]
        public string NumeroFolio { get; set; }


        [Display(Name = "Cédula de la Persona Notificada")]
        [Required]
        public string Notificado { get; set; }
        [Display(Name = "Persona Notificada")]
        public string VistaPersonaNotificada { get; set; }

        [Display(Name = "Oficial Actuante")]
        [Required]
        public string Oficial { get; set; }
        [Display(Name = "Oficial Actuante")]
        public string VistaOficialActuante { get; set; }


        [Required]
        [Display(Name = "Tipo de Testigo")]
        public int TipoTestigo { get; set; }

        [Display(Name = "Tipo de Testigo")]
        public string VistaTipoTestigo { get; set; }


        [Display(Name = "Testigo")]
        public string IdTestigoPolicia { get; set; }

        [Display(Name = "Testigo")]
        public string IdTestigoPersona { get; set; }

        [Display(Name = "Cédula")]
        public string VistaIdTestigo { get; set; }

        [Display(Name = "Testigo")]
        public string VistaTestigo { get; set; }


        [Display(Name = "Distrito")]
        [Required]
        public int Distrito { get; set; }
        [Display(Name = "Distrito")]
        public string VistaDistrito { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha Procedimiento")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }
        [Required]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "Hora Procedimiento")]
        public DateTime Hora { get; set; }

        [Required]
        [StringLength(250, ErrorMessage = "Dirección excede los 250 caracteres.")]
        [Display(Name = "Dirección Exacta Procedimiento")]
        public string DireccionExactaProcedimiento { get; set; }

        [Display(Name = "Barrio y/o Caserío")]
        [Required]
        [StringLength(60)]
        public string Barrio { get; set; }

        [Display(Name = "Disposiciones")]
        [StringLength(5000)]
        public string Disposiciones { get; set; }
        public int Estado { get; set; }
        [Display(Name = " Estado del Acta")]
        public string VistaEstadoActa { get; set; }

        public int IdAuditoria { get; set; }
        public int IdCategoria { get; set; }
        public string VistaCategoria { get; set; }
        public int IdElemento { get; set; }
        public string VistaElemento { get; set; }
        public DateTime FechaAuditoria { get; set; }
        public int Accion { get; set; }
        public string VistaAccion { get; set; }
        public int IdUsuario { get; set; }
        public string VistaUsuario { get; set; }
        public IEnumerable<SelectListItem> Estados { get; set; }

        public IEnumerable<SelectListItem> Distritos { get; set; }
        public IEnumerable<SelectListItem> TiposTestigo { get; set; }

    }


    public class DateValidationNotificacion : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime todayDate = Convert.ToDateTime(value);
            return todayDate <= DateTime.Now;
        }
    }

}