using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FrontEnd.Models.ViewModels
{
    public class ListArmaViewModel
    {
        public int IdArma { get; set; }
        public int PoliciaAsignado { get; set; }
        public string NumeroSerie { get; set; }
        public int TipoArma { get; set; }
        public string Marca { get; set; }
        public int Calibre { get; set; }
        public int Condicion { get; set; }
        public int Ubicacion { get; set; }
        public string Observacion { get; set; }
        public int EstadoArma { get; set; }
        public string Modelo { get; set; }
        public string NombrePolicia { get; set; }

    }
}