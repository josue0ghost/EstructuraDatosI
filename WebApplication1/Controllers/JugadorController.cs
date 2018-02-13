using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Clases;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class JugadorController : Controller
    {
        /*public List<Jugador> listado = new List<Jugador> {
            new Jugador{ id = 1, nombre = ""}
        };*/
        // GET: Jugador
        public ActionResult Index()
        {
            return View(Data.Instance.Jugadores);
        }

        // GET: Jugador/Details/5
        public ActionResult Details(int id)
        {
            var jgd = Data.Instance.Jugadores.Where(x => x.id == id).FirstOrDefault();
            return View(jgd);
        }

        // GET: Jugador/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Jugador/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                Data.Instance.Jugadores.Add(new Jugador
                {
                    id = Data.Instance.Jugadores.Count,
                    nombre = collection["Nombre"],
                    apellido = collection["Apellido"],
                    posicion = collection["Posicion"],
                    club = collection["club"],
                    salario_base = Convert.ToDouble(collection["Salario_Base"]),
                    compensacion_garantizada = Convert.ToDouble(collection["Compensacion_Garantizada"])
                });
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        /*[HttpPost]
        public ActionResult CreatewithArchive(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }*/

        // GET: Jugador/Edit/5
        public ActionResult Edit(int id)
        {
            var jgd = Data.Instance.Jugadores.Where(x => x.id == id).FirstOrDefault();
            return View(jgd);
        }

        // POST: Jugador/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                Data.Instance.Jugadores[id].nombre = collection["Nombre"];
                Data.Instance.Jugadores[id].apellido = collection["Apellido"];
                Data.Instance.Jugadores[id].posicion = collection["Posicion"];
                Data.Instance.Jugadores[id].club = collection["Club"];
                Data.Instance.Jugadores[id].salario_base = Convert.ToDouble(collection["Salario_Base"]);
                Data.Instance.Jugadores[id].compensacion_garantizada = Convert.ToDouble(collection["Compensacion_Garantizada"]);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Jugador/Delete/5
        public ActionResult Delete(int id)
        {
            var jgd = Data.Instance.Jugadores.Where(x => x.id == id).FirstOrDefault();
            return View(jgd);
        }

        // POST: Jugador/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                Data.Instance.Jugadores.RemoveAt(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        //public static List<Jugador> temp = Data.Instance.Jugadores;
        public ActionResult Search()
        {
            return View(Data.Instance.Jugadores);
        }

        [HttpPost]
        public ActionResult Search(FormCollection collection, string nameButton)
        {
            try
            {
                // TODO: Add delete logic here
                var nombreF = collection["nombrefilter"];
                var posicionF = collection["posicionfilter"];
                var apellidoF = collection["apellidofilter"];
                var salarioF = collection["salariofilter"];
                switch (nameButton)
                {
                    case "Buscar por Nombre":
                        if (nombreF != "")
                        {
                            return View(Data.Instance.Jugadores.Where(x => x.nombre == nombreF));
                        }
                        if(collection["nombrefilter"].Equals(""))
                        {
                            ViewBag.Message = string.Format("No se ha ingresado un parametro de busqueda");
                            return View(Data.Instance.Jugadores);
                        }
                        break;
                    case "Buscar por Apellido":
                        if (apellidoF != "")
                        {
                            return View(Data.Instance.Jugadores.Where(a => a.apellido == apellidoF));
                        }
                        if (collection["apellidofilter"].Equals(""))
                        {
                            ViewBag.Message = string.Format("No se ha ingresado un parametro de busqueda");
                            return View(Data.Instance.Jugadores);
                        }
                        break;
                    case "Buscar por Posicion":
                        if (posicionF != "")
                        {
                            return View(Data.Instance.Jugadores.Where(b => b.posicion == posicionF));
                        }
                        if (collection["posicionfilter"].Equals(""))
                        {
                            ViewBag.Message = string.Format("No se ha ingresado un parametro de busqueda");
                            return View(Data.Instance.Jugadores);
                        }
                        break;
                    case "Buscar por Salario Mayor":
                        if (salarioF != "")
                        {
                            return View(Data.Instance.Jugadores.Where(c => c.salario_base > Convert.ToDouble(salarioF)));
                        }
                        if (collection["salariofilter"].Equals(""))
                        {
                            ViewBag.Message = string.Format("No se ha ingresado un parametro de busqueda");
                            return View(Data.Instance.Jugadores);
                        }
                        break;
                    case "Buscar por Salario Menor":
                        if (salarioF != "")
                        {
                            return View(Data.Instance.Jugadores.Where(d => d.salario_base < Convert.ToDouble(salarioF)));
                        }
                        if (collection["salariofilter"].Equals(""))
                        {
                            ViewBag.Message = string.Format("No se ha ingresado un parametro de busqueda");
                            return View(Data.Instance.Jugadores);
                        }
                        break;
                    case "Buscar por Salario Igual":
                        if (salarioF != "")
                        {
                            return View(Data.Instance.Jugadores.Where(f => f.salario_base == Convert.ToDouble(salarioF)));
                        }
                        if (collection["salariofilter"].Equals(""))
                        {
                            ViewBag.Message = string.Format("No se ha ingresado un parametro de busqueda");
                            return View(Data.Instance.Jugadores);
                        }
                        break;
                }
                return RedirectToAction("Search");
            }
            catch
            {
                return View();
            }
        }
    }
}
