using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.DAL
{
    public interface IUsuarioDAL
    {
        List<Usuarios> Get();
        void Add(Usuarios usuario);
        void Edit(Usuarios usuario);
        Usuarios GetUsuario(int? idUsuario);
        Usuarios GetUsuarioCorreo(string correo);
        bool CedulaUsuarioExiste(string cedula);
        bool UsernameUsuarioExiste(string username);
        Usuarios GetUsuarioCedula(string cedula);
    }
}
