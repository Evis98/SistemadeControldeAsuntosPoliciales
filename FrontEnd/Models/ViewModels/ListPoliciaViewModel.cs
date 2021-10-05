using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FrontEnd.Models.ViewModels
{
    public class ListPoliciaViewModel
    {
        public int IdPolicia { get; set; }
        public string Cedula { get; set; }
        public int TipoCedula { get; set; }
        public string Nombre { get; set; }
        public DateTime Fecha_nacimiento { get; set; }
        public string CorreoElectronico { get; set; }
        public string Direccion { get; set; }
        public string TelefonoCelular { get; set; }
        public string TelefonoCasa { get; set; }
        public string ContactoEmergencia { get; set; }
        public string TelefonoEmergencia { get; set; }
        public int Estado { get; set; }



    }
}