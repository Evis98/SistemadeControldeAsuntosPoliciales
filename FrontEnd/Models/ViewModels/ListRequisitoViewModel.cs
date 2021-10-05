using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FrontEnd.Models.ViewModels
{
    public class ListRequisitoViewModel
    {
        public int IdRequisito { get; set; }
        public DateTime fecha_Vencimiento { get; set; }
        public int TipoRequisito { get; set; }
        public int IdPolicia { get; set; }
        public string imagen { get; set; }
        public string detalles { get; set; }
    }
}