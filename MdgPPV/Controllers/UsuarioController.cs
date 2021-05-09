using MdgPPV.Data;
using MdgPPV.Filters;
using MdgPPV.Models;
using MdgPPV.Models.UsuarioViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace MdgPPV.Controllers
{
    public class UsuarioController : Controller
    {
        private IEnumerable<SelectListItem> roles;

        [AuthorizeUser(idOperacion: 2)]
        // GET: Usuario
        public ActionResult Index()
        {

            using (InformesTecnicosDBEntities db = new InformesTecnicosDBEntities())
            {
                var usuarios = db.Usuario.Include("Rol");

                return View(usuarios.ToList());

            }
        }

        public ActionResult Usuario()
        {
            Llenar();
            TablaUsuarioViewModel modelo = new TablaUsuarioViewModel()
            {
                ListaRoles = roles
            };
            return View(modelo);
        }

        private void Llenar()
        {
            roles = new RolAdmin().Consultar().ToList().Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Nombre });
        }

        [HttpPost]
        public ActionResult Usuario(TablaUsuarioViewModel model)
        {
            try
            {

                if (ModelState.IsValid)
                {

                    Usuario modeloI = new Usuario()
                    {
                        Id = model.Usuario.Id,
                        Nombre = model.Usuario.Nombre,
                        Email = model.Usuario.Email,
                        Contraseña = model.Usuario.Contraseña,
                        Fecha = model.Usuario.Fecha,
                        IdRol = model.Usuario.IdRol

                    };
                    new UsuarioAdmin().Guardar(modeloI);

                    Llenar();
                    model = new TablaUsuarioViewModel()
                    {
                        ListaRoles = roles
                    };
                    return Redirect("~/Usuario/");
                }

                return View(model);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }


        public ActionResult EditarUsuario(TablaUsuarioViewModel model, int? Id)
        {

            using (InformesTecnicosDBEntities db = new InformesTecnicosDBEntities())
            {

                if (Id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                Usuario usuario = db.Usuario.Find(Id);

                if (usuario == null)
                {
                    return HttpNotFound();
                }
                ViewBag.IdRol = new SelectList(db.Rol.ToList(), "Id", "Nombre", usuario.IdRol);
                return View(usuario);
            }

        }


        [HttpPost]
        public ActionResult EditarUsuario(int? id)
        {

            using (InformesTecnicosDBEntities db = new InformesTecnicosDBEntities())
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var userToUpdate = db.Usuario.Find(id);
                if (TryUpdateModel(userToUpdate, "",
                   new string[] { "Nombre", "Email", "Contraseña", "Fecha", "IdRol" }))
                {
                    try
                    {
                        db.SaveChanges();

                        return Redirect("~/Usuario/Index");
                    }
                    catch (RetryLimitExceededException)
                    {
                        ModelState.AddModelError("", "No es posible guardar los cambios. Si el problema persiste comuniquelo al administrador");
                    }
                }
                ViewBag.IdRol = new SelectList(db.Rol.ToList(), "Id", "Nombre", userToUpdate.IdRol);
                return View(userToUpdate);
            }
        }


        [HttpGet]
        public ActionResult EliminarUsuario(int Id)
        {
            using (InformesTecnicosDBEntities db = new InformesTecnicosDBEntities())
            {

                var oUsuario = db.Usuario.Find(Id);

                if (Id > 0)
                {
                    db.Usuario.Remove(oUsuario);
                    db.SaveChanges();
                }

            }
            return Redirect("~/Usuario/Index");
        }
    }
}