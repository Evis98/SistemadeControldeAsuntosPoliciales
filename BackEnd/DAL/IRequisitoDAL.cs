﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.DAL
{
   public interface IRequisitoDAL
    {
        List<Requisitos> Get();
        void Add(Requisitos requisito);
        void Edit(Requisitos requisito);
        void EliminaRequisito(Requisitos requisito);
        Requisitos GetRequisito(int idRequisito);
        List<Requisitos> GetRequisitosPortacion(int idPolicia, string detalle);

    }
}
