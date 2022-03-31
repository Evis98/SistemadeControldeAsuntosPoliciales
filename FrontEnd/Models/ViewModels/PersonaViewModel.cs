using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FrontEnd.Models.ViewModels
{
    public class PersonaViewModel
    {
        public int IdPersona { get; set; }
        [Required]
        [Display(Name = "Tipo de Identificación")]
        public int TipoIdentificacionPersona { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "Identificación excede los 20 caracteres")]
        [Display(Name = "Número de Identificación")]
        public string Identificacion { get; set; }
        public string IdentificacionPersonaFiltrada { get; set; }
        [Required]
        [StringLength(250, ErrorMessage = "Nombre excede los 250 caractéres")]
        [Display(Name = "Nombre")]
        public string NombrePersona { get; set; }
        [Display(Name = "Sexo")]
        public int SexoPersona { get; set; }
        [DataType(DataType.Date)]
        [DateValidationPersona(ErrorMessage = "Fecha ingresada invalida")]
        [Display(Name = "Fecha de Nacimiento")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaNacimientoPersona { get; set; }
        [StringLength(50, ErrorMessage = "Nacionalidad excede los 50 caracteres.")]
        [Required]
        [Display(Name = "Nacionalidad ")]
        public string NacionalidadPersona { get; set; }
        [StringLength(250, ErrorMessage = "Direccion exacta excede los 250 caracteres.")]
        [Display(Name = "Dirección Exacta (Opcional)")]
        public string DireccionExacta { get; set; }
        [StringLength(9, ErrorMessage = "Escriba un número de teléfono con 8 digitos.", MinimumLength = 9)]
        [Display(Name = "Teléfono de Casa (Opcional)")]
        public string TelefonoCasaPersona { get; set; }
        [StringLength(9, ErrorMessage = "Escriba un número de teléfono con 8 digitos.", MinimumLength = 9)]
        [Display(Name = "Teléfono Celular (Opcional)")]
        public string TelefonoCelularPersona { get; set; }
        [StringLength(9, ErrorMessage = "Escriba un número de teléfono con 8 digitos.", MinimumLength = 9)]
        [Display(Name = "Teléfono de Oficina (Opcional)")]
        public string TelefonoTrabajoPersona { get; set; }
        [StringLength(100, ErrorMessage = "Profesión excede la cantidad maxima de caractéres")]
        [Display(Name = "Profesión (Opcional)")]
        public string ProfesionPersona { get; set; }
        [StringLength(100, ErrorMessage = "Correo electrónico excede los 100 caracteres.")]
        [EmailAddress(ErrorMessage = "Escriba una dirección de correo válida.")]
        [Display(Name = "Correo Electrónico (Opcional)")]
        public string CorreoElectronicoPersona { get; set; }
        [StringLength(100, ErrorMessage = "Lugar de trabajo excede la cantidad maxima de caractéres")]
        [Display(Name = "Lugar de Trabajo (Opcional)")]
        public string LugarTrabajoPersona { get; set; }
        public IEnumerable<SelectListItem> TiposDeIdentificacion { get; set; }
        public IEnumerable<SelectListItem> TiposDeSexo { get; set; }
        public IEnumerable<SelectListItem> Nacionalidades { get; set; }
    }
    public class DateValidationPersona : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime todayDate = Convert.ToDateTime(value);
            return todayDate <= DateTime.Now;
        }
    }

}