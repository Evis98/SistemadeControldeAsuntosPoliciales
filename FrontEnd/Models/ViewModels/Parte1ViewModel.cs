using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FrontEnd.Models.ViewModels
{
    public class Parte1ViewModel 
    {
        //1 Ubicación del sitio
        public int IdPartePolcial { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha del Suceso")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

        public DateTime Fecha { get; set; }
        [Required]
        [DataType(DataType.Time)]
        [Display(Name = "Hora del Suceso")]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm}", ApplyFormatInEditMode = true)]
        public DateTime Hora { get; set; }

        // [Display(Name = "Provincia")]
        // public string Provincia { get; set; }

        //  [Display(Name = "Cantón")]
        // public string Canton { get; set; }

        [Required(ErrorMessage = "Este campo es Obligatorio")]
        [Display(Name = "Distrito")]
        public int Distrito { get; set; }

        [Required(ErrorMessage = "Este campo es Obligatorio")]
        [StringLength(250, ErrorMessage = "Descripción de barrio excede los 250 caracteres.")]
        [Display(Name = "Barrio y/o (Avenida-Calle)")]
        public string Barrio { get; set; }

        [Required(ErrorMessage = "Este campo es Obligatorio")]
        [StringLength(250, ErrorMessage = "Dirección excede los 250 caracteres.")]
        [Display(Name = "Dirección Exacta")]
        public string Direccion { get; set; }
        [Required(ErrorMessage = "Este campo es Obligatorio")]
        [Display(Name = "Lugar del Suceso")]
        public int LugarSuceso { get; set; }

        public int IdInfractor { get; set; }
        [Display(Name = "Tipo de Identificación")]
        public string TipoIdentificacionInfractor { get; set; }
        [Required(ErrorMessage = "Este campo es Obligatorio")]
        [Display(Name = "Número de Identificación")]
        public string IdentificacionInfractor { get; set; }


        //  [Display(Name = "Edad")]
        // public string EdadInfractor { get; set; }
        [Required(ErrorMessage = "Este campo es Obligatorio")]
        [StringLength(250, ErrorMessage = "Nombre excede los 250 caracteres.")]
        [Display(Name = "Nombre")]
        public string NombreInfractor { get; set; }

        // [Display(Name = "Nacionalidad")]
        // public string NacionalidadInfractor { get; set; }


        // [Display(Name = "Fecha de Nacimiento")]
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        // public string FechaNacimientoInfractor { get; set; }

        // [Display(Name = "Sexo")]
        // public string SexoInfractor { get; set; }

        // [Display(Name = "Dirección Exacta")]
        // public string DireccionExactaInfractor { get; set; }

        //  [Display(Name = "Conocido Como")]
        // public string ConocidoComoInfractor { get; set; }
        [Required(ErrorMessage = "Este campo es Obligatorio")]
        [Display(Name = "Aprehendido")]
        public int AprendidoInfractor { get; set; }
        [Required(ErrorMessage = "Este campo es Obligatorio")]
        [DataType(DataType.Time)]
        [Display(Name = "Hora de Aprehension")]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm}", ApplyFormatInEditMode = true)]
        public DateTime HoraAprehension { get; set; }
        [Required(ErrorMessage = "Este campo es Obligatorio")]
        [Display(Name = "Entendido")]
        public int EntendidoInfractor { get; set; }

        [Required(ErrorMessage = "Este campo es Obligatorio")]
        [StringLength(250, ErrorMessage = "Vestimenta excede los 100 caracteres.")]
        [Display(Name = "Vestimenta")]
        public string Vestimenta { get; set; }


        //  [Display(Name = "Rasgos Fisicos")]
        //  public string RasgosFisicos { get; set; }

        //2----------------------------------------------------------------------------

        //Ofendido 1
        public int IdOfendido1 { get; set; }

        [Display(Name = "Tipo de Identificación")]
        public int TipoIdentificacionOfendido1 { get; set; }
        [Required(ErrorMessage = "Este campo es Obligatorio")]
        [Display(Name = "Número de Identificación")]
        public string IdentificacionOfendido1 { get; set; }

        [Required(ErrorMessage = "Este campo es Obligatorio")]
        // [StringLength(250, ErrorMessage = "Este campo es Obligatorio")]
        [Display(Name = "Nombre")]
        public string NombreOfendido1 { get; set; }


        // [Display(Name = "Nacionalidad")]
        // public string NacionalidadOfendido1 { get; set; }


        // [Display(Name = "Fecha de Nacimiento")]
        //[DataType(DataType.Date)]
        // [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        // public DateTime FechaNacimientoOfendido1 { get; set; }

        // [Display(Name = "Edad")]
        // public string EdadOfendido1 { get; set; }

        //  [Display(Name = "Sexo")]
        //  public int SexoOfendido1 { get; set; }

        //  [Display(Name = "Dirección Exacta")]
        //  [StringLength(250, ErrorMessage = "Dirección Exacta excede los 250 caracteres.")]
        //   public string DireccionExactaOfendido1 { get; set; }

        // [StringLength(9, ErrorMessage = "Escriba un número de teléfono con 8 digitos.", MinimumLength = 9)]
        // [Display(Name = "Teléfono Casa Habitación")]
        //  public string TelefonoHabitacionOfendido1 { get; set; }


        // [StringLength(9, ErrorMessage = "Escriba un número de teléfono con 8 digitos.", MinimumLength = 9)]
        //  [Display(Name = "Teléfono Trabajo")]
        // public string TelefonoTrabajoOfendido1 { get; set; }

        //  [StringLength(9, ErrorMessage = "Escriba un número de teléfono con 8 digitos.", MinimumLength = 9)]
        //   [Display(Name = "Teléfono Celular")]
        // public string TelefonoCelularOfendido1 { get; set; }

        //  [StringLength(50, ErrorMessage = "Profesión excede los 50 caracteres.")]
        //   [Display(Name = "Profesión u Oficio")]
        //   public string ProfesionUOficioOfendido1 { get; set; }

        //   [StringLength(100, ErrorMessage = "Dirección de correo excede los 100 caracteres.")]
        //   [EmailAddress(ErrorMessage = "Escriba una dirección de correo válida.")]
        //   [Display(Name = "Correo Electrónico ")]
        //  public string CorreoElectronicoOfendido1 { get; set; }

        //Ofendido 2
        public int IdOfendido2 { get; set; }

        [Display(Name = "Tipo de Identificación")]
        public int TipoIdentificacionOfendido2 { get; set; }

        [Display(Name = "Número de Identificación")]
        public string IdentificacionOfendido2 { get; set; }


        [StringLength(250, ErrorMessage = "Nombre excede los 100 caracteres.")]
        [Display(Name = "Nombre")]
        public string NombreOfendido2 { get; set; }


        // [Display(Name = "Nacionalidad")]
        //  public string NacionalidadOfendido2 { get; set; }


        // [Display(Name = "Fecha de Nacimiento")]
        // [DataType(DataType.Date)]
        // [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        // public DateTime FechaNacimientoOfendido2 { get; set; }

        // [Display(Name = "Edad")]
        // public string EdadOfendido2 { get; set; }

        // [Display(Name = "Sexo")]
        //  public int SexoOfendido2 { get; set; }

        //  [Display(Name = "Dirección Exacta")]
        //  [StringLength(250, ErrorMessage = "Dirección Exacta excede los 250 caracteres.")]
        //  public string DireccionExactaOfendido2 { get; set; }

        //  [StringLength(9, ErrorMessage = "Escriba un número de teléfono con 8 digitos.", MinimumLength = 9)]
        //  [Display(Name = "Teléfono Casa Habitación")]
        //  public string TelefonoHabitacionOfendido2 { get; set; }

        //  [StringLength(9, ErrorMessage = "Escriba un número de teléfono con 8 digitos.", MinimumLength = 9)]
        //  [Display(Name = "Teléfono Trabajo")]
        //  public string TelefonoTrabajoOfendido2 { get; set; }

        //  [StringLength(9, ErrorMessage = "Escriba un número de teléfono con 8 digitos.", MinimumLength = 9)]
        // [Display(Name = "Teléfono Celular")]
        //  public string TelefonoCelularOfendido2 { get; set; }

        //  [StringLength(50, ErrorMessage = "Profesión excede los 50 caracteres.")]
        //  [Display(Name = "Profesión u Oficio")]
        // public string ProfesionUOficioOfendido2 { get; set; }

        // [StringLength(100, ErrorMessage = "Dirección de correo excede los 100 caracteres.")]
        //   [EmailAddress(ErrorMessage = "Escriba una dirección de correo válida.")]
        //  [Display(Name = "Correo Electrónico")]
        //  public string CorreoElectronicoOfendido2 { get; set; }

        //Ofendido 3
        public int IdOfendido3 { get; set; }

        [Display(Name = "Tipo de Identificación")]
        public int TipoIdentificacionOfendido3 { get; set; }

        [Display(Name = "Número de Identificación")]
        public string IdentificacionOfendido3 { get; set; }

        [StringLength(250, ErrorMessage = "Nombre excede los 250 caracteres.")]
        [Display(Name = "Nombre")]
        public string NombreOfendido3 { get; set; }


        //  [Display(Name = "Nacionalidad")]
        //  public string NacionalidadOfendido3 { get; set; }


        //  [Display(Name = "Fecha de Nacimiento")]
        //  [DataType(DataType.Date)]
        //  [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        //  public DateTime FechaNacimientoOfendido3 { get; set; }

        //  [Display(Name = "Edad")]
        //  public string EdadOfendido3 { get; set; }

        //  [Display(Name = "Sexo")]
        //   public int SexoOfendido3 { get; set; }

        //  [Display(Name = "Dirección Exacta")]
        //  [StringLength(250, ErrorMessage = "Dirección Exacta excede los 250 caracteres.")]
        //  public string DireccionExactaOfendido3 { get; set; }

        //  [StringLength(9, ErrorMessage = "Escriba un número de teléfono con 8 digitos.", MinimumLength = 9)]
        // [Display(Name = "Teléfono Casa Habitación")]
        //   public string TelefonoHabitacionOfendido3 { get; set; }

        //   [StringLength(9, ErrorMessage = "Escriba un número de teléfono con 8 digitos.", MinimumLength = 9)]
        //  [Display(Name = "Teléfono Trabajo")]
        public string TelefonoTrabajoOfendido3 { get; set; }

        //  [StringLength(9, ErrorMessage = "Escriba un número de teléfono con 8 digitos.", MinimumLength = 9)]
        //  [Display(Name = "Teléfono Celular")]
        //  public string TelefonoCelularOfendido3 { get; set; }

        //  [StringLength(50, ErrorMessage = "Profesión excede los 50 caracteres.")]
        //  [Display(Name = "Profesión u Oficio")]
        //  public string ProfesionUOficioOfendido3 { get; set; }

        // [StringLength(100, ErrorMessage = "Dirección de correo excede los 100 caracteres.")]
        // [EmailAddress(ErrorMessage = "Escriba una dirección de correo válida.")]
        //  [Display(Name = "Correo Electrónico ")]
        // public string CorreoElectronicoOfendido3 { get; set; }

        //Ofendido 4
        public int IdOfendido4 { get; set; }

        [Display(Name = "Tipo de Identificación")]
        public int TipoIdentificacionOfendido4 { get; set; }

        [Display(Name = "Número de Identificación")]
        public string IdentificacionOfendido4 { get; set; }

        [StringLength(250, ErrorMessage = "Nombre excede los 100 caracteres.")]
        [Display(Name = "Nombre")]
        public string NombreOfendido4 { get; set; }


        // [Display(Name = "Nacionalidad")]
        // public string NacionalidadOfendido4 { get; set; }


        // [Display(Name = "Fecha de Nacimiento")]
        // [DataType(DataType.Date)]
        //  [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        // public DateTime FechaNacimientoOfendido4 { get; set; }

        //  [Display(Name = "Edad")]
        //   public string EdadOfendido4 { get; set; }

        //  [Display(Name = "Sexo")]
        //  public int SexoOfendido4 { get; set; }

        //  [Display(Name = "Dirección Exacta")]
        // [StringLength(250, ErrorMessage = "Dirección Exacta excede los 250 caracteres.")]
        //  public string DireccionExactaOfendido4 { get; set; }

        //  [StringLength(9, ErrorMessage = "Escriba un número de teléfono con 8 digitos.", MinimumLength = 9)]
        //  [Display(Name = "Teléfono Casa Habitación")]
        //  public string TelefonoHabitacionOfendido4 { get; set; }


        //  [StringLength(9, ErrorMessage = "Escriba un número de teléfono con 8 digitos.", MinimumLength = 9)]
        //  [Display(Name = "Teléfono Trabajo")]
        //  public string TelefonoTrabajoOfendido4 { get; set; }

        //  [StringLength(9, ErrorMessage = "Escriba un número de teléfono con 8 digitos.", MinimumLength = 9)]
        //  [Display(Name = "Teléfono Celular")]
        //  public string TelefonoCelularOfendido4 { get; set; }

        //  [StringLength(50, ErrorMessage = "Profesión excede los 50 caracteres.")]
        //   [Display(Name = "Profesión u Oficio")]
        //   public string ProfesionUOficioOfendido4 { get; set; }

        //  [StringLength(100, ErrorMessage = "Dirección de correo excede los 100 caracteres.")]
        //  [EmailAddress(ErrorMessage = "Escriba una dirección de correo válida.")]
        // [Display(Name = "Correo Electrónico ")]
        // public string CorreoElectronicoOfendido4 { get; set; }

        //Ofendido 5
        public int IdOfendido5 { get; set; }

        [Display(Name = "Tipo de Identificación")]
        public int TipoIdentificacionOfendido5 { get; set; }

        [Display(Name = "Número de Identificación")]
        public string IdentificacionOfendido5 { get; set; }

        [StringLength(250, ErrorMessage = "Nombre excede los 100 caracteres.")]
        [Display(Name = "Nombre")]
        public string NombreOfendido5 { get; set; }


        //   [Display(Name = "Nacionalidad")]
        //   public string NacionalidadOfendido5 { get; set; }


        //   [Display(Name = "Fecha de Nacimiento")]
        //   [DataType(DataType.Date)]
        //   [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        //   public DateTime FechaNacimientoOfendido5 { get; set; }

        //  [Display(Name = "Edad")]
        //  public string EdadOfendido5 { get; set; }

        //   [Display(Name = "Sexo")]
        //  public int SexoOfendido5 { get; set; }

        //   [Display(Name = "Dirección Exacta")]
        //   [StringLength(250, ErrorMessage = "Dirección Exacta excede los 250 caracteres.")]
        //   public string DireccionExactaOfendido5 { get; set; }

        //    [StringLength(9, ErrorMessage = "Escriba un número de teléfono con 8 digitos.", MinimumLength = 9)]
        //    [Display(Name = "Teléfono Casa Habitación")]
        //    public string TelefonoHabitacionOfendido5 { get; set; }


        //  [StringLength(9, ErrorMessage = "Escriba un número de teléfono con 8 digitos.", MinimumLength = 9)]
        //   [Display(Name = "Teléfono Trabajo")]
        //   public string TelefonoTrabajoOfendido5 { get; set; }

        //   [StringLength(9, ErrorMessage = "Escriba un número de teléfono con 8 digitos.", MinimumLength = 9)]
        //    [Display(Name = "Teléfono Celular")]
        //   public string TelefonoCelularOfendido5 { get; set; }

        //   [StringLength(50, ErrorMessage = "Profesión excede los 50 caracteres.")]
        //   [Display(Name = "Profesión u Oficio")]
        //   public string ProfesionUOficioOfendido5 { get; set; }

        //   [StringLength(100, ErrorMessage = "Dirección de correo excede los 100 caracteres.")]
        //   [EmailAddress(ErrorMessage = "Escriba una dirección de correo válida.")]
        //   [Display(Name = "Correo Electrónico ")]
        //    public string CorreoElectronicoOfendido5 { get; set; }

        //testigo 1
        public int IdTestigo1 { get; set; }

        [Display(Name = "Tipo de Identificación")]
        public int TipoIdentificacionTestigo1 { get; set; }

        [Display(Name = "Número de Identificación")]
        public string IdentificacionTestigo1 { get; set; }

        [StringLength(250, ErrorMessage = "Nombre excede los 100 caracteres.")]
        [Display(Name = "Nombre")]
        public string NombreTestigo1 { get; set; }

        // [Display(Name = "Dirección Exacta")]
        //  [StringLength(250, ErrorMessage = "Dirección Exacta excede los 250 caracteres.")]
        //  public string DireccionExactaTestigo1 { get; set; }

        //  [StringLength(9, ErrorMessage = "Escriba un número de teléfono con 8 digitos.", MinimumLength = 9)]
        //  [Display(Name = "Teléfono")]
        //  public string TelefonoTestigo1 { get; set; }

        //  [StringLength(50, ErrorMessage = "Profesión excede los 50 caracteres.")]
        //  [Display(Name = "Lugar de Trabajo")]
        //  public string LugarTrabajoTestigo1 { get; set; }

        //  [StringLength(100, ErrorMessage = "Dirección de correo excede los 100 caracteres.")]
        //  [EmailAddress(ErrorMessage = "Escriba una dirección de correo válida.")]
        // [Display(Name = "Correo Electrónico ")]
        //  public string CorreoElectronicoTestigo1 { get; set; }

        //testigo 2
        public int IdTestigo2 { get; set; }

        [Display(Name = "Tipo de Identificación")]
        public int TipoIdentificacionTestigo2 { get; set; }

        [Display(Name = "Número de Identificación")]
        public string IdentificacionTestigo2 { get; set; }

        [StringLength(250, ErrorMessage = "Nombre excede los 100 caracteres.")]
        [Display(Name = "Nombre")]
        public string NombreTestigo2 { get; set; }

        //  [Display(Name = "Dirección Exacta")]
        //  [StringLength(250, ErrorMessage = "Dirección Exacta excede los 250 caracteres.")]
        //  public string DireccionExactaTestigo2 { get; set; }

        //  [StringLength(9, ErrorMessage = "Escriba un número de teléfono con 8 digitos.", MinimumLength = 9)]
        // [Display(Name = "Teléfono")]
        // public string TelefonoTestigo2 { get; set; }

        //  [StringLength(50, ErrorMessage = "Profesión excede los 50 caracteres.")]
        //  [Display(Name = "Lugar de Trabajo")]
        //   public string LugarTrabajoTestigo2 { get; set; }

        //   [StringLength(100, ErrorMessage = "Dirección de correo excede los 100 caracteres.")]
        //   [EmailAddress(ErrorMessage = "Escriba una dirección de correo válida.")]
        //   [Display(Name = "Correo Electrónico ")]
        //   public string CorreoElectronicoTestigo2 { get; set; }

        [Required(ErrorMessage = "Este campo es Obligatorio")]
        [Display(Name = "Descripción de Hechos")]
        [StringLength(10000, ErrorMessage = "Descripción de hechos excede el máximo de caracteres.")]
        public string DescripcionHechos { get; set; }

        [Required(ErrorMessage = "Este campo es Obligatorio")]
        [Display(Name = "Diligencias Policiales")]
        [StringLength(10000, ErrorMessage = "Diligencias Policiales excede el máximo de caracteres.")]
        public string DiligenciasPoliciales { get; set; }
        [Required(ErrorMessage = "Este campo es Obligatorio")]
        [Display(Name = "Manisfestación de los Ofendidos")]
        [StringLength(10000, ErrorMessage = "Manisfestación del Ofendido el máximo de caracteres.")]
        public string ManisfestacionOfendido { get; set; }
        [Required(ErrorMessage = "Este campo es Obligatorio")]
        [Display(Name = "Manisfestación de los Testigos")]
        [StringLength(10000, ErrorMessage = "Manisfestación de los Testigos excede el máximo de caracteres.")]
        public string ManisfestacionTestigo { get; set; }

        //Aspectos Administrativos
        [Required(ErrorMessage = "Este campo es Obligatorio")]
        [Display(Name = "¿Quién dio la alerta?")]
        public int Alertador { get; set; }
        [Required(ErrorMessage = "Este campo es Obligatorio")]
        [Display(Name = "Ente a Cargo")]
        public int EnteAcargo { get; set; }

        
        [Display(Name = "Número de Móvil")]
        public string Movil { get; set; }


        //Policía Actuante
        [Required(ErrorMessage = "Este campo es Obligatorio")]
        [Display(Name = "Nombre")]
        public string NombrePoliciaActuante { get; set; }

        [Required(ErrorMessage = "Este campo es Obligatorio")]
        [Display(Name = "Número de Identificación")]
        public string IdentificacionPoliciaActuante { get; set; }


        [Display(Name = "Teléfono")]
        public string TelefonoPoliciaActuante { get; set; }

   
        [Display(Name = "Unidad Origen")]      
        public int UnidadOrigenPoliciaActuante { get; set; }


        [DataType(DataType.Time)]
        [Display(Name = "Hora de Confección")]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm}", ApplyFormatInEditMode = true)]
        public DateTime HoraConfeccionDocumento { get; set; }

        //Policía Asiste 
        [Required(ErrorMessage = "Este campo es Obligatorio")]
        [Display(Name = "Nombre")]
        public string NombrePoliciaAsiste { get; set; }

        [Required(ErrorMessage = "Este campo es Obligatorio")]
        [Display(Name = "Número de Identificación")]
        public string IdentificacionPoliciaAsiste { get; set; }


        [Display(Name = "Teléfono")]
        public string TelefonoPoliciaAsiste { get; set; }

        [Required(ErrorMessage = "Este campo es Obligatorio")]
        [Display(Name = "Unidad Origen")]
        
        public int UnidadOrigenPoliciaAsiste { get; set; }

        
        [StringLength(20, ErrorMessage = "Numero de Móvil excede los 20 caracteres.")]
        [Display(Name = "Número de Móvil")]

        public string NumeroMovilPolciaAsiste { get; set; }


        public IEnumerable<SelectListItem> TiposLugaresSuceso { get; set; }
        public IEnumerable<SelectListItem> Distritos { get; set; }
        public IEnumerable<SelectListItem> TiposDeIdentificacion { get; set; }

        public IEnumerable<SelectListItem> TiposDeSexo { get; set; }
        public IEnumerable<SelectListItem> Nacionalidades { get; set; }

        public IEnumerable<SelectListItem> Alertadores { get; set; }
        public IEnumerable<SelectListItem> UnidadesDeOrigen { get; set; }
        public IEnumerable<SelectListItem> EntesACargos { get; set; }
        public IEnumerable<SelectListItem> Aprehendidos { get; set; }
        public IEnumerable<SelectListItem> Entendidos { get; set; }
    }
}
