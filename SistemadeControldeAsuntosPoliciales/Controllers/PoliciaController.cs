using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections.Generic;
using ConsoleApp1.DAL;
using ConsoleApp1;
using SistemadeControldeAsuntosPoliciales.Models;
using SistemadeControldeAsuntosPoliciales.Models.ViewModels;

namespace SistemadeControldeAsuntosPoliciales.Controllers
{
    public class PoliciaController : Controller
    {

        PoliciaDAL policiaDAL = new PoliciaDAL();

        // GET: Policia
        public ActionResult Index()
        {
            List<ListPoliciaViewModel> lista;
            List<Policias> lista2 = new List<Policias>();
            lista2 = policiaDAL.Get().ToList();
            lista = (from d in lista2
                     select new ListPoliciaViewModel
                     {
                         IdPolicia = d.idPolicia,
                         Cedula = d.cedula,
                         TipoCedula = (int) d.tipoCedula,
                         Nombre = d.nombre,
                         Fecha_nacimiento = (System.DateTime) d.fechaNacimiento,
                         CorreoElectronico = d.correoElectronico,
                         Direccion = d.direccion,
                         TelefonoCelular = d.telefonoCelular,
                         TelefonoCasa = d.telefonoCasa,
                         ContactoEmergencia = d.contactoEmergencia,
                         TelefonoEmergencia = d.telefonoEmergencia,
                         Estado = (int)d.estado
                     }).ToList();
            return View(lista);
        }
    }
}