using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BackEnd.DAL
{
    public class PoliciaDAL : IPoliciaDAL
    {
        public List<Policias> Get()
        {
            List<Policias> lista = new List<Policias>();
            using (SCAPEntities db = new SCAPEntities())
            {
                lista = db.Policias.ToList();
            }
            return lista;
        }

        public void Add(Policias nuevo)
        {
            using (SCAPEntities db = new SCAPEntities())
            {
                db.Policias.Add(nuevo);
                db.SaveChanges();
            }

        }

        public void Edit(Policias cambiado)
        {
            using (SCAPEntities db = new SCAPEntities())
            {
                db.Entry(cambiado).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }

        }
        public Policias getPolicia(int id)
        {
            Policias poli = new Policias();
            using (SCAPEntities db = new SCAPEntities())
            {
                poli = db.Database.SqlQuery<Policias>("select * from Policias where idPolicia =" + id).Single<Policias>();
            }
            return poli;
        }
        public int getPoliciaCedula(string cedula)
        {
            int poli;
            using (SCAPEntities db = new SCAPEntities())
            {
                poli = db.Database.SqlQuery<int>("select idPolicia from Policias where cedula ='" + cedula+"'").Single<int>();
            }
            return poli;
        }

        public int? estadoDefault()
        {
            int estado;
            using (SCAPEntities db = new SCAPEntities())
            {

                estado = db.Database.SqlQuery<int>("Select idTablaGeneral from TablaGeneral where tabla = 'Policias' and descripcion = 'Activo'")
                                 .Single<int>();


            }
            return estado;
        }

        public int? getTipoCedula(int tipoCedula)
        {
            int aux;
            using (SCAPEntities db = new SCAPEntities())
            {
                aux = db.Database.SqlQuery<int>("Select idTablaGeneral from TablaGeneral where tabla= 'Policias' and campo = 'tipoCedula' and codigo =" + tipoCedula).Single<int>();
            }

            return aux;
        }
        public string getEstadoPolicia(int estado)
        {
            string descripcion;
            using (SCAPEntities db = new SCAPEntities())
            {
                descripcion = db.Database.SqlQuery<string>("Select descripcion from TablaGeneral where tabla= 'Policias' and campo = 'estado' and idTablaGeneral =" + estado).Single<string>();
            }
            return descripcion;
        }

        public int getIdEstado(string estado)
        {
            int IdTabla;
            using (SCAPEntities db = new SCAPEntities())
            {
                IdTabla = db.Database.SqlQuery<int>("Select idTablaGeneral from TablaGeneral where tabla= 'Policias' and campo = 'estado' and descripcion = '" + estado+"'").Single<int>();
            }
            return IdTabla;
        }

        public void CambiaEstadoPolicia(int id, int estado)
        {
            using (SCAPEntities db = new SCAPEntities())
            {
                string comando = "update Policias set estado = " + estado + "where idPolicia = " + id;
                db.Database.ExecuteSqlCommand(comando);
                db.SaveChanges();
            }
        }
    }
}
