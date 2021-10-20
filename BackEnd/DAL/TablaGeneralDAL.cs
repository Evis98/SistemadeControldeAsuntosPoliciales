﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.DAL
{
    public class TablaGeneralDAL : ITablaGeneralDAL
    {
        //Recibe el atributo "idTablaGeneral" donde el atributo "descripción" es "Activo"
        public int EstadoDefault()
        {
            int estado;
            using (SCAPEntities db = new SCAPEntities())
            {

                estado = db.Database.SqlQuery<int>("Select idTablaGeneral from TablaGeneral where tabla = 'Policias' and descripcion = 'Activo'").Single<int>();
            }
            return estado;
        }

        //Permite recibir el atributo "idTablaGeneral" de la Tabla General haciendo uso del atributo "tipoCedula" del policía
        public int GetTipoCedula(int? tipoCedula)
        {
            int aux;
            using (SCAPEntities db = new SCAPEntities())
            {
                aux = db.Database.SqlQuery<int>("Select idTablaGeneral from TablaGeneral where tabla= 'Policias' and campo = 'tipoCedula' and codigo =" + tipoCedula).Single<int>();
            }

            return aux;
        }

        //Permite recibir el atributo "idTablaGeneral" de la Tabla General haciendo uso del atributo "estado" de la misma tabla
        public int GetIdEstado(string estado)
        {
            int IdTabla;
            using (SCAPEntities db = new SCAPEntities())
            {
                IdTabla = db.Database.SqlQuery<int>("Select idTablaGeneral from TablaGeneral where tabla= 'Policias' and campo = 'estado' and descripcion = '" + estado + "'").Single<int>();
            }
            return IdTabla;
        }

        //Permite recibir el atributo "idTablaGeneral" de la Tabla General haciendo uso del atributo "tipoRequisito" de la tabla Requisitos
        public int GetTipoRequisito(int? tipoRequisito)
        {
            int aux;
            using (SCAPEntities db = new SCAPEntities())
            {
                aux = db.Database.SqlQuery<int>("Select idTablaGeneral from TablaGeneral where tabla= 'Requisitos' and campo = 'tipoRequisito' and codigo =" + tipoRequisito).Single<int>();
            }
            return aux;
        }
        public string GetDescripcion(int? idTablaGeneral)
        {
            string aux;
            using (SCAPEntities db = new SCAPEntities())
            {
                aux = db.Database.SqlQuery<string>("Select descripcion from TablaGeneral where idTablaGeneral =" + idTablaGeneral).Single<string>();
            }
            return aux;
        }
        //Permite recibir el atributo "descripcion" de la Tabla General haciendo uso del atributo "tipoArma" de la misma
        public List<TablaGeneral> GetTiposArma()
        {
            List<TablaGeneral> descripcion;
            using (SCAPEntities db = new SCAPEntities())
            {
                descripcion = db.Database.SqlQuery<TablaGeneral>("select * from TablaGeneral where tabla = 'Armas' and campo = 'tipoArma'").ToList<TablaGeneral>();
            }
            return descripcion;
        }

        public List<TablaGeneral> GetTiposCedulaPolicia()
        {
            List<TablaGeneral> descripcion;
            using (SCAPEntities db = new SCAPEntities())
            {
                descripcion = db.Database.SqlQuery<TablaGeneral>("Select * from TablaGeneral where tabla = 'Policias' and campo = 'tipoCedula'").ToList<TablaGeneral>();
            }
            return descripcion;
        }
        public List<TablaGeneral> GetTiposRequisito()
        {
            List<TablaGeneral> descripcion;
            using (SCAPEntities db = new SCAPEntities())
            {
                descripcion = db.Database.SqlQuery<TablaGeneral>("Select * from TablaGeneral where tabla = 'Requisitos' and campo = 'tipoRequisito'").ToList<TablaGeneral>();
            }
            return descripcion;
        }



        public int GetTipoIdentificacionInfractor(int? tipoId)
        {
            int aux;
            using (SCAPEntities db = new SCAPEntities())
            {
                aux = db.Database.SqlQuery<int>("Select idTablaGeneral from TablaGeneral where tabla= 'Infractores' and campo = 'tipoDeIdentificacion' and codigo =" + tipoId).Single<int>();
            }

            return aux;
        }

        public List<TablaGeneral> GetTipoSexoInfractor()
        {
            List<TablaGeneral> descripcion;
            using (SCAPEntities db = new SCAPEntities())
            {
                descripcion = db.Database.SqlQuery<TablaGeneral>("Select * from TablaGeneral where tabla = 'Infractores' and campo = 'sexo'").ToList<TablaGeneral>();
            }
            return descripcion;
        }

       public List<TablaGeneral> GetTiposIdentificacionInfractor() 
        {
            List<TablaGeneral> tiposIdentificacion;
            using (SCAPEntities db = new SCAPEntities())
            {
                tiposIdentificacion = db.Database.SqlQuery<TablaGeneral>("Select * from TablaGeneral where tabla = 'Infractores' and campo = 'tipoDeIdentificacion'").ToList<TablaGeneral>();
            }
            return tiposIdentificacion;
        }




        public List<TablaGeneral> GetTiposCalibre()
        {
            List<TablaGeneral> descripcion;
            using (SCAPEntities db = new SCAPEntities())
            {
                descripcion = db.Database.SqlQuery<TablaGeneral>("Select * from TablaGeneral where tabla = 'Armas' and campo = 'calibre'").ToList<TablaGeneral>();
            }
            return descripcion;


        }

        public List<TablaGeneral> GetTiposCondicion()
        {

            List<TablaGeneral> descripcion;
            using (SCAPEntities db = new SCAPEntities())
            {
                descripcion = db.Database.SqlQuery<TablaGeneral>("Select * from TablaGeneral where tabla = 'Armas' and campo = 'condicion'").ToList<TablaGeneral>();
            }
            return descripcion;
        }

        public List<TablaGeneral> GetTiposUbicacion()
        {


            List<TablaGeneral> descripcion;
            using (SCAPEntities db = new SCAPEntities())
            {
                descripcion = db.Database.SqlQuery<TablaGeneral>("Select * from TablaGeneral where tabla = 'Armas' and campo = 'ubicacion'").ToList<TablaGeneral>();
            }
            return descripcion;
        }



    }







}