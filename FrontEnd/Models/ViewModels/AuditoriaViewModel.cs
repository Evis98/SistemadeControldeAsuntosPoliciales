using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FrontEnd.Models.ViewModels
{
    public  class AuditoriaViewModel 
    {
        int IdAuditoria { get; set; }        
        int IdCategoria { get; set; }
        string VistaCategoria { get; set; }
        int IdElemento { get; set; }
        string VistaElemento { get; set; }
        DateTime Fecha { get; set;  }
        int Accion { get; set; }
        string VistaAccion { get; set; }
        int IdUsuario { get; set; }
        string VistaUsuario { get; set; }
        string Justificacion { get; set; }
  
    }
}