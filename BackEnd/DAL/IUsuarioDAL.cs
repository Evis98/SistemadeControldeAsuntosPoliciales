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
        string GetCedulaUsuario(string cedula);
        bool CedulaUsuarioExiste(string cedula);
        Usuarios GetUsuarioCedula(string cedula);
    }
}
