using MdgPPV.Filters;
using MdgPPV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MdgPPV.Controllers
{
    public class HomeController : Controller
    {
        #region INICIO DE APLICACION

        [AuthorizeUser(idOperacion: 1)]
        // GET: Home
        public ActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Index(Usuario usuario)
        {
            ViewBag.Nombre = usuario.Nombre;

            return View();
        }

        #endregion
    }
}