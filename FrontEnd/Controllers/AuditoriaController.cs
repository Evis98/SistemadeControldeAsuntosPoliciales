using BackEnd;
using BackEnd.DAL;
using FrontEnd.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace FrontEnd.Controllers
{
    public class AuditoriaController : Controller
    {

        IAuditoriaDAL auditoriaDAL;
        ITablaGeneralDAL tablaGeneralDAL;
        IUsuarioDAL usuarioDAL;

        //Metodos Útiles
        public void Autorizar()
        {
            if (Session["userID"] != null)
            {
                if (Session["Rol"].ToString() == "4" || Session["Rol"].ToString() == "1")
                {
                    Session["Error"] = "Usuario no autorizado";
                    Response.Redirect("~/Error/ErrorUsuario.cshtml");
                }
            }
            else
            {
                Response.Redirect("~/Login/Index");
            }
        }

        public AuditoriaViewModel CargarAuditoria(Auditorias auditoria)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            usuarioDAL = new UsuarioDAL();
            AuditoriaViewModel modelo = new AuditoriaViewModel()
            {
                IdAuditoria = auditoria.idAuditoria,
                IdCategoria = auditoria.idCategoria,
                VistaCategoria = tablaGeneralDAL.Get(auditoria.idCategoria).descripcion,
                IdElemento = auditoria.idElemento,
                Fecha = auditoria.fecha,
                Accion = auditoria.accion,
                VistaAccion = tablaGeneralDAL.Get(auditoria.accion).descripcion,
                IdUsuario = auditoria.idUsuario,
                VistaUsuario = usuarioDAL.GetUsuario(auditoria.idUsuario).nombre
            };

            return modelo;
        }

        //Metodo de la Vista
        public ActionResult ListadoAuditoria(int id, string tiposAccion, string filtrosSeleccionado, string busquedaFechaInicioP, string busquedaFechaFinalP)
        {
            Autorizar();
            auditoriaDAL = new AuditoriaDAL();
            tablaGeneralDAL = new TablaGeneralDAL();

            //Carga lista de auditorías
            List<AuditoriaViewModel> auditorias = new List<AuditoriaViewModel>();
            List<AuditoriaViewModel> auditoriasFiltrados = new List<AuditoriaViewModel>();
            foreach (Auditorias auditoria in auditoriaDAL.Get())
            {
                if (id == auditoria.idElemento && tablaGeneralDAL.Get(auditoria.idCategoria).descripcion == Session["tabla"].ToString())
                {
                    auditorias.Add((AuditoriaViewModel)CargarAuditoria(auditoria));
                }
            }
            if (filtrosSeleccionado != "0" && tiposAccion != null)
            {
                foreach (AuditoriaViewModel auditoria in auditorias)
                {

                    if (filtrosSeleccionado == "Acción")
                    {
                        if (tablaGeneralDAL.Get(auditoria.Accion).descripcion.Contains(tiposAccion))
                        {
                            auditoriasFiltrados.Add(auditoria);
                        }
                    }

                }
                if (filtrosSeleccionado == "Fecha")
                {
                    DateTime fechaInicio = DateTime.Parse(busquedaFechaInicioP);
                    DateTime fechaFinal = DateTime.Parse(busquedaFechaFinalP);

                    if (fechaInicio < fechaFinal)
                    {
                        if (auditoriaDAL.GetAuditoriasRango(fechaInicio, fechaFinal, id) != null)
                        {

                            foreach (Auditorias auditoriasFecha in auditoriaDAL.GetAuditoriasRango(fechaInicio, fechaFinal, id).ToList())
                                if (id == auditoriasFecha.idElemento && tablaGeneralDAL.Get(auditoriasFecha.idCategoria).descripcion == Session["tabla"].ToString())
                                {
                                    {
                                        auditoriasFiltrados.Add((AuditoriaViewModel)CargarAuditoria(auditoriasFecha));
                                    }
                                }
                        }
                    }


                }
                auditorias = auditoriasFiltrados;

            }
            return View(auditorias);
        }
    }
}
