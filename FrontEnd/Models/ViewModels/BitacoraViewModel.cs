using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FrontEnd.Models.ViewModels
{
    public class BitacoraViewModel
    {
        public int IdBitacora { get; set; }
        public string NumeroConsecutivo { get; set; }
        public bool ArmaPoliciaAsignado { get; set; }

        [Required]
        [Display(Name = "Número de serie de Arma")]
        public string NumeroSerieArma { get; set; }

        [Required]
        [Display(Name = "Armero que entrega")]
        public string ArmeroProveedor { get; set; }
        [Display(Name = "Armero que entrega")]
        public string VistaArmeroProveedor { get; set; }
        [Display(Name = "Armero que recibe")]
        public string ArmeroReceptor { get; set; }
        [Required]
        [Display(Name = "Policía Solicitante")]
        public string PoliciaSolicitante { get; set; }
        [Display(Name = "Policía Solicitante")]
        public string VistaPoliciaSolicitante { get; set; }

        [Display(Name = "Condición del arma al entregarse")]
        public string CondicionInicial { get; set; }

        [Display(Name = "Condición del arma al devolverse")]
        public int CondicionFinal { get; set; }

        [Display(Name = "Fecha de solicitud")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaCreacion { get; set; }

        [Display(Name = "Fecha de devolución")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaFinalizacion { get; set; }

        [Required]
        [Display(Name = "Munición entregada con el arma")]
        public string MunicionEntregada { get; set; }

        [Display(Name = "Munición devuelta con el arma")]
        public string MunicionDevuelta { get; set; }

        [Required]
        [Display(Name = "Cargadores entregados con el arma")]
        public string CargadoresEntregados { get; set; }

        [Display(Name = "Cargadores devueltos con el arma")]
        public string CargadoresDevueltos { get; set; }

        [StringLength(250, ErrorMessage = "Las observaciones exceden los 250 caracteres.")]
        [Display(Name = "Observaciones de Entrega")]
        public string ObservacionesEntrega { get; set; }

        [StringLength(250, ErrorMessage = "Las observaciones exceden los 250 caracteres.")]
        [Display(Name = "Observaciones de Devolucion")]
        public string ObservacionesDevuelta { get; set; }

        [Display(Name = "Estado actual de bitácora")]
        public int EstadoActual { get; set; }
        [Display(Name = "Estado actual de bitácora")]
        public string VistaEstadoActual { get; set; }

        public IEnumerable<SelectListItem> TiposCondicion { get; set; }
        public string VistaArmeroReceptor { get; internal set; }
        public string VistaCondicionFinal { get; internal set; }
        public int IdArma { get; set; }

        [Display(Name = "Condición del arma al entregarse")]
        public string VistaCondicionInicial { get; set; }
    }
}