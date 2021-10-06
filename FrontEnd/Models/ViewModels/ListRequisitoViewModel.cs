using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FrontEnd.Models.ViewModels
{
    public class ListRequisitoViewModel
    {
        public int IdRequisito { get; set; }
        public DateTime Fecha_Vencimiento { get; set; }
        public int TipoRequisito { get; set; }
        public int IdPolicia { get; set; }
        public string Imagen { get; set; }
        public string Detalles { get; set; }
    }
}