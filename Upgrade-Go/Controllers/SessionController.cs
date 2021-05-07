using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Upgrade_Go.Models;

namespace Upgrade_Go.Controllers
{
    public class SessionController : Controller
    {
        // GET: Session
        public ActionResult Index()
        {
            return View();
        }


        //[Authorize]
        //public ActionResult Agregar(string producto, string precio, string marca)
        //{
        //    if (producto != null)
        //    {
        //        List<ProductoCarrito> carrito;

        //        //almacenar en la sesion
        //        if (Session["Producto"] != null)
        //        {
        //            carrito = Session["Producto"] as List<ProductoCarrito>;
        //        }
        //        else
        //        {
        //            //crear nueva lista
        //            carrito = new List<ProductoCarrito>();
        //        }

        //        ProductoCarrito productofinal = new ProductoCarrito();
        //        productofinal.Titulo = producto;
        //        productofinal.Precio = precio;
        //        productofinal.Marca = marca;
        //        carrito.Add(productofinal);

        //        Session["Producto"] = carrito;

        //    }
        //    return RedirectToAction("Inicio", "Productos");
        //}


        [Authorize]
        public ActionResult Agregar(int producto)
        {

            List<Productos> carrito;

            //almacenar en la sesion
            if (Session["Producto"] != null)
            {
                carrito = Session["Producto"] as List<Productos>;
            }
            else
            {
                //crear nueva lista
                carrito = new List<Productos>();
            }
            DataBase db = new DataBase();
            var productofinal = db.Productos.FirstOrDefault(e => e.Id == producto);

            //foreach (var elemento in carrito)
            //{
            //    if (elemento.Titulo.ToString() == productofinal.ToString())
            //    {

            //        productofinal.cantidad++;
            //    }
            //    else
            //    {
            //        productofinal.cantidad = 1;
            //    }
            //}

            carrito.Add(productofinal);

            Session["Producto"] = carrito;


            return RedirectToAction("Inicio", "Productos");
        }


        [Authorize]
        [HttpPost]
        public ActionResult Pack(int Auriculares = 0 , int Monitor = 0, int Teclado = 0, int Mouse = 0, int Ram = 0, int DiscoDuro= 0)
        {
            if (Auriculares >= 1 || Monitor >= 1 || Teclado >= 1 || Mouse >= 1 || Ram >= 1 || DiscoDuro >= 1)
            {
                ArrayList productos = new ArrayList();


                if (Auriculares >= 1)
                {
                    productos.Add(Auriculares);
                }
                if (Monitor >= 1)
                {
                    productos.Add(Monitor);
                }
                if (Teclado >= 1)
                {
                    productos.Add(Teclado);
                }
                if (Mouse >= 1)
                {
                    productos.Add(Mouse);
                }
                if (Ram >= 1)
                {
                    productos.Add(Ram);
                }
                if (DiscoDuro >= 1)
                {
                    productos.Add(DiscoDuro);
                }
                foreach (int prod in productos)
                {
                    Agregar(prod);
                }

            }

            return RedirectToAction("Compra", "Session");
        }

        //obtener lista
        public ActionResult Compra()
        {
            return View(Session["Producto"]);
        }

        public ActionResult Borrar()
        {
            Session["Producto"] = null;

            return RedirectToAction("Compra");
        }
    }
}


