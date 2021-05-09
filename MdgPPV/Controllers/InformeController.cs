using MdgPPV.Data;
using MdgPPV.Filters;
using MdgPPV.Models;
using MdgPPV.Models.InformeViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MdgPPV.Controllers
{
    public class InformeController : Controller
    {
        private IEnumerable<SelectListItem> clientes;

        #region INFORMES TECNICOS
        [AuthorizeUser(idOperacion: 3)]

        // GET: Informe
        public ActionResult Index(string filtro)
        {
            using (InformesTecnicosDBEntities db = new InformesTecnicosDBEntities())
            {
                var busqueda = from o in db.Informes.Include("Cliente") select o;

                if (!String.IsNullOrEmpty(filtro))
                {
                    busqueda = busqueda.Where(o => o.NombreInforme.Contains(filtro) || o.Cliente.RazonSocial.Contains(filtro));
                }
                return View(busqueda.ToList());
            }
        }


        public ActionResult CargarInforme()
        {
            Llenar();
            TablaInformeViewModel modelo = new TablaInformeViewModel()
            {
                ListaClientes = clientes
            };
            return View(modelo);
        }

        private void Llenar()
        {
            clientes = new ClienteAdmin().Consultar().ToList().Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.RazonSocial });
        }

        [HttpPost]
        public ActionResult CargarInforme(TablaInformeViewModel model, HttpPostedFileBase file)
        {
            
            try
            {

                if (ModelState.IsValid)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                    
                    var fileName = Path.GetFileName(file.FileName);
                    byte[] bytes;

                    using (BinaryReader br = new BinaryReader(file.InputStream))
                    {
                        bytes = br.ReadBytes(file.ContentLength);
                    }

                        Informes modeloI = new Informes()
                        {
                            IdCliente = model.Informe.IdCliente,
                            NombreInforme = model.Informe.NombreInforme,
                            Fecha = model.Informe.Fecha,
                            Vencimiento = model.Informe.Vencimiento,                            
                            PdfName = Path.GetFileName(file.FileName),
                            ContentType = file.ContentType,
                            PdfFile = bytes,

                        };
                    new InformeAdmin().Guardar(modeloI);

                    }

                    Llenar();
                    model = new TablaInformeViewModel()
                    {
                        ListaClientes = clientes
                    };

                    return Redirect("~/Informe/");
                }
               
                return View(model);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
          
        }

        public FileContentResult FileDownload(int id)
        {
            byte[] Data;
            string name;

            InformeAdmin db = new InformeAdmin();

            Informes informes = db.Obtener(id);

            Data = informes.PdfFile.ToArray();
            name = informes.PdfName;

            return File(Data, "Text", name);
        }



        //EDICION DE INFORMES
        public ActionResult EditarInforme(int? Id)
        {
            using (InformesTecnicosDBEntities db = new InformesTecnicosDBEntities())
            {

                if (Id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                Informes informe = db.Informes.Find(Id);

                if (informe == null)
                {
                    return HttpNotFound();
                }
                ViewBag.IdCliente = new SelectList(db.Cliente.ToList(), "Id", "RazonSocial", informe.IdCliente);
                return View(informe);
            }
        }


        [HttpPost]
        public ActionResult EditarInforme(HttpPostedFileBase file, int? id)
        {

            using (InformesTecnicosDBEntities db = new InformesTecnicosDBEntities())
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var informeToUpdate = db.Informes.Find(id);
                if (TryUpdateModel(informeToUpdate, "",
                   new string[] { "NombreInforme", "Fecha", "Vencimiento", "IdCliente", "PdfFile", "PdfName", "ContenType" }))
                {
                    try
                    {
                        db.SaveChanges();

                        return Redirect("~/Informe/Index");
                    }
                    catch (RetryLimitExceededException)
                    {
                        ModelState.AddModelError("", "No es posible guardar los cambios. Si el problema persiste comuniquelo al administrador");
                    }
                }
                ViewBag.IdCliente = new SelectList(db.Cliente.ToList(), "Id", "RazonSocial", informeToUpdate.IdCliente);
                return View(informeToUpdate);
            }
        }


        [HttpGet]
        public ActionResult EliminarInforme(int Id)
        {
            using (InformesTecnicosDBEntities db = new InformesTecnicosDBEntities())
            {

                var oInforme = db.Informes.Find(Id);

                if (Id > 0)
                {
                    db.Informes.Remove(oInforme);
                    db.SaveChanges();
                }

            }
            return Redirect("~/Informe/Index");
        }

        #endregion
    }
}