using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.DAL
{
    public class UsuarioDAL : IUsuarioDAL
    {
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

        

      
    }
}
