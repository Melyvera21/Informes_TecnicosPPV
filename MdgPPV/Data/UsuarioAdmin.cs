using MdgPPV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MdgPPV.Data
{
    public class UsuarioAdmin
    {
        public void Guardar(Usuario modelo)
        {
            using (InformesTecnicosDBEntities itContext = new InformesTecnicosDBEntities())
            {
                itContext.Usuario.Add(modelo);
                itContext.SaveChanges();
            }
        }
    }
}