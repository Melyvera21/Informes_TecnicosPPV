using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MdgPPV.Models.InformeViewModel
{
    public class TablaInformeViewModel
    {
        //public int Id { get; set; }

        //[Required]
        //[StringLength(50)]
        //[Display(Name = "Informe")]
        //public string NombreInforme { get; set; }

        //[Required]
        //[DataType(DataType.Date)]
        //[Display(Name = "Fecha")]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        //public DateTime? Fecha { get; set; }

        //[Required]
        //[DataType(DataType.Date)]
        //[Display(Name = "Vencimiento")]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        //public DateTime? Vencimiento { get; set; }

        public Informes Informe { get; set; }
        public IEnumerable<SelectListItem> ListaClientes { get; set; }
    }
}