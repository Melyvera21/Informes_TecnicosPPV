using MdgPPV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MdgPPV.Data
{
    public class RolAdmin
    {
        public List<Rol> Consultar()
        {
            using (InformesTecnicosDBEntities itContext = new InformesTecnicosDBEntities())
            {
                return itContext.Rol.AsNoTracking().ToList();
            }
        }
    }
}