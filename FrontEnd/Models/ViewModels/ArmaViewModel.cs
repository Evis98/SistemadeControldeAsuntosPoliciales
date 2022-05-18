using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FrontEnd.Models.ViewModels
{
    public class ArmaViewModel: AuditoriaViewModel
    {
        public int IdArma { get; set; }
        public string SerieFiltrada { get; set; }

        [Display(Name = "Policía Asignado")]
        public string PoliciaAsignado { get; set; }
        [Display(Name = "Policía Asignado")]
        public string NombrePolicia { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Número de Serie excede los 20 caracteres.")]
        [Display(Name = "Número de Serie")]
        public string NumeroSerie { get; set; }

        [Required]
        [Display(Name = "Tipo de Arma")]
        public int TipoArma { get; set; }
        [Display(Name = "Tipo de Arma")]
        public string VistaTipoArma { get; set; }

        [Required]
        [Display(Name = "Marca del Arma")]
        public int Marca { get; set; }
        [Display(Name = "Marca del Arma")]
        public string VistaMarca { get; set; }

        [Required]
        [Display(Name = "Calibre del Arma")]
        public int Calibre { get; set; }
        [Display(Name = "Calibre del Arma")]
        public string VistaCalibre { get; set; }

        [Required]
        [Display(Name = "Condición del Arma")]
        public int Condicion { get; set; }
        [Display(Name = "Condición del Arma")]
        public string VistaCondicion { get; set; }

        [Required]
        [Display(Name = "Ubicación del Arma")]
        public int Ubicacion { get; set; }
        [Display(Name = "Ubicación del Arma")]
        public string VistaUbicacion { get; set; }

        [StringLength(150, ErrorMessage = "Observaciones exceden los 150 caracteres.")]
        [Display(Name = "Observaciones (Opcional)")]
        public string Observacion { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Modelo excede los 50 caracteres.")]
        [Display(Name = " Modelo del Arma")]
        public string ModeloArma { get; set; }

        public int EstadoArma { get; set; }
        [Display(Name = " Estado del Arma")]
        public string VistaEstadoArma { get; set; }

        public int IdAuditoria { get; set; }
        public int IdCategoria { get; set; }
        public string VistaCategoria { get; set; }
        public int IdElemento { get; set; }
        public string VistaElemento { get; set; }
        public DateTime Fecha { get; set; }
        public int Accion { get; set; }
        public string VistaAccion { get; set; }
        public int IdUsuario { get; set; }
        public string VistaUsuario { get; set; }

        [StringLength(150, ErrorMessage = "Justificación excede los 150 caracteres.")]
        public string Justificacion { get; set; }


        //ComboBoxes
        public IEnumerable<SelectListItem> TiposArma { get; set; }
        public IEnumerable<SelectListItem> TiposCalibre { get; set; }
        public IEnumerable<SelectListItem> TiposCondicion { get; set; }
        public IEnumerable<SelectListItem> TiposUbicacion { get; set; }
        public IEnumerable<SelectListItem> TiposMarcas { get; set; }
        public IEnumerable<SelectListItem> ListaPolicias { get; set; }
    }
}