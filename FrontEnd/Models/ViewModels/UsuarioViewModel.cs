﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FrontEnd.Models.ViewModels
{
    public class UsuarioViewModel : AuditoriaViewModel
    { 
        public int IdUsuario { get; set; }
        [Required]
        [Display(Name = "Nombre de Usuario")]
        public string Nombre { get; set; }
        [Display(Name = "Error")]
        public string LoginErrorMessage { get;  set; }
        [StringLength(100, ErrorMessage = "Correo electrónico excede los 100 caracteres.")]
        [EmailAddress(ErrorMessage = "Escriba una dirección de correo válida.")]
        [Display(Name = "Identificador (Correo Electrónico)")]
        [Required]
        public string UserName { get;  set; }
        [Display(Name = "Contraseña")]
        public string Password { get; set; }
        [Required]
        [Display(Name = "Cédula")]
        public string Cedula { get; set; }
        [Required]
        [Display(Name = "Rol del Usuario")]
        public int Rol { get; set; }
        [Display(Name = "Rol del Usuario")]
        public string VsitaRol { get; set; }
        public int IdAuditoria { get; set; }
        public int IdCategoria { get; set; }
        public string VistaCategoria { get; set; }
        public int IdElemento { get; set; }
        public string VistaElemento { get; set; }
        public DateTime Fecha { get; set; }
        public int Accion { get; set; }
        public string VistaAccion { get; set; }
        public int IdUsuarioAuditoria { get; set; }
        public string VistaUsuario { get; set; }
        public string CedulaUsuarioFiltrada { get; set; }

        public IEnumerable<SelectListItem> TiposDeRol { get; set; }
    }

}