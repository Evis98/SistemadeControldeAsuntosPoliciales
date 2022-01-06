using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FrontEnd.Models.ViewModels
{
    public class ListRequisitoViewModel
    {
        public int IdRequisito { get; set; }
        [Display(Name = "Fecha de Vencimiento")]
        public string FechaVencimiento { get; set; }
        [Display(Name = "Tipo de Requisito")]
        public string TipoRequisito { get; set; }
        public string DetalleTipoRequisito { get; set; }
        public int? IdPolicia { get; set; }
        [Display(Name = "POLICÍA")]
        public string NombrePolicia { get; set; }
        public string Imagen { get; set; }
        public string Detalles { get; set; }
        public List<String> ListaTipoRequisitos { get; set; }
    }
}