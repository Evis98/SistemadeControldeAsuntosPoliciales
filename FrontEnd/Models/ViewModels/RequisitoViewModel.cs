using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FrontEnd.Models.ViewModels
{
    public class RequisitoViewModel
    {
        public int IdRequisito { get; set; }

        public int IdPolicia { get; set; }
        [Display(Name = "Nombre del Policia")]
        public string NombrePolicia { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Vencimiento")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaVencimiento { get; set; } = null;
        [Display(Name = "Fecha de Vencimiento")]
        public string VistaFechaVencimiento { get; set; }

        [Required]
        [Display(Name = "Tipo de Requisito")]
        public int TipoRequisito { get; set; }
        [Display(Name = "Tipo de Requisito")]
        public string VistaTipoRequisito { get; set; }

        [Required]
        [StringLength(250, ErrorMessage = "Detalles exceden los 250 caracteres.")]
        [Display(Name = "Detalles")]
        public string Detalles { get; set; }

        [Display(Name = "Archivo (Opcional)")]
        public HttpPostedFileBase Archivo { get; set; }
        public string Imagen { get; set; }


        //ComboBoxes
        public IEnumerable<SelectListItem> TiposRequisito { get; set; }
    }
   

}