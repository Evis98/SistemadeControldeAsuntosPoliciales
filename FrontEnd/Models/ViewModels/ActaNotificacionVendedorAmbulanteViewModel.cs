using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FrontEnd.Models.ViewModels
{
    public class ActaNotificacionVendedorAmbulanteViewModel : AuditoriaViewModel
    {
        public int IdActaNotificacionVendedorAmbulante { get; set; }

        [Display(Name = "Número de Folio")]
        [StringLength(30)]
        public string NumeroFolio { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DateValidationNotificacion(ErrorMessage = "Fecha ingresada invalida")]
        [Display(Name = "Fecha del Suceso")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "Hora del Suceso")]
        public DateTime Hora { get; set; }

        [Required]
        [Display(Name = "Persona Notificada")]
        public string IdNotificado { get; set; }

        [Display(Name = "Persona Notificada")]
        public string VistaPersonaNotificada { get; set; }

        [Required]
        [Display(Name = "Distrito")]
        public int Distrito { get; set; }
        [Display(Name = "Distrito")]
        public string VistaTipoDistrito { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Caserío excede los 50 caracteres.")]
        public string Caserio { get; set; }

        [Required]
        [Display(Name = "Tipo de Testigo")]
        public int TipoTestigo { get; set; }

       
        [Display(Name = "Tipo de Testigo")]
        public string VistaTipoTestigo { get; set; }

        [Display(Name = "Testigo")]
        public string IdTestigo { get; set; }

        [Display(Name = "Cédula")]
        public string VistaIdTestigo { get; set; }

        [Display(Name = "Testigo")]
        public string VistaTestigo { get; set; }
        
        [Required]
        [StringLength(250, ErrorMessage = "Dirección excede los 250 caracteres.")]
        [Display(Name = "Dirección del suceso(notificación)")]
        public string DireccionNotificacion { get; set; }

        [Required]
        [Display(Name = "Ejerce en")]

        public int FormaDeVenta { get; set; }


        [Display(Name = "Ejerce en")]
        public string VistaFormaDeVenta { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Actividad de venta excede los 100 caracteres.")]
        [Display(Name = "Actividad de venta")]
        public string ActividadVenta { get; set; }

        [StringLength(250, ErrorMessage = "Placa de vehículo excede los 30 caracteres.")]
        [Display(Name = "Placa de vehículo")]
        public string PlacaVehiculo { get; set; }

        [Required]
        [StringLength(250, ErrorMessage = "Dirección excede los 250 caracteres.")]
        [Display(Name = "Dirección de Persona Notificada")]
        public string DireccionNotificado { get; set; }

        [Display(Name = "Oficial Actuante")]
        [Required]
        public string OficialActuante { get; set; }
        [Display(Name = "Oficial Actuante")]
        public string VistaOficialActuante { get; set; }
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
        public IEnumerable<SelectListItem> FormasVenta { get; set; }
    }
}
public class DateValidationNotificacion : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        DateTime todayDate = Convert.ToDateTime(value);
        return todayDate <= DateTime.Now;
    }
}
