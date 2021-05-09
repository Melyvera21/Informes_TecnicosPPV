using System;
using System.Linq;
using System.Web.Mvc;

namespace MdgPPV.Controllers
{
    public class AccesoController : Controller
    {
        // GET: Acceso
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string User, string Pass)
        {
            try
            {
                using (Models.InformesTecnicosDBEntities db = new Models.InformesTecnicosDBEntities())
                {
                    var oUser = (from d in db.Usuario
                                 where d.Email == User.Trim() && d.Contraseña == Pass.Trim()
                                 select d).FirstOrDefault();
                    if (oUser == null)
                    {
                        ViewBag.Error = "Usuario o contraseña invalida";
                        return View();
                    }

                    Session["User"] = oUser;

                }

                return Redirect("~/Home/Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }

        }
    }
}