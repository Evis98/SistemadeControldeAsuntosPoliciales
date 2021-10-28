using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FrontEnd.Models.ViewModels
{
    public class ListArmaViewModel
    {
        public int IdArma { get; set; }
        
        public int? PoliciaAsignado { get; set; }
        [Display(Name = "Número de Serie")]
        public string NumeroSerie { get; set; }
        [Display(Name = "Tipo")]
        public string TipoArma { get; set; }
        [Display(Name = "Marca")]
        public string Marca { get; set; }
        [Display(Name = "Calibre")]
        public string Calibre { get; set; }
        [Display(Name = "Condición")]
        public string Condicion { get; set; }
        [Display(Name = "Ubicación")]
        public string Ubicacion { get; set; }
        [Display(Name = "Observaciones")]
        public string Observacion { get; set; }
        [Display(Name = "Estado ")]
        public string EstadoArma { get; set; }
        [Display(Name = "Modelo ")]
        public string Modelo { get; set; }
        [Display(Name = "Policía Asignado")]
        public string NombrePolicia { get; set; }

    }
}