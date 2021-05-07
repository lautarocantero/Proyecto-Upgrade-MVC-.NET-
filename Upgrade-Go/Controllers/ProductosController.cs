using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Upgrade_Go.Models;

namespace Upgrade_Go.Controllers
{
    public class ProductosController : Controller
    {
        private DataBase db = new DataBase();

        // GET: Productos
        public ActionResult Index()
        {
            return View(db.Productos.ToList());
        }

        public ActionResult Inicio(string tipo)
        {
            ViewBag.tipo = tipo;
            return View(db.Productos.ToList());
        }

        public ActionResult Filtrar(string tipo)
        {
            ViewBag.tipo = tipo;
            return View(db.Productos.ToList());
        }

        public ActionResult Armar()
        {
            return View(db.Productos.ToList());
        }

        // GET: Productos/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Productos productos = db.Productos.Find(id);
            if (productos == null)
            {
                return HttpNotFound();
            }
            return View(productos);
        }

        // GET: Productos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Productos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Titulo,Descripcion,Imagen,Precio,Marca,Tipo")] Productos productos)
        {
            HttpPostedFileBase fileBase = Request.Files[0]; //proporciona el acceso al archivo

            if(fileBase.ContentLength == 0)
            {
                ModelState.AddModelError("Imagen", "Es necesario seleccionar una imagen");
            }
            else
            {

                //if (fileBase.FileName.EndsWith(".jgp"))
                //{
                    WebImage image = new WebImage(fileBase.InputStream);
                    productos.Imagen = image.GetBytes();
                //}
                //else
                //{
                //    ModelState.AddModelError("Imagen", "El sistema solo acepta imagenes con formato .JPG");
                //}

                
            }
           
            if (ModelState.IsValid)
            {
                db.Productos.Add(productos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(productos);
        }

        // GET: Productos/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Productos productos = db.Productos.Find(id);
            if (productos == null)
            {
                return HttpNotFound();
            }
            return View(productos);
        }

        // POST: Productos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Titulo,Descripcion,Imagen,Precio,Marca,Tipo")] Productos productos)
        {
           
            Productos _prod = new Productos();

            HttpPostedFileBase fileBase = Request.Files[0]; 
            if(fileBase.ContentLength == 0)
            {
                _prod = db.Productos.Find(productos.Id);
                productos.Imagen = _prod.Imagen;
            }
            else
            {
                //if (fileBase.FileName.EndsWith(".JPG"))
                //{
                    WebImage imagex = new WebImage(fileBase.InputStream);
                    productos.Imagen = imagex.GetBytes();
                //}
                //else
                //{
                //    ModelState.AddModelError("Imagen", "El sistema solo acepta imagenes con formato .JPG");
                //}
                ////WebImage image = new WebImage(fileBase.InputStream);

                
            }


            if (ModelState.IsValid)
            {
                db.Entry(_prod).State = EntityState.Detached;
                db.Entry(productos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productos);
        }

        // GET: Productos/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Productos productos = db.Productos.Find(id);
            if (productos == null)
            {
                return HttpNotFound();
            }
            return View(productos);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Productos productos = db.Productos.Find(id);
            db.Productos.Remove(productos);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult getImage(int id)
        {
            Productos producto = db.Productos.Find(id);
            byte[] byteImage = producto.Imagen;

            MemoryStream memoryStream = new MemoryStream(byteImage);
            Image image = Image.FromStream(memoryStream);

            memoryStream = new MemoryStream();
            image.Save(memoryStream,ImageFormat.Jpeg);
            memoryStream.Position = 0;

            return File(memoryStream,"image/jpg");
        }


    }
}
