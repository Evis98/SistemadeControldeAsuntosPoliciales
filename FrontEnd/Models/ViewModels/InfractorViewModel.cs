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
        public int? TipoIdentificacion { get; set; }
        [Required]
        [StringLength(20)]
        [Display(Name = "Número de Identificación")]
        public string Identificacion { get; set; }
      
        [Required]
        [StringLength(50)]
        [Display(Name = "Nacionalidad")]
        public string Nacionalidad { get; set; }
        [Required]
        [StringLength(150)]
        [Display(Name = "Nombre Completo")]
        public string Nombre { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Nacimiento")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaNacimiento { get; set; }
        [Required]
        [StringLength(9)]
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; }
        [Required]
        [StringLength(250)]
        [Display(Name = "Dirección Exacta")]
        public string DireccionExacta { get; set; }
        [Required]
        [Display(Name = "Sexo")]
        public int Sexo { get; set; }
        [StringLength(100)]
        [EmailAddress]
        [Display(Name = "Correo Electrónico")]
        public string CorreoElectronico { get; set; }      
        [StringLength(150)]
        [Display(Name = "Observaciones")]
        public string Observaciones { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Profesión u Oficio")]
        public string ProfesionUOficio { get; set; }
        [Required]
        [StringLength(4)]
        [Display(Name = "Estatura")]
        public string Estatura { get; set; }
        [StringLength(150)]
        [Display(Name = "Tatuajes")]
        public string Tatuajes { get; set; }
        [StringLength(150)]
        [Display(Name = "Nombre del Padre")]
        public string NombrePadre { get; set; }
        [StringLength(150)]
        [Display(Name = "Nombre de la Madre")]
        public string NombreMadre { get; set; }
        [Display(Name = "Adjuntar imagen")]
        public HttpPostedFileBase Archivo { get; set; }
        public string Imagen { get; set; }

      
        public IEnumerable<SelectListItem> TiposDeIdentificacion { get; set; }
        public IEnumerable<SelectListItem> TiposDeSexo { get; set; }
        //public int? Estado { get; set; }
    }
}