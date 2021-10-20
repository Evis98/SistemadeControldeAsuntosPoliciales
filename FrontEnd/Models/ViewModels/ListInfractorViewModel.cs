using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FrontEnd.Models.ViewModels
{

    public class ListInfractorViewModel
    {

        public int IdInfractor { get; set; }

        [Display(Name = "Tipo de Identificación")]
        public string TipoIdentificacion { get; set; }


        [Display(Name = "Número de Identificación")]
        public string Identificacion { get; set; }

        [Display(Name = "Nacionalidad")]
        public string Nacionalidad { get; set; }


        [Display(Name = "Nombre Completo")]
        public string Nombre { get; set; }


        [Display(Name = "Fecha de Nacimiento")]
        public string FechaNacimiento { get; set; }


        [Display(Name = "Teléfono")]
        public string Telefono { get; set; }


        [Display(Name = "Dirección Exacta")]
        public string DireccionExacta { get; set; }

        [Display(Name = "Sexo")]
        public string Sexo { get; set; }
    

        [Display(Name = "Correo Electrónico")]
        public string CorreoElectronico { get; set; }
        
        [Display(Name = "Observaciones")]
        public string Observaciones { get; set; }

        
        [Display(Name = "Profesión u Oficio")]
        public string ProfesionUOficio { get; set; }
       
        [Display(Name = "Estatura")]
        public string Estatura { get; set; }
       
        [Display(Name = "Tatuajes")]
        public string Tatuajes { get; set; }
        
        [Display(Name = "Nombre del Padre")]
        public string NombrePadre { get; set; }
     
        [Display(Name = "Nombre de la Madre")]
        public string NombreMadre { get; set; }
        
        [Display(Name = "Imagen")]
        public string Imagen { get; set; }

        [Display(Name = "Edad")]
        public string Edad { get; set; }


    }
}