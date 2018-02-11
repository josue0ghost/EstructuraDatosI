using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
            return View();
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
            return View();
        }

        // POST: Jugador/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            try
            {
                // TODO: Add insert logic here
                if (!file.FileName.EndsWith(".csv"))
                    return View();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }

    class Archivo
    {
        public List<string> Lectura(string sRuta)
        {
            List<string> lSLineas = new List<string>();
            StreamReader srLector = new StreamReader(sRuta, Encoding.Default);
            string sLineas = srLector.ReadLine();
            while (sLineas != null)
            {
                lSLineas.Add(sLineas);
                sLineas = srLector.ReadLine();
            }
            srLector.Close();
            return lSLineas;
        }

        private void EscrituraArchivo(string sRuta, List<string> lSDatos)
        {
            StreamWriter swEscritor = new StreamWriter(sRuta, false, Encoding.Default);
            foreach (string slinea in lSDatos)
            {
                swEscritor.WriteLine(slinea);
            }
            swEscritor.Close();
        }
    }
}
