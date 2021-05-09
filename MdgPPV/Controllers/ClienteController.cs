using MdgPPV.Filters;
using MdgPPV.Models;
using MdgPPV.Models.ClienteViewModel;
using System;
using System.Linq;
using System.Web.Mvc;

namespace MdgPPV.Controllers
{
    public class ClienteController : Controller
    {
        #region ADMINISTRACION DE CLIENTES

        [AuthorizeUser(idOperacion: 2)]
        // GET: Cliente
        public ActionResult Index(string filtro)
        {
            using (InformesTecnicosDBEntities db = new InformesTecnicosDBEntities())
            {
                var busqueda = from o in db.Cliente select o;

                if (!String.IsNullOrEmpty(filtro))
                {
                    busqueda = busqueda.Where(o => o.RazonSocial.Contains(filtro) || o.NombreFantasia.Contains(filtro) || o.Direccion.Contains(filtro) || o.Cuit.Contains(filtro) || o.Telefono.Contains(filtro) || o.Email.Contains(filtro) || o.Responsable.Contains(filtro));
                }
                return View(busqueda.ToList());
            }
        }

        public ActionResult NuevoCliente()
        {

            return View();
        }

        [HttpPost]
        public ActionResult NuevoCliente(TablaClienteViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (InformesTecnicosDBEntities db = new InformesTecnicosDBEntities())
                    {
                        var oCliente = new Cliente();
                        oCliente.Email = model.Email;
                        oCliente.Fecha = model.Fecha;
                        oCliente.NombreFantasia = model.NombreFantasia;
                        oCliente.RazonSocial = model.RazonSocial;
                        oCliente.Responsable = model.Responsable;
                        oCliente.Telefono = model.Telefono;
                        oCliente.Cuit = model.Cuit;
                        oCliente.Direccion = model.Direccion;

                        db.Cliente.Add(oCliente);
                        db.SaveChanges();
                    }

                    return Redirect("~/Cliente/");
                }

                return View(model);


            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }

        //Edicion de registros de Cliente
        public ActionResult EditarCliente(int Id)
        {
            TablaClienteViewModel model = new TablaClienteViewModel();
            using (InformesTecnicosDBEntities db = new InformesTecnicosDBEntities())
            {
                var oCliente = db.Cliente.Find(Id);
                model.NombreFantasia = oCliente.NombreFantasia;
                model.Email = oCliente.Email;
                model.RazonSocial = oCliente.RazonSocial;
                model.Telefono = oCliente.Telefono;
                model.Cuit = oCliente.Cuit;
                model.Direccion = oCliente.Direccion;
                model.Id = oCliente.Id;
                model.Responsable = oCliente.Responsable;
                model.Fecha = oCliente.Fecha;
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult EditarCliente(TablaClienteViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (InformesTecnicosDBEntities db = new InformesTecnicosDBEntities())
                    {
                        var oCliente = db.Cliente.Find(model.Id);
                        oCliente.Email = model.Email;
                        oCliente.Fecha = model.Fecha;
                        oCliente.NombreFantasia = model.NombreFantasia;
                        oCliente.Cuit = model.Cuit;
                        oCliente.Responsable = model.Responsable;
                        oCliente.RazonSocial = model.RazonSocial;
                        oCliente.Direccion = model.Direccion;
                        oCliente.Telefono = model.Telefono;

                        db.Entry(oCliente).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }

                    return Redirect("~/Cliente/Index");
                }

                return View(model);


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult Eliminar(int Id)
        {
            using (InformesTecnicosDBEntities db = new InformesTecnicosDBEntities())
            {

                var oCliente = db.Cliente.Find(Id);
                db.Cliente.Remove(oCliente);
                db.SaveChanges();
            }
            return Redirect("~/Cliente/Index");
        }


        #endregion
    }
}