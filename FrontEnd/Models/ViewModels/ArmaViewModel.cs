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
        [Required]
        [Display(Name = "Policía Asignado")]
        public int? PoliciaAsignado { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "Número de Serie excede los 20 caracteres.")]
        [Display(Name = "Número de Serie")]
        public string NumeroSerie { get; set; }
        [Required]
        [Display(Name = "Tipo de Arma")]
        public int TipoArma { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Marca excede los 50 caracteres.")]
        [Display(Name = "Marca del Arma")]
        public string Marca { get; set; }
        [Required]
        [Display(Name = "Calibre del Arma")]
        public int Calibre { get; set; }
        [Required]
        [Display(Name = "Condición del Arma")]
        public int Condicion { get; set; }
        [Required]
        [Display(Name = "Ubicación del Arma")]
        public int Ubicacion { get; set; }
        
        [StringLength(150, ErrorMessage = "Observaciones exceden los 150 caracteres.")]
        [Display(Name = "Observaciones")]
        public string Observacion { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Modelo excede los 50 caracteres.")]
        [Display(Name = " Modelo del Arma")]
        public string ModeloArma { get; set; }
        public IEnumerable<SelectListItem> TiposArma { get; set; }
        public IEnumerable<SelectListItem> TiposCalibre { get; set; }
        public IEnumerable<SelectListItem> TiposCondicion { get; set; }
        public IEnumerable<SelectListItem> TiposUbicacion { get; set; }
        public IEnumerable<SelectListItem> ListaPolicias { get; set; }
        public string SerieFiltrada { get; set;}
        public int EstadoArma { get; set; }
    }
}