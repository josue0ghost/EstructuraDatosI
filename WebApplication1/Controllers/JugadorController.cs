using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Clases;
using WebApplication1.Models;
using System.Diagnostics;
using System.IO;

namespace WebApplication1.Controllers
{
    public class JugadorController : Controller
    {
        // GET: Jugador

        public ActionResult Index(string nameButton)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;
            Log.SendToLog("Opening Index view and showing items from Data.Instance.Jugadores", ts);
            return View(Data.Instance.Jugadores);
        }

        // GET: Jugador/Details/5
        public ActionResult Details(int id)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var jgd = Data.Instance.Jugadores.Where(x => x.id == id).FirstOrDefault();
            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;
            Log.SendToLog("Opening Details view and showing details from item id = " + id + " from list Data.Instance.Jugadores", ts);
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
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
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
                stopwatch.Stop();
                TimeSpan ts = stopwatch.Elapsed;
                Log.SendToLog("Adding new player to Data.Instance.Jugadores from Create view", ts);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Jugador/Edit/5
        public ActionResult Edit(int id)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var jgd = Data.Instance.Jugadores.Where(x => x.id == id).FirstOrDefault();
            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;
            Log.SendToLog("Opening Edit view and showing de values if item id = " + id + " from Data.Instance.Jugadores", ts);
            return View(jgd);
        }

        // POST: Jugador/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                Data.Instance.Jugadores[id].nombre = collection["Nombre"];
                Data.Instance.Jugadores[id].apellido = collection["Apellido"];
                Data.Instance.Jugadores[id].posicion = collection["Posicion"];
                Data.Instance.Jugadores[id].club = collection["Club"];
                Data.Instance.Jugadores[id].salario_base = Convert.ToDouble(collection["Salario_Base"]);
                Data.Instance.Jugadores[id].compensacion_garantizada = Convert.ToDouble(collection["Compensacion_Garantizada"]);
                stopwatch.Stop();
                TimeSpan ts = stopwatch.Elapsed;
                Log.SendToLog("Editing values from item id = " + id + " from Data.Instance.Jugadores", ts);
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
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var jgd = Data.Instance.Jugadores.Where(x => x.id == id).FirstOrDefault();
            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;
            Log.SendToLog("Opening Delete view and showing values from item id = " + id + " from Data.Instance.Jugadores to delete them", ts);
            return View(jgd);
        }

        // POST: Jugador/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                // TODO: Add delete logic here
                Data.Instance.Jugadores.RemoveAt(id);
                stopwatch.Stop();
                TimeSpan ts = stopwatch.Elapsed;
                Log.SendToLog("Deleting values from item id = " + id + " from Data.Instance.Jugadores", ts);
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
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;
            Log.SendToLog("Opening Search view and showing items from Data.Instance.Jugadores ", ts);
            return View(Data.Instance.Jugadores);
        }

        [HttpPost]
        public ActionResult Search(FormCollection collection, string nameButton)
        {
            Stopwatch stopwatch = new Stopwatch();
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
                        stopwatch.Start();
                        if (nombreF != "")
                        {
                            stopwatch.Stop();
                            TimeSpan ts = stopwatch.Elapsed;
                            Log.SendToLog("Showing items from Data.Instance.Jugadores in Search view where their name is " + nombreF, ts);
                            return View(Data.Instance.Jugadores.Where(x => x.nombre == nombreF));
                        }
                        else
                        {
                            ViewBag.Message = string.Format("No se ha ingresado un parametro de busqueda");
                            stopwatch.Stop();
                            TimeSpan ts = stopwatch.Elapsed;
                            Log.SendToLog("Showing items from Data.Instance.Jugadores in Search view", ts);
                            return View(Data.Instance.Jugadores);

                        }
                    case "Buscar por Apellido":
                        stopwatch.Start();
                        if (apellidoF != "")
                        {
                            stopwatch.Stop();
                            TimeSpan ts = stopwatch.Elapsed;
                            Log.SendToLog("Showing items from Data.Instance.Jugadores in Search view where their last name is " + apellidoF, ts);
                            return View(Data.Instance.Jugadores.Where(a => a.apellido == apellidoF));
                        }
                        else
                        {
                            ViewBag.Message = string.Format("No se ha ingresado un parametro de busqueda");
                            stopwatch.Stop();
                            TimeSpan ts = stopwatch.Elapsed;
                            Log.SendToLog("Showing items from Data.Instance.Jugadores in Search view", ts);
                            return View(Data.Instance.Jugadores);
                        }
                    case "Buscar por Posicion":
                        stopwatch.Start();
                        if (posicionF != "")
                        {
                            stopwatch.Stop();
                            TimeSpan ts = stopwatch.Elapsed;
                            Log.SendToLog("Showing items from Data.Instance.Jugadores in Search view where their position is " + posicionF, ts);
                            return View(Data.Instance.Jugadores.Where(b => b.posicion == posicionF));
                        }
                        else
                        {
                            ViewBag.Message = string.Format("No se ha ingresado un parametro de busqueda");
                            stopwatch.Stop();
                            TimeSpan ts = stopwatch.Elapsed;
                            Log.SendToLog("Showing items from Data.Instance.Jugadores in Search view", ts);
                            return View(Data.Instance.Jugadores);
                        }
                    case "Buscar por Salario Mayor":
                        stopwatch.Start();
                        if (salarioF != "")
                        {
                            stopwatch.Stop();
                            TimeSpan ts = stopwatch.Elapsed;
                            Log.SendToLog("Showing items from Data.Instance.Jugadores in Search view where their salary is greater than" + salarioF, ts);
                            return View(Data.Instance.Jugadores.Where(c => c.salario_base > Convert.ToDouble(salarioF)));
                        }
                        else
                        {
                            ViewBag.Message = string.Format("No se ha ingresado un parametro de busqueda");
                            stopwatch.Stop();
                            TimeSpan ts = stopwatch.Elapsed;
                            Log.SendToLog("Showing items from Data.Instance.Jugadores in Search view", ts);
                            return View(Data.Instance.Jugadores);
                        }
                    case "Buscar por Salario Menor":
                        stopwatch.Start();
                        if (salarioF != "")
                        {
                            stopwatch.Stop();
                            TimeSpan ts = stopwatch.Elapsed;
                            Log.SendToLog("Showing items from Data.Instance.Jugadores in Search view where their salary is less than " + salarioF, ts);
                            return View(Data.Instance.Jugadores.Where(d => d.salario_base < Convert.ToDouble(salarioF)));
                        }
                        else
                        {
                            ViewBag.Message = string.Format("No se ha ingresado un parametro de busqueda");
                            stopwatch.Stop();
                            TimeSpan ts = stopwatch.Elapsed;
                            Log.SendToLog("Showing items from Data.Instance.Jugadores in Search view", ts);
                            return View(Data.Instance.Jugadores);
                        }
                    case "Buscar por Salario Igual":
                        stopwatch.Start();
                        if (salarioF != "")
                        {
                            stopwatch.Stop();
                            TimeSpan ts = stopwatch.Elapsed;
                            Log.SendToLog("Showing items from Data.Instance.Jugadores in Search view where their salary is " + salarioF, ts);
                            return View(Data.Instance.Jugadores.Where(f => f.salario_base == Convert.ToDouble(salarioF)));
                        }
                        if (collection["salariofilter"].Equals(""))
                        {
                            ViewBag.Message = string.Format("No se ha ingresado un parametro de busqueda");
                            stopwatch.Stop();
                            TimeSpan ts = stopwatch.Elapsed;
                            Log.SendToLog("Showing items from Data.Instance.Jugadores in Search view", ts);
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
