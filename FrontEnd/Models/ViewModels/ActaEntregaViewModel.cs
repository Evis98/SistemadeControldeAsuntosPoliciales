using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;



namespace FrontEnd.Models.ViewModels
{
    public class ActaEntregaViewModel : AuditoriaViewModel
    {

        public int IdActaEntrega { get; set; }

        [Display(Name = "Número de Folio")]
        [StringLength(30)]
        public string NumeroFolio { get; set; }

        [Display(Name = "Nombre del Encargado")]
        [Required]
        public string Encargado { get; set; }
        [Display(Name = "Encargado")]
        public string VistaPoliciaEncargado { get; set; }
        [Display(Name = "Tipo Testigo")]
        public int? TipoTestigo { get; set; }
        [Display(Name = "Testigo Persona (Opcional)")]
        public string TestigoPersona { get; set; }
        [Display(Name = "Testigo Persona")]
        public string VistaTestigoPersona { get; set; }
        [Display(Name = "Testigo Policía (Opcional)")]
        public string TestigoPolicia { get; set; }
        [Display(Name = "Testigo Policía")]
        public string VistaTestigoPolicia { get; set; }
        [Display(Name = "Supervisor")]
        [Required]
        public string Supervisor { get; set; }
        [Display(Name = "Supervisor")]
        public string VistaPoliciaSupervisor { get; set; }


        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha del Suceso")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }
        [Required]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "Hora del Suceso")]
        public DateTime Hora { get; set; }

        [Display(Name = "Razon Social (Opcional)")]

        public string RazonSocial { get; set; }
        [Display(Name = "Razon Social")]
        public string VistaRazonSocial { get; set; }


        [Display(Name = "Tipo Acta")]
        [Required]
        public int TipoActa { get; set; }
        [Display(Name = "Tipo Acta")]
        public string VistaTipoActa { get; set; }

        [Display(Name = "Consecutivo Acta Decomiso")]
        [StringLength(50)]
        public string ConsecutivoActaDecomiso { get; set; }

        [Display(Name = "Consecutivo Acta Hallazgo")]
        [StringLength(50)]
        public string ConsecutivoActaHallazgo { get; set; }

        [Display(Name = "A Nombre De")]
        public string NombreDe { get; set; }


        [Display(Name = "Recibe")]
        [Required]

        public string Recibe { get; set; }
        [Display(Name = "Recibe")]
        public string VistaRecibe { get; set; }
        [Display(Name = "Inventario Entregado")]
        [Required]
        [StringLength(1000)]
        public string InventarioEntregado { get; set; }
        public int Estado { get; set; }
        [Display(Name = " Estado del Acta")]

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

        public string VistaEstadoActa { get; set; }
        public IEnumerable<SelectListItem> Estados { get; set; }
        public IEnumerable<SelectListItem> TiposActa { get; set; }
        public IEnumerable<SelectListItem> TiposTestigo { get; set; }

    }
}