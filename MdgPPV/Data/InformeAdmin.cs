using MdgPPV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MdgPPV.Data
{
    public class InformeAdmin
    {
        public void Guardar(Informes modelo)
        {
            using (InformesTecnicosDBEntities itContext = new InformesTecnicosDBEntities())
            {
                itContext.Informes.Add(modelo);
                itContext.SaveChanges();
            }
        }

        public Informes Obtener(int id)
        {
            var informe = new Informes();
            try
            {
                using (var context = new InformesTecnicosDBEntities())
                {
                    informe = context.Informes
                                    .Include("Cliente")
                                    .Where(x => x.Id == id)
                                    .Single();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return informe;
        }
    }
}