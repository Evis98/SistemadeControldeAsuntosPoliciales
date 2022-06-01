using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections.Generic;
using BackEnd.DAL;
using BackEnd;
using FrontEnd.Models;
using FrontEnd.Models.ViewModels;
using System.IO;

namespace FrontEnd.Controllers
{
    public class PoliciaController : Controller
    {
        IPoliciaDAL policiaDAL;
        ITablaGeneralDAL tablaGeneralDAL;
        IAuditoriaDAL auditoriaDAL;
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

        public Policias ConvertirPolicia(PoliciaViewModel modelo)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            Policias policia = new Policias()
            {
                idPolicia = modelo.IdPolicia,
                cedula = modelo.Cedula.ToUpper(),
                tipoCedula = tablaGeneralDAL.GetCodigo("Policias", "tipoCedula", modelo.TipoCedula.ToString()).idTablaGeneral,
                nombre = modelo.Nombre.ToUpper(),
                fechaNacimiento = modelo.FechaNacimiento,
                correoElectronico = modelo.CorreoElectronico,
                direccion = modelo.Direccion.ToUpper(),
                telefonoCelular = modelo.TelefonoCelular,
                telefonoCasa = modelo.TelefonoCasa,
                contactoEmergencia = modelo.ContactoEmergencia.ToUpper(),
                telefonoEmergencia = modelo.TelefonoEmergencia,
            };
            if (modelo.Estado == 0)
            {
                policia.estado = tablaGeneralDAL.Get("Generales", "estado", "Activo").idTablaGeneral;
            }
            else
            {
                policia.estado = modelo.Estado;
            }
            return policia;
        }

        public PoliciaViewModel CargarPolicia(Policias policia)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            PoliciaViewModel modelo = new PoliciaViewModel()
            {
                IdPolicia = policia.idPolicia,
                Cedula = policia.cedula,
                TipoCedula = int.Parse(tablaGeneralDAL.Get(policia.tipoCedula).codigo),
                VistaTipoCedula = tablaGeneralDAL.Get(policia.tipoCedula).descripcion,
                Edad = ObtenerEdad(policia.fechaNacimiento),
                Nombre = policia.nombre,
                FechaNacimiento = policia.fechaNacimiento,
                CorreoElectronico = policia.correoElectronico,
                Direccion = policia.direccion,
                TelefonoCelular = policia.telefonoCelular,
                TelefonoCasa = policia.telefonoCasa,
                ContactoEmergencia = policia.contactoEmergencia,
                TelefonoEmergencia = policia.telefonoEmergencia,
                Estado = policia.estado,
                VistaEstado = tablaGeneralDAL.Get(policia.estado).descripcion
            };

            return modelo;
        }

        public void CrearCarpetaPolicia(PoliciaViewModel modelo)
        {
            string rutaCarpeta = Server.MapPath(@"~\ArchivosSCAP\Policias\" + modelo.Cedula.ToString() + "-" + modelo.Nombre.ToString());
            if (!Directory.Exists(rutaCarpeta))
            {
                Directory.CreateDirectory(rutaCarpeta);
                Console.WriteLine(rutaCarpeta);
            }
        }

        public ActionResult CambioEstado()
        {
            Autorizar();

            policiaDAL = new PoliciaDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            auditoriaDAL = new AuditoriaDAL();
            Policias policia = policiaDAL.GetPolicia((int)Session["idPolicia"]);

            if (tablaGeneralDAL.Get(policia.estado).descripcion == "Activo")
            {
                policia.estado = tablaGeneralDAL.Get("Generales", "estado", "Inactivo").idTablaGeneral;
            }
            else
            {
                policia.estado = tablaGeneralDAL.Get("Generales", "estado", "Activo").idTablaGeneral;
            }

            policiaDAL.Edit(policia);
            auditoriaDAL.Add(CambiarEstadoAuditoria((int)Session["idPolicia"]));
            return Redirect("~/Policia/Detalle/" + Session["idPolicia"]);

        }

        string ObtenerEdad(DateTime? fechaNacimiento)
        {

            var edad = DateTime.Today.Year - fechaNacimiento.Value.Year;
            if (fechaNacimiento.Value.Date > DateTime.Today.AddYears(-edad)) edad--;
            return edad.ToString();

        }

        public Auditorias ConvertirAuditoria(AuditoriaViewModel modelo)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            Auditorias auditoria;
            auditoria = new Auditorias()
            {
                idAuditoria = modelo.IdAuditoria,
                idCategoria = modelo.IdCategoria,
                idElemento = modelo.IdElemento,
                fecha = DateTime.Now,
                accion = modelo.Accion,
                idUsuario = modelo.IdUsuario
            };
            return auditoria;
        }

        public Auditorias CambiarEstadoAuditoria(int idPolicia)
        {
            AuditoriaViewModel modelo = new AuditoriaViewModel();
            tablaGeneralDAL = new TablaGeneralDAL();
            policiaDAL = new PoliciaDAL();
            usuarioDAL = new UsuarioDAL();
            auditoriaDAL = new AuditoriaDAL();
            Auditorias auditoria;
            auditoria = new Auditorias()
            {
                idAuditoria = modelo.IdAuditoria,
                accion = modelo.Accion = tablaGeneralDAL.GetCodigo("Auditoria", "accion", "3").idTablaGeneral,
                idCategoria = modelo.IdCategoria = tablaGeneralDAL.GetCodigo("Auditoria", "tabla", "1").idTablaGeneral,
                idUsuario = modelo.IdUsuario = usuarioDAL.GetUsuario((int?)Session["userID"]).idUsuario,
                fecha = DateTime.Now,
                idElemento = policiaDAL.GetPolicia(idPolicia).idPolicia
            };
            return auditoria;
        }

        public bool ValidarEdad(DateTime FechaNacimiento)
        {
            DateTime todayDate = FechaNacimiento;
            DateTime validationDate = new DateTime(DateTime.Now.Year - 18, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            return validationDate <= todayDate;
        }

        //Metodos de las Vistas
        public ActionResult Index(string filtrosSeleccionado, string busqueda)//Hay que revisar el nombre de la variable
        {
            Autorizar();
            policiaDAL = new PoliciaDAL();
            tablaGeneralDAL = new TablaGeneralDAL();

            //Carga combobox busqueda
            List<TablaGeneral> comboIndex = tablaGeneralDAL.Get("Policias", "index");
            List<SelectListItem> items = comboIndex.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.descripcion
                };
            });
            ViewBag.items = items;

            //Carga lista de policias
            List<PoliciaViewModel> policias = new List<PoliciaViewModel>();
            List<PoliciaViewModel> policiasFiltrados = new List<PoliciaViewModel>();
            foreach (Policias policia in policiaDAL.Get())
            {
                policias.Add(CargarPolicia(policia));
            }
            if (busqueda != null)
            {
                foreach (PoliciaViewModel policia in policias)
                {
                    if (filtrosSeleccionado == "Cédula")
                    {
                        if (policia.Cedula.Contains(busqueda))
                        {
                            policiasFiltrados.Add(policia);
                        }
                    }
                    if (filtrosSeleccionado == "Nombre")
                    {
                        if (policia.Nombre.Contains(busqueda))
                        {
                            policiasFiltrados.Add(policia);
                        }
                    }
                }
                policias = policiasFiltrados;
            }
            return View(policias.OrderBy(x => x.Nombre).ToList());
        }


        public ActionResult Nuevo()
        {
            Autorizar();
            tablaGeneralDAL = new TablaGeneralDAL();
            PoliciaViewModel modelo = new PoliciaViewModel()
            {
                TiposCedula = tablaGeneralDAL.Get("Policias", "tipoCedula").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo }),
                FechaNacimiento = DateTime.Today
            };
            return View(modelo);
        }

        [HttpPost]
        public ActionResult Nuevo(PoliciaViewModel modelo)
        {
            Autorizar();
            //revisar en el frontend el telefono
            policiaDAL = new PoliciaDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            usuarioDAL = new UsuarioDAL();
            auditoriaDAL = new AuditoriaDAL();
            
            AuditoriaViewModel auditoriaModelo = new AuditoriaViewModel
            {
                Accion = tablaGeneralDAL.GetCodigo("Auditoria", "accion", "1").idTablaGeneral,
                IdCategoria = tablaGeneralDAL.GetCodigo("Auditoria", "tabla", "1").idTablaGeneral,
                IdUsuario = usuarioDAL.GetUsuario((int?)Session["userID"]).idUsuario
            };

            try
            {
                if (ModelState.IsValid)
                {
                    int errores = 0;
                    if (policiaDAL.PoliciaExiste(modelo.Cedula))
                    {
                        ModelState.AddModelError(nameof(modelo.Cedula), "La cédula ingresada ya existe");
                        errores++;
                    }
                    if (ValidarEdad(modelo.FechaNacimiento))
                    {
                        ModelState.AddModelError(nameof(modelo.FechaNacimiento), "El policía debe ser Mayor de Edad");
                        errores++;
                    }
                    if (errores == 0)
                    {                      
                        CrearCarpetaPolicia(modelo);//revisar el tamaño del path de la carpeta
                        policiaDAL.Add(ConvertirPolicia(modelo));
                        auditoriaModelo.IdElemento = policiaDAL.GetPoliciaCedula(modelo.Cedula).idPolicia;
                        auditoriaDAL.Add(ConvertirAuditoria(auditoriaModelo));
                        int aux = policiaDAL.GetPoliciaCedula(modelo.Cedula).idPolicia;
                        return Redirect("~/Policia/Detalle/" + aux);
                    }
                }
                modelo.TiposCedula = tablaGeneralDAL.Get("Policias", "tipoCedula").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
                modelo.Estado = tablaGeneralDAL.Get("Generales", "estado", "Activo").idTablaGeneral;
                return View(modelo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ActionResult Detalle(int id)
        {
            Autorizar();
            policiaDAL = new PoliciaDAL();
            Session["idPolicia"] = id;
            Session["tabla"] = "Policia";
            Session["nombrePolicia"] = policiaDAL.GetPolicia(id).nombre;
            Session["auditoria"] = policiaDAL.GetPolicia(id).nombre;
            PoliciaViewModel modelo = CargarPolicia(policiaDAL.GetPolicia(id));
            return View(modelo);
        }

        public ActionResult Editar(int id)
        {
            Autorizar();
            policiaDAL = new PoliciaDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            PoliciaViewModel modelo = CargarPolicia(policiaDAL.GetPolicia(id));
            modelo.TiposCedula = tablaGeneralDAL.Get("Policias", "tipoCedula").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            return View(modelo);
        }

        [HttpPost]
        public ActionResult Editar(PoliciaViewModel modelo)
        {
            Autorizar();
            policiaDAL = new PoliciaDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            usuarioDAL = new UsuarioDAL();
            auditoriaDAL = new AuditoriaDAL();


            modelo.TiposCedula = tablaGeneralDAL.Get("Policias", "tipoCedula").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });

            AuditoriaViewModel auditoriaModelo = new AuditoriaViewModel()
            {
                Accion = tablaGeneralDAL.GetCodigo("Auditoria", "accion", "2").idTablaGeneral,
                IdCategoria = tablaGeneralDAL.GetCodigo("Auditoria", "tabla", "1").idTablaGeneral,
                IdUsuario = usuarioDAL.GetUsuario((int?)Session["userID"]).idUsuario
            };
            try
            {
                if (ModelState.IsValid)
                {
                    int errores = 0;
                    if (ValidarEdad(modelo.FechaNacimiento))
                    {
                        ModelState.AddModelError(nameof(modelo.FechaNacimiento), "El policía debe ser Mayor de Edad");
                        errores++;
                    }
                    if (policiaDAL.PoliciaExiste(modelo.Cedula) && modelo.Cedula != policiaDAL.GetPolicia(modelo.IdPolicia).cedula)
                    {
                        ModelState.AddModelError(nameof(modelo.Cedula), "La cédula ingresada ya existe");
                        errores++;
                    }
                    if (errores == 0)
                    {
                        policiaDAL.Edit(ConvertirPolicia(modelo));
                        auditoriaModelo.IdElemento = policiaDAL.GetPoliciaCedula(modelo.Cedula).idPolicia;
                        auditoriaDAL.Add(ConvertirAuditoria(auditoriaModelo));
                        return Redirect("~/Policia/Detalle/" + modelo.IdPolicia);
                    }
                }
                return View(modelo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}