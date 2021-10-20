using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.DAL
{
    public class InfractorDAL : IInfractorDAL
    {
        //Recibe todos los infractores ingresados en la base de datos con su información
        public List<Infractores> Get()
        {
            List<Infractores> lista = new List<Infractores>();
            using (SCAPEntities db = new SCAPEntities())
            {
                lista = db.Infractores.ToList();
            }
            return lista;
        }

        //Permite agregar un infractor nuevo a la base de datos
        public void Add(Infractores infractor)
        {
            using (SCAPEntities db = new SCAPEntities())
            {
                db.Infractores.Add(infractor);
                db.SaveChanges();
            }
        }

        //Permite actualizar la información de un infractor en la base de datos
        public void Edit(Infractores infractor)
        {
            using (SCAPEntities db = new SCAPEntities())
            {
                db.Entry(infractor).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }

        }

        //Permite recibir un infractor con toda su información a través de su atributo "idInfractor"
        public Infractores GetInfractor(int? idInfractor)
        {
            Infractores infractor = new Infractores();
            using (SCAPEntities db = new SCAPEntities())
            {
                infractor = db.Database.SqlQuery<Infractores>("select * from Infractores where idInfractor =" + idInfractor).Single<Infractores>();
            }
            return infractor;
        }

        //Permite recibir el nombre del infractor a través de su atributo "idInfractor"
        public string GetNombreInfractor(int idInfractor)
        {
            string nombre;
            using (SCAPEntities db = new SCAPEntities())
            {
                nombre = db.Database.SqlQuery<string>("select nombreCompleto from Infractores where idInfractor ='" + idInfractor + "'").Single<string>();
            }
            return nombre;
        }

        //Permite recibir el numero de indentificacion del infractor a través de su atributo "idInfractor"
        public int GetNumeroIdInfractor(string identificacionInfractor)
        {
            int id;
            using (SCAPEntities db = new SCAPEntities())
            {
                id = db.Database.SqlQuery<int>("select idInfractor from Infractores where numeroDeIdentificacion ='" + identificacionInfractor + "'").Single<int>();
            }
            return id;
        }
    }
}