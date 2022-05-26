using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.DAL
{
    public class UsuarioDAL : IUsuarioDAL
    {
        public List<Usuarios> Get()
        {
            List<Usuarios> lista = new List<Usuarios>();
            using (SCAPEntities db = new SCAPEntities())
            {
                lista = db.Usuarios.ToList();
            }
            return lista;
        }
        public void Add(Usuarios usuario)
        {
            using (SCAPEntities db = new SCAPEntities())
            {
                db.Usuarios.Add(usuario);
                db.SaveChanges();
            }
        }
        public void Edit(Usuarios usuario)
        {
            using (SCAPEntities db = new SCAPEntities())
            {
                db.Entry(usuario).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }

        }
        public Usuarios GetUsuario(int? idUsuario)
        {
            try
            {
                Usuarios resultado;
                using (SCAPEntities db = new SCAPEntities())
                {
                    resultado = db.Usuarios.Find(idUsuario);
                }
                return resultado;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public Usuarios GetUsuarioCorreo(string correo)
        {
            try
            {
                Usuarios resultado = new Usuarios();
                using (SCAPEntities db = new SCAPEntities())
                {
                    resultado = db.Usuarios.Where(x => x.usuario == correo).FirstOrDefault();
                }
                return resultado;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public bool CedulaUsuarioExiste(string cedula)
        {
            try
            {
                int contador;
                using (SCAPEntities db = new SCAPEntities())
                {
                    contador = db.Usuarios.Where(x => x.cedula == cedula).Count();
                }
                return contador > 0 ? true : false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string GetCedulaUsuario(string cedula)
        {
            if (CedulaUsuarioExiste(cedula))
            {
                return cedula;
            }
            else
            {
                return null;
            }
        }


        public Usuarios GetUsuarioCedula(string cedula)
        {
            try
            {
                Usuarios resultado;
                using (SCAPEntities db = new SCAPEntities())
                {
                    resultado = db.Usuarios.Where(x => x.cedula == cedula).FirstOrDefault();
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

