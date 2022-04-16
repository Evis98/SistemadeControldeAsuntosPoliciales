using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace FrontEnd.Models.ViewModels
{
    public class ActaHallazgoViewModel
    {

        public int IdActaHallazgo { get; set; }


        [Display(Name = "Número de Folio")]
        [StringLength(30)]
        public string NumeroFolio { get; set; }

        [Display(Name = "Distrito")]
        [Required]
        public int Distrito { get; set; }
        [Display(Name = "Distrito")]
        public string VistaDistrito { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DateValidationHallazgo(ErrorMessage = "Fecha ingresada invalida")]
        [Display(Name = "Fecha del Suceso")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }

        public int EstadoActa { get; set; }
        [Display(Name = " Estado del Acta")]
        public string VistaEstadoActa { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "Hora del Suceso")]
        public DateTime Hora { get; set; }

        [Display(Name = "Avenida")]
        [Required]
        [StringLength(60)]
        public string Avenida { get; set; }

        [Display(Name = "Calle")]
        [Required]
        [StringLength(60)]
        public string Calle { get; set; }

        [Display(Name = "Otras Señas")]
        [Required]
        [StringLength(250)]
        public string OtrasSenas { get; set; }

        [Display(Name = "Inventario")]
        [Required]
        [StringLength(500)]
        public string Inventario { get; set; }

        [Display(Name = "Observaciones")]
       
        [StringLength(250)]
        public string Observaciones { get; set; }

        [Display(Name = "Nombre del Encargado")]
        [Required]
        public string Encargado { get; set; }
        [Display(Name = "Encargado")]
        public string VistaPoliciaEncargado { get; set; }

        [Display(Name = "Testigo")]
       
        public string Testigo { get; set; }
        [Display(Name = "Testigo")]
        public string VistaPoliciaTestigo { get; set; }
        [Display(Name = "Supervisor")]
        [Required]
        public string Supervisor { get; set; }
        [Display(Name = "Supervisor")]
        public string VistaPoliciaSupervisor { get; set; }

        public IEnumerable<SelectListItem> Distritos { get; set; }


    }
    public class DateValidationHallazgo : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime todayDate = Convert.ToDateTime(value);
            return todayDate <= DateTime.Now;
        }
    }
}