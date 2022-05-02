using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;



namespace FrontEnd.Models.ViewModels
{
    public class ActaEntregaViewModel
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
        [Display(Name = "Testigo")]
        [Required]
        public string Testigo { get; set; }
        [Display(Name = "Testigo")]
        public string VistaPoliciaTestigo { get; set; }
        [Display(Name = "Supervisor")]
        [Required]
        public string Supervisor { get; set; }
        [Display(Name = "Supervisor")]
        public string VistaPoliciaSupervisor { get; set; }

        [Display(Name = "Instalaciones")]
        [Required]
        public string Instalaciones { get; set; }

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

        [Display(Name = "Razon Social")]
        [Required]
        [StringLength(60)]
        public string RazonSocial { get; set; }
        [Display(Name = "Cedula Juridica")]
        [Required]
        [StringLength(60)]
        public string CedulaJuridica { get; set; }

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
        [Required]
        [StringLength(150)]
        public string NombreDe { get; set; }

        [Display(Name = "Identificacion Entregado")]
        [StringLength(25)]
        public string IdentificacionEntregado { get; set; }

        [Display(Name = "Recibe")]
        [Required]
        [StringLength(150)]
        public string Recibe { get; set; }
        [Display(Name = "Cedula Recibe")]
        [Required]
        [StringLength(60)]
        public string CedulaRecibe { get; set; }
        [Display(Name = "Inventario Entregado")]
        [Required]
        [StringLength(1000)]
        public string InventarioEntregado { get; set; }

        public IEnumerable<SelectListItem> TiposActa { get; set; }

    }
}