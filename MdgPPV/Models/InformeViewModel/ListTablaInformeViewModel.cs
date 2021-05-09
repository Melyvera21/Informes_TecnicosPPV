using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MdgPPV.Models.InformeViewModel
{
    public class ListTablaInformeViewModel
    {
        public int Id { get; set; }
        public string NombreInforme { get; set; }
        public DateTime? Fecha { get; set; }
        public DateTime? Vencimiento { get; set; }
        public int IdCliente { get; set; }
       
    }
}