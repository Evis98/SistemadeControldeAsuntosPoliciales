using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FrontEnd.Models.ViewModels
{
    public class UsuarioViewModel
    { 
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        [Display(Name = "Error")]
        public string LoginErrorMessage { get;  set; }
        [Display(Name = "Usuario")]
        [Required]
        public string UserName { get;  set; }
        [Required]
        [Display(Name = "Contraseña")]
        public string Password { get;  set; }
    }

}