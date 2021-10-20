using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FrontEnd.Models.ViewModels
{
    public class ListArmaViewModel
    {
        public int IdArma { get; set; }
        public string PoliciaAsignado { get; set; }
        public string NumeroSerie { get; set; }
        public string TipoArma { get; set; }
        public string Marca { get; set; }
        public string Calibre { get; set; }
        public string Condicion { get; set; }
        public string Ubicacion { get; set; }
        public string Observacion { get; set; }
        public string EstadoArma { get; set; }
        public string Modelo { get; set; }
        public string NombrePolicia { get; set; }

    }
}