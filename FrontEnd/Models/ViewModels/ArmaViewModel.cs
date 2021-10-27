using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FrontEnd.Models.ViewModels
{
    public class ArmaViewModel
    {
        public int IdArma { get; set; }
        [Display(Name = "Policía Asignado")]
        public int? PoliciaAsignado { get; set; }
        [Required]
        [StringLength(20)]
        [Display(Name = "Número de serie")]
        public string NumeroSerie { get; set; }
        [Required]
        [Display(Name = "Tipo de arma")]
        public int TipoArma { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Marca del arma")]
        public string Marca { get; set; }
        [Required]
        [Display(Name = "Calibre del arma")]
        public int Calibre { get; set; }
        [Required]
        [Display(Name = "Condicíon del arma")]
        public int Condicion { get; set; }
        [Required]
        [Display(Name = "Ubicación del arma")]
        public int Ubicacion { get; set; }
        [Required]
        [StringLength(100)]
        [Display(Name = "Observaciones")]
        public string Observacion { get; set; }
        [Required]
        [Display(Name = " Modelo del arma")]
        public string ModeloArma { get; set; }
        public IEnumerable<SelectListItem> TiposArma { get; set; }
        public IEnumerable<SelectListItem> TiposCalibre { get; set; }
        public IEnumerable<SelectListItem> TiposCondicion { get; set; }
        public IEnumerable<SelectListItem> TiposUbicacion { get; set; }
        public IEnumerable<SelectListItem> ListaPolicias { get; set; }
        public int EstadoArma { get; set; }
    }
}