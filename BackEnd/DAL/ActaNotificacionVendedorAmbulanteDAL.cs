using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.DAL
{
    public class ActaNotificacionVendedorAmbulanteDAL : IActaNotificacionVendedorAmbulanteDAL
    {
        //Recibe todos las armas ingresados en la base de datos con su información
        public List<ActasNotificacionVendedorAmbulante> Get()
        {
            List<ActasNotificacionVendedorAmbulante> lista = new List<ActasNotificacionVendedorAmbulante>();
            using (SCAPEntities db = new SCAPEntities())
            {
                lista = db.ActasNotificacionVendedorAmbulante.ToList();
            }
            return lista;
        }

        //Permite agregar un arma nueva a la base de datos
        public void Add(ActasNotificacionVendedorAmbulante nuevo)
        {
            using (SCAPEntities db = new SCAPEntities())
            {
                db.ActasNotificacionVendedorAmbulante.Add(nuevo);
                db.SaveChanges();
            }
        }

        //Permite actualizar la información de un arma en la base de datos
        public void Edit(ActasNotificacionVendedorAmbulante cambiada)
        {
            using (SCAPEntities db = new SCAPEntities())
            {
                db.Entry(cambiada).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }

        }
        //Permite recibir un arma con toda su información a través de su atributo "idArma"
        public ActasNotificacionVendedorAmbulante GetActaNotificacionVendedorAmbulante(int idActaNotificacionVendedorAmbulante)
        {
            try
            {
                ActasNotificacionVendedorAmbulante resultado;
                using (SCAPEntities db = new SCAPEntities())
                {
                    resultado = db.ActasNotificacionVendedorAmbulante.Find(idActaNotificacionVendedorAmbulante);
                }
                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ActasNotificacionVendedorAmbulante GetActaNotificacionVendedorAmbulanteFolio(string numeroFolio)
        {
            try
            {
                ActasNotificacionVendedorAmbulante resultado;
                using (SCAPEntities db = new SCAPEntities())
                {
                    resultado = db.ActasNotificacionVendedorAmbulante.Where(x => x.numeroFolio == numeroFolio).FirstOrDefault();
                }
                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public int GetCount()
        {
            using (SCAPEntities db = new SCAPEntities())
            {
                return db.ActasNotificacionVendedorAmbulante.Where(x => x.fechaHora.Year == DateTime.Now.Year).Count();
            }

        }
        public bool FolioExiste(string numeroFolio)
        {
            try
            {
                int contador;
                using (SCAPEntities db = new SCAPEntities())
                {
                    contador = db.ActasNotificacionVendedorAmbulante.Where(x => x.numeroFolio == numeroFolio).Count();
                }
                return contador > 0 ? true : false;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string GetFolioActaNotificacionVendedorAmbulante(string serie)
        {
            if (FolioExiste(serie))
            {
                return serie;
            }
            else
            {
                return null;
            }
        }
        public List<ActasNotificacionVendedorAmbulante> GetActaNotificacionVendedorAmbulanteRango(DateTime inicio, DateTime final)
        {
            try
            {
                List<ActasNotificacionVendedorAmbulante> resultado;
                using (SCAPEntities db = new SCAPEntities())
                {
                    resultado = db.ActasNotificacionVendedorAmbulante.Where(x => x.fechaHora >= inicio && x.fechaHora <= final).ToList();
                }
                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
