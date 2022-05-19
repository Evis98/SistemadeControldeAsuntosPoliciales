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

        public bool GetRolesUsuario(int idUsuario){

            bool result = false;
            List<RolesUsuarios> lista;
            List<RolesUsuarios> aux = new List<RolesUsuarios>();

            using (SCAPEntities db = new SCAPEntities())
            {
                lista = db.RolesUsuarios.ToList();
                foreach(RolesUsuarios rol in lista)
                {
                    if(rol.idUsuario == idUsuario)
                    {
                        aux.Add(rol);
                    }
                }
                if (lista.Count != 0) {
                    result = true;
                }
            }
            return result;
            }
    }
}
