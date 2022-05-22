using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FrontEnd.Models.ViewModels
{
    public class ActaDeDestruccionDePerecederosViewModel : AuditoriaViewModel
    {

        public int IdActaDeDestruccionDePerecederos { get; set; }

        public int IdActaDecomiso { get; set; }

        public int IdActaHallazgo { get; set; }

        [Display(Name = "Número de Acta Referenciada")]
        public string NumeroActaLigada { get; set; }

        [Display(Name = "Número de Folio")]
        [StringLength(30)]
        public string NumeroFolio { get; set; }



        [Display(Name = "Nombre del Encargado")]
        [Required]
        public string Encargado { get; set; }
        [Display(Name = "Encargado")]
        public string VistaPoliciaEncargado { get; set; }



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

        [Display(Name = "Tipo Acta")]
        [Required]
        public int TipoActaD { get; set; }
        [Display(Name = "Tipo Acta")]
        public string VistaTipoActaD { get; set; }

        [Display(Name = "Consecutivo Acta Decomiso")]
        [StringLength(50)]
        public string ConsecutivoActaDecomiso { get; set; }

        [Display(Name = "Consecutivo Acta Hallazgo")]
        [StringLength(50)]
        public string ConsecutivoActaHallazgo { get; set; }

        public string InventarioDestruido { get; set; }
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

        public IEnumerable<SelectListItem> TiposActaD { get; set; }

        public IEnumerable<SelectListItem> TiposTestigo { get; set; }
    }
}