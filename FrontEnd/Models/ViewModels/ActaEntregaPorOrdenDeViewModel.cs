﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace FrontEnd.Models.ViewModels
{
    public class ActaEntregaPorOrdenDeViewModel 
    {
        public int IdActaEntregaPorOrdenDe { get; set; }

        public int IdActaLigada { get; set; }

        [Display(Name = "Número de Folio")]
        [StringLength(30)]
        public string NumeroFolio { get; set; }

        [Required(ErrorMessage = "Este campo es Obligatorio")]
        [DataType(DataType.Date)]
        [DateValidation(ErrorMessage = "Fecha ingresada invalida")]
        [Display(Name = "Fecha de la Entrega")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "Este campo es Obligatorio")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "Hora de la Entrega")]
        public DateTime Hora { get; set; }

        [Display(Name = "Número de Inventario")]
        [StringLength(int.MaxValue)]
        public string NumeroInventario { get; set; }


        [Display(Name = "Identificación Orden De")]
        [Required(ErrorMessage = "Este campo es Obligatorio")]
        public string IdentificacionPorOrdenDe { get; set; }

        [Display(Name = "Por Orden De")]
        public string NombrePorOrdenDe { get; set; }

        [Display(Name = "Número de Identificación")]
        [Required(ErrorMessage = "Este campo es Obligatorio")]
        public string CedulaFuncionarioQueEntrega { get; set; }

        [Display(Name = "Funcionario que entrega")]
        public string NombreFuncionarioQueEntrega { get; set; }



        [Required]
        [Display(Name = "Tipo de Testigo")]
        public int TipoTestigo { get; set; }

        [Display(Name = "Tipo de Testigo")]
        public string VistaTipoTestigo { get; set; }


        [Display(Name = "Testigo")]
        public string IdTestigoPolicia { get; set; }

        [Display(Name = "Testigo")]
        public string IdTestigoPersona { get; set; }

        [Display(Name = "Cédula")]
        public string VistaIdTestigo { get; set; }

        [Display(Name = "Testigo")]
        public string VistaTestigo { get; set; }




        [Display(Name = "Número de Acta Referenciada")]
        public string NumeroActaLigada { get; set; }

        [Display(Name = "Número de Resolución")]
        [StringLength(150, ErrorMessage = "Número de Resolución excede los 150 caracteres.")]
        [Required(ErrorMessage = "Este campo es Obligatorio")]
        public string NumeroResolucion { get; set; }

        [Display(Name = "Identificación de la persona a entregar")]
        [Required(ErrorMessage = "Este campo es Obligatorio")]
        public string CedulaPersonaQueSeLeEntrega { get; set; }


        [Display(Name = "Nombre de la persona a entregar")]

        public string NombrePersonaQueSeLeEntrega { get; set; }

        [Display(Name = "Descripción de los artículos")]
        [StringLength(5000)]
        public string DescripcionDeArticulos { get; set; }

        [Required(ErrorMessage = "Este campo es Obligatorio")]
        [Display(Name = "Tipo de Inventario")]
        public int TipoInventario { get; set; }

        [Display(Name = "Inventario referenciado de")]
        public string VistaTipoInventario { get; set; }

        public int Estado { get; set; }

        [Display(Name = "Estado")]
        public string VistaEstadoActa { get; set; }
      
        public IEnumerable<SelectListItem> Estados { get; set; }
        public IEnumerable<SelectListItem> TiposDeInventario { get; set; }
        public IEnumerable<SelectListItem> TiposTestigo { get; set; }
        public class DateValidation : ValidationAttribute
        {
            public override bool IsValid(object value)
            {
                DateTime todayDate = Convert.ToDateTime(value);
                return todayDate <= DateTime.Now;
            }
        }
    }

}