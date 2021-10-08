using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FrontEnd.Models.ViewModels
{
    public class RequisitoViewModel
    {
        public int IdRequisito { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de vencimiento: ")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Fecha_Vencimiento { get; set; }
        [Required]
        [Display(Name = "Tipo de requisito: ")]
        public int TipoRequisito { get; set; }
        public string Imagen { get; set; }
        [Required]
        [StringLength(250)]
        [Display(Name = "Detalles: ")]
        public string Detalles { get; set; }
        [Display(Name = "Archivo: ")]
        public HttpPostedFileBase Archivo { get; set; }
        [StringLength(150)]
        [Display(Name = "Policía: ")]
        public string Nombre { get; set; }
        public int IdPolicia { get; set; }
    }
}