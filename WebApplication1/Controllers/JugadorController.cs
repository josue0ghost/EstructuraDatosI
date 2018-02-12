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
                    id = Data.Instance.Jugadores.Count + 1,
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

        // GET: Jugador/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Jugador/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logiche re
                /*Jugador ed = Data.Instance.Jugadores.ElementAt(id);
                collection["Nombre"] = "Willson";
                collection["Apellido"] = ed.apellido;
                collection["Posicion"] = ed.posicion;*/
                Data.Instance.Jugadores.Insert(id, new Jugador
                {
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

        [HttpGet]
        public ActionResult UploadFile()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file)
        {
            Archivo archivo = new Archivo();
            List<string[]> ReadedLines = new List<string[]>();
            try
            {
                if (!file.FileName.EndsWith(".csv"))                
                    return View();
                
                if (file.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(file.FileName);
                    string path = Path.Combine(Server.MapPath("~/UploadedFiles"), fileName);
                    file.SaveAs(path);

                    ReadedLines = archivo.Lectura(path);
                }

                for (int i = 0; i < ReadedLines.Count; i++)
                    if (ReadedLines[i].Contains("club") ||
                        ReadedLines[i].Contains("last_name") ||
                        ReadedLines[i].Contains("first_name") ||
                        ReadedLines[i].Contains("position") ||
                        ReadedLines[i].Contains("base_salary") ||
                        ReadedLines[i].Contains("guaranteed_compensation")
                        )
                        ReadedLines.RemoveAt(i);

                foreach (var item in ReadedLines)
                {
                    Data.Instance.Jugadores.Add(new Jugador
                    {
                        id = Data.Instance.Jugadores.Count,
                        club = item[0],
                        apellido = item[1],
                        nombre = item[2],
                        posicion = item[3],
                        salario_base = Convert.ToDouble(item[4]),
                        compensacion_garantizada = Convert.ToDouble(item[5])

                    });                                
                }

                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.Message = "Error al subir el archivo";
                return View();
            }
        }
    }

    class Archivo
    {
        public List<string[]> Lectura(string sRuta)
        {
            List<string[]> listItems = new List<string[]>();
            List<string> lSLineas = new List<string>();

            StreamReader srLector = new StreamReader(sRuta, Encoding.Default);
            string sLineas = srLector.ReadLine();
            while (sLineas != null)
            {
                lSLineas.Add(sLineas);
                sLineas = srLector.ReadLine();
            }
            srLector.Close();

            foreach (var item in lSLineas)
            {
                string[] Line = item.Split(',');
                listItems.Add(Line);
            }
            lSLineas.Clear();
            return listItems;
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
