using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.DAL
{
    public class UsuarioDAL :  IUsuarioDAL
    {
        public Usuarios GetUsuario(int? idUsuario)
        {
            Usuarios usuario = new Usuarios();
            using (SCAPEntities db = new SCAPEntities())
            {
                usuario = db.Database.SqlQuery<Usuarios>("select * from Usuarios where idUsuario =" + idUsuario).Single<Usuarios>();
            }
            return usuario;
        }
    }
}
