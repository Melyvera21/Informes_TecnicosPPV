using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MdgPPV.Models.UsuarioViewModel
{
    public class TablaUsuarioViewModel
    {
        //public int Id { get; set; }

        //[Required]
        //[StringLength(50)]
        //[Display(Name = "Nombre")]
        //public string Nombre { get; set; }

        //[Required]
        //[StringLength(50)]
        //[Display(Name = "Email")]
        //public string Email { get; set; }

        //[Required]
        //[StringLength(50)]
        //[Display(Name = "Contraseña")]
        //public string Contraseña { get; set; }

        //[Required]
        //[DataType(DataType.Date)]
        //[Display(Name = "Fecha")]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        //public DateTime? Fecha { get; set; }


        public Usuario Usuario { get; set; }

        public IEnumerable<SelectListItem> ListaRoles { get; set; }
    }
}