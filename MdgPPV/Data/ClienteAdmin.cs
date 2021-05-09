using MdgPPV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MdgPPV.Data
{
    public class ClienteAdmin
    {
        public List<Cliente> Consultar()
        {
            using (InformesTecnicosDBEntities itContext = new InformesTecnicosDBEntities())
            {
                return itContext.Cliente.AsNoTracking().ToList();
            }
        }
    }
}