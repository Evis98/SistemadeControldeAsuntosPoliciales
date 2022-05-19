using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.DAL
{
    public interface IUsuarioDAL
    {
        Usuarios GetUsuario(int? idUsuario);
        bool GetRolesUsuario(int idUsuario);
        Usuarios GetUsuarioCorreo(string correo);
    }
}
