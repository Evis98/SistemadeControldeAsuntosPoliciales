using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FrontEnd.Models.ViewModels
{
    public class ListParte1ViewModel 
    {
        public int IdParte { get; set; }
        [Display(Name = "Número de Folio")]
        public string NumeroFolio { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Fecha del Suceso")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }
        [DataType(DataType.Time)]
        [Display(Name = "Hora del Suceso")]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime Hora { get; set; }
        [Display(Name = "Distrito")]
        public string Distrito { get; set; }
        [Display(Name = "Barrio y/o (Avenida-Calle)")]
        public string Barrio { get; set; }
        [Display(Name = "Dirección Exacta")]
        public string Direccion { get; set; }
        [Display(Name = "Lugar del Suceso")]
        public string LugarSuceso { get; set; }
        public int IdInfractor { get; set; }
        [Display(Name = "Número de Identificación")]
        public string IdentificacionInfractor { get; set; }
        [Display(Name = "Nombre")]
        public string NombreInfractor { get; set; }
        [Display(Name = "Edad del Imputado")]
        public string EdadInfractor { get; set; }
        [Display(Name = "Aprehendido")]
        public string AprendidoInfractor { get; set; }
        [DataType(DataType.Time)]
        [Display(Name = "Hora de Aprehension")]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime HoraAprehension { get; set; }
        [Display(Name = "Entendido")]
        public string EntendidoInfractor { get; set; }
        [Display(Name = "Vestimenta")]
        public string Vestimenta { get; set; }

        public int IdOfendido1 { get; set; }
        [Display(Name = "Número de Identificación")]
        public string IdentificacionOfendido1 { get; set; }
        [Display(Name = "Nombre")]
        public string NombreOfendido1 { get; set; }
        [Display(Name = "Edad del ofendido durante el suceso")]
        public string EdadOfendido1 { get; set; }
        public int IdOfendido2 { get; set; }
        [Display(Name = "Número de Identificación")]
        public string IdentificacionOfendido2 { get; set; }
        [Display(Name = "Nombre")]
        public string NombreOfendido2 { get; set; }
        [Display(Name = "Edad del ofendido durante el suceso")]
        public string EdadOfendido2 { get; set; }
        public int IdOfendido3 { get; set; }
        [Display(Name = "Número de Identificación")]
        public string IdentificacionOfendido3 { get; set; }
        [Display(Name = "Nombre")]
        public string NombreOfendido3 { get; set; }
        [Display(Name = "Edad del ofendido durante el suceso")]
        public string EdadOfendido3 { get; set; }
        public int IdOfendido4 { get; set; }
        [Display(Name = "Número de Identificación")]
        public string IdentificacionOfendido4 { get; set; }
        [Display(Name = "Nombre")]
        public string NombreOfendido4 { get; set; }
        [Display(Name = "Edad del ofendido durante el suceso")]
        public string EdadOfendido4 { get; set; }
        public int IdOfendido5 { get; set; }
        [Display(Name = "Número de Identificación")]
        public string IdentificacionOfendido5 { get; set; }
        [Display(Name = "Nombre")]
        public string NombreOfendido5 { get; set; }
        [Display(Name = "Edad del ofendido durante el suceso")]
        public string EdadOfendido5 { get; set; }
        public int IdTestigo1 { get; set; }
        [Display(Name = "Número de Identificación")]
        public string IdentificacionTestigo1 { get; set; }
        [Display(Name = "Nombre")]
        public string NombreTestigo1 { get; set; }

        public int IdTestigo2 { get; set; }
        [Display(Name = "Número de Identificación")]
        public string IdentificacionTestigo2 { get; set; }
        [Display(Name = "Nombre")]
        public string NombreTestigo2 { get; set; }

        [Display(Name = "Descripción de Hechos")]
        public string DescripcionHechos { get; set; }
        [Display(Name = "Diligencias Policiales")]
        public string DiligenciasPoliciales { get; set; }
        [Display(Name = "Manisfestación de los Ofendidos")]
        public string ManisfestacionOfendido { get; set; }
        [Display(Name = "Manisfestación de los Testigos")]
        public string ManisfestacionTestigo { get; set; }
        [Display(Name = "¿Quién dio la alerta?")]
        public string Alertador { get; set; }
        [Display(Name = "Ente a Cargo")]
        public string EnteAcargo { get; set; }
        [Display(Name = "Número de Móvil")]
        public string Movil { get; set; }


        public int IdPoliciaActuante { get; set; }
        [Display(Name = "Nombre")]
        public string NombrePoliciaActuante { get; set; }
        [Display(Name = "Número de Identificación")]
        public string IdentificacionPoliciaActuante { get; set; }
        [Display(Name = "Teléfono")]
        public string TelefonoPoliciaActuante { get; set; }
        [Display(Name = "Unidad Origen")]
        public string UnidadOrigenPoliciaActuante { get; set; }


        public int IdPoliciaAsiste { get; set; }
        [Display(Name = "Nombre")]
        public string NombrePoliciaAsiste { get; set; }
        [Display(Name = "Número de Identificación")]
        public string IdentificacionPoliciaAsiste { get; set; }
        [Display(Name = "Teléfono")]
        public string TelefonoPoliciaAsiste { get; set; }
        [Display(Name = "Unidad Origen")]
        public string UnidadOrigenPoliciaAsiste { get; set; }
        [Display(Name = "Número de Móvil")]
        public string NumeroMovilPolciaAsiste { get; set; }

        [DataType(DataType.Time)]
        [Display(Name = "Hora de Confección")]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime HoraConfeccionDocumento { get; set; }     


    }
}
