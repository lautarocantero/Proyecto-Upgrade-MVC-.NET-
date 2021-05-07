using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Upgrade_Go.Models;

namespace Upgrade_Go.Controllers
{
    public class UsuariosController : Controller
    {
        private DataBase db = new DataBase();

        // GET: Usuarios
        public ActionResult Index()
        {
            return View(db.Usuarios.ToList());
        }

        public ActionResult Ayuda()
        {
            return View("Ayuda");
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuarios usuarios = db.Usuarios.Find(id);
            if (usuarios == null)
            {
                return HttpNotFound();
            }
            return View(usuarios);
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Mail,Nombre,Contraseña,Cumpleaños,Dni,Tarjeta")] Usuarios usuarios)
        {
            if (ModelState.IsValid)
            {
                db.Usuarios.Add(usuarios);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(usuarios);
        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuarios usuarios = db.Usuarios.Find(id);
            if (usuarios == null)
            {
                return HttpNotFound();
            }
            return View(usuarios);
        }

        // POST: Usuarios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Mail,Nombre,Contraseña,Cumpleaños,Dni,Tarjeta")] Usuarios usuarios)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuarios).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(usuarios);
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuarios usuarios = db.Usuarios.Find(id);
            if (usuarios == null)
            {
                return HttpNotFound();
            }
            return View(usuarios);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Usuarios usuarios = db.Usuarios.Find(id);
            db.Usuarios.Remove(usuarios);
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

        public ActionResult Cuenta(string email)
        {

            string emailUsuario;

            if (Session["Usuario"] != null)
            {
                emailUsuario = Session["Usuario"].ToString();
            }
            else
            {
                emailUsuario = email;
            }
            Session["Usuario"] = emailUsuario;
            ViewBag.email = Session["Usuario"];
            return View("Cuenta", db.Usuarios.ToList());
        }

        [HttpPost]
        public ActionResult Login(string email, string contrasena)
        {

            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(contrasena))
            {
                DataBase db = new DataBase();
                var user = db.Usuarios.FirstOrDefault(e => e.Mail == email && e.Contraseña == contrasena);

                if (user != null) //encontrado
                {
                    FormsAuthentication.SetAuthCookie(user.Mail, true);
                    return RedirectToAction("Cuenta","Usuarios", new { email = email });
                }
                else
                {
                    return RedirectToAction("Cuenta", new { mensaje = "No encontramos tus datos" });
                }
            }
            else
            {
                return RedirectToAction("Cuenta", new { mensaje = "Llena los campos para iniciar sesion" });
            }

        }
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session["Usuario"] = null;
            return RedirectToAction("Cuenta");
        }


    }
}
