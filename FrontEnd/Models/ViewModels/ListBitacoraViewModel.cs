﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FrontEnd.Models.ViewModels
{
    public class ListBitacoraViewModel
    {
        public int IdBitacora { get; set; }
        public int IdArma { get; set; }

        [Display(Name = "Número de serie")]
        public string NumeroSerieArmaAsignada { get; set; }

        [Display(Name = "Tipo de arma")]
        public int TipoArmaAsignada { get; set; }

        [Display(Name = "Armero que entrega")]
        public string ArmeroProveedor { get; set; }

        [Display(Name = "Armero que recibe")]
        public string ArmeroReceptor { get; set; }

        [Display(Name = "Policía Solicitante")]
        public string PoliciaSolicitante { get; set; }

        [Display(Name = "Condición del arma al entregarse")]
        public int CondicionInicial { get; set; }

        [Display(Name = "Condición del arma al devolverse")]
        public int? CondicionFinal { get; set; }

        [Display(Name = "Fecha de solicitud")]
        public DateTime FechaCreacion { get; set; }

        [Display(Name = "Fecha de devolución")]
        public DateTime? FechaFinalizacion { get; set; }

        [Display(Name = "Munición entregada con el arma")]
        public int MunicionEntregada { get; set; }

        [Display(Name = "Munición devuelta con el arma")]
        public int? MunicionDevuelta { get; set; }

        [Display(Name = "Cargadores entregados con el arma")]
        public int CargadoresEntregados { get; set; }

        [Display(Name = "Cargadores devueltos con el arma")]
        public int? CargadoresDevueltos { get; set; }

        [Display(Name = "Observaciones")]
        public string Observaciones { get; set; }

        [Display(Name = "Estado actual de bitácora")]
        public string EstadoActualBitacora { get; set; }
    }
}