using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FrontEnd.Models.ViewModels
{
    public class InfractorViewModel 
    {
        public int IdInfractor { get; set; }

        [Required]
        [Display(Name = "Tipo de Identificación")]
        public int TipoIdentificacion { get; set; }
        [Display(Name = "Tipo de Identificación")]
        public string VistaTipoidentifiacion { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Identificación excede los 20 caracteres.")]
        [Display(Name = "Número de Identificación")]
        public string Identificacion { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Nacionalidad excede los 50 caracteres.")]
        [Display(Name = "Nacionalidad")]
        public string Nacionalidad { get; set; }
        [Display(Name = "Nacionalidad")]
        public string VistaNacionalidad { get; set; }

        [Required]
        [StringLength(150, ErrorMessage = "Nombre  excede los 150 caracteres.")]
        [Display(Name = "Nombre Completo")]
        public string Nombre { get; set; }
        
        [Required]
        [DataType(DataType.Date)]
        [DateValidation(ErrorMessage = "Fecha ingresada invalida")]
        [Display(Name = "Fecha de Nacimiento")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaNacimiento { get; set; }

        [Display(Name = "Edad")]
        public string Edad { get; set; }

        [StringLength(9, ErrorMessage = "Escriba un número de teléfono con 8 digitos.", MinimumLength = 9)]
        [Display(Name = "Teléfono (Opcional)")]
        public string Telefono { get; set; }

        [Required]
        [StringLength(250, ErrorMessage = "Direccion exacta excede los 250 caracteres.")]
        [Display(Name = "Dirección Exacta")]
        public string DireccionExacta { get; set; }

        [Required]
        [Display(Name = "Sexo")]
        public int Sexo { get; set; }
        [Display(Name = "Sexo")]
        public string VistaSexo { get; set; }

        [StringLength(100, ErrorMessage = "Dirección de correo excede los 100 caracteres.")]
        [EmailAddress(ErrorMessage = "Escriba una dirección de correo válida.")]
        [Display(Name = "Correo Electrónico (Opcional)")]
        public string CorreoElectronico { get; set; }

        [StringLength(150, ErrorMessage = "Observación excede los 150 caracteres.")]
        [Display(Name = "Observaciones (Opcional)")]
        public string Observaciones { get; set; }
       
        [StringLength(50, ErrorMessage = "Profesión excede los 50 caracteres.")]
        [Display(Name = "Profesión u Oficio (Opcional)")]
        public string ProfesionUOficio { get; set; }

        [Required]
        [StringLength(4, ErrorMessage = "Escriba la estatura en metros (Ejemplo: 1.60).", MinimumLength = 4)]
        [Display(Name = "Estatura")]
        public string Estatura { get; set; }

        [StringLength(150, ErrorMessage = "Tatuajes excede los 150 caracteres.")]
        [Display(Name = "Tatuajes (Opcional)")]
        public string Tatuajes { get; set; }

        [StringLength(150, ErrorMessage = "Nombre del Padre excede los 150 caracteres.")]
        [Display(Name = "Nombre del Padre (Opcional)")]
        public string NombrePadre { get; set; }

        [StringLength(150, ErrorMessage = "Nombre de la Madre excede los 150 caracteres.")]
        [Display(Name = "Nombre de la Madre (Opcional)")]
        public string NombreMadre { get; set; }

        [StringLength(150, ErrorMessage = "Conocido como excede los 150 caracteres.")]
        [Display(Name = "Conocido como (Opcional)")]
        public string ApodoInfractor { get; set; }

        [StringLength(150, ErrorMessage = "Rasgos Fisicos excede los 150 caracteres.")]
        [Display(Name = "Rasgos Fisicos (Opcional)")]
        public string RasgosFisicos { get; set; }

        [Display(Name = "Adjuntar imagen")]
        public HttpPostedFileBase Archivo { get; set; }

        public string Imagen { get; set; }
        public string LugarTrabajoPersona { get; set; }
       
        //ComboBoxes
        public IEnumerable<SelectListItem> TiposDeIdentificacion { get; set; }
        public IEnumerable<SelectListItem> TiposDeSexo { get; set; }
        public IEnumerable<SelectListItem> Nacionalidades { get; set; }
    }
    public class DateValidation : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime todayDate = Convert.ToDateTime(value);
            return todayDate <= DateTime.Now;
        }
    }
}