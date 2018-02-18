using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Clases;
using WebApplication1.Models;
using EstructurasDeDatosLineales;

namespace WebApplication1.Controllers
{
    public class JugadorController : Controller
    {
        Archivo archivo = new Archivo();
        Methods method = new Methods();

        #region C#List

        public ActionResult Home()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;
            Log.beginLog();
            Log.SendToLog("Opening Home view", ts);
            return View();
        }

        // GET: Jugador
        public ActionResult Index()
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
                if((collection["Club"] == "") || (collection["Salario_Base"] == ""))
                {
                    var jgd = Data.Instance.Jugadores.Where(x => x.id == id).FirstOrDefault();
                    ViewBag.Message = string.Format("No puede dejar el parametro de club o salario base vacio");
                    stopwatch.Stop();
                    TimeSpan ts = stopwatch.Elapsed;
                    Log.SendToLog("Trying to edit values from item id = " + id + " from Data.Instance.Jugadores but failed", ts);
                    return View(jgd);
                }
                else
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
                    case "Mostrar Lista Completa":
                        stopwatch.Start();
                        stopwatch.Stop();
                        TimeSpan tst = stopwatch.Elapsed;
                        Log.SendToLog("Showing items from Data.Instance.lJugador in Search view", tst);
                        return View(Data.Instance.lJugadores);
                }
                return RedirectToAction("Search");
            }
            catch
            {
                return View();
            }
        }


        // GET: Jugador/UploadFile
        [HttpGet]
        public ActionResult UploadFile()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;
            Log.SendToLog("Opening UploadFile view", ts);
            return View();
        }

        // POST: Jugador/UploadFile
        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            
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

                    method.DeleteInnecesaryLine(ReadedLines);
                    method.AddToList(ReadedLines, true);
                }

                stopwatch.Stop();
                TimeSpan ts = stopwatch.Elapsed;
                Log.SendToLog("Reading uploaded file and adding new players to Data.Instance.Jugadores from UploadFile view", ts);

                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.Message = "Error al subir el archivo";

                stopwatch.Stop();
                TimeSpan ts = stopwatch.Elapsed;
                Log.SendToLog("Showing error at upload file from UploadFile view", ts);

                return View();
            }
        }

        // GET: Jugador/UploadFileDelete
        [HttpGet]
        public ActionResult UploadFileDelete()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;
            Log.SendToLog("Opening UploadFile view", ts);
            return View();
        }

        // POST: Jugador/UploadFileDelete
        [HttpPost]
        public ActionResult UploadFileDelete(HttpPostedFileBase file)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
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

                    method.DeleteInnecesaryLine(ReadedLines);
                    method.DeleteFromList(ReadedLines, true);
                }

                stopwatch.Stop();
                TimeSpan ts = stopwatch.Elapsed;
                Log.SendToLog("Reading uploaded file and removing players from Data.Instance.Jugadores from UploadFileDelete view", ts);
                return RedirectToAction("Index");
            }
            catch
            {
                stopwatch.Stop();
                TimeSpan ts = stopwatch.Elapsed;
                Log.SendToLog("Showing error at upload file from UploadFileDelete view", ts);
                ViewBag.Message = "Error al subir el archivo";
                return View();
            }
        }

        #endregion

        #region Artesanal List
        // GET: Jugador
        public ActionResult IndexAL()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;
            Log.SendToLog("Opening Index view and showing items from Data.Instance.lJugadores", ts);
            return View(Data.Instance.lJugadores);
            //Data.Instance.Jugadores
        }

        // GET: Jugador/Details/5
        public ActionResult DetailsAL(int id)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var jgd = Data.Instance.lJugadores.Where(x => x.id == id);
            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;
            Log.SendToLog("Opening Details view and showing details from item id = " + id + " from Data.Instance.lJugador", ts);
            return View(jgd.First.value);
        }

        // GET: Jugador/Create
        public ActionResult CreateAL()
        {
            return View();
        }

        // POST: Jugador/Create
        [HttpPost]
        public ActionResult CreateAL(FormCollection collection)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                // TODO: Add insert logic here
                Data.Instance.lJugadores.Insertar(new Jugador
                {
                    id = Data.Instance.lJugadores.Count,
                    nombre = collection["Nombre"],
                    apellido = collection["Apellido"],
                    posicion = collection["Posicion"],
                    club = collection["club"],
                    salario_base = Convert.ToDouble(collection["Salario_Base"]),
                    compensacion_garantizada = Convert.ToDouble(collection["Compensacion_Garantizada"])
                });
                stopwatch.Stop();
                TimeSpan ts = stopwatch.Elapsed;
                Log.SendToLog("Adding new player to Data.Instance.lJugador from Create view", ts);
                return RedirectToAction("IndexAL");
            }
            catch
            {
                return View();
            }

        }

        // GET: Jugador/Edit/5
        public ActionResult EditAL(int id)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var jgd = Data.Instance.lJugadores.Where(x => x.id == id);
            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;
            Log.SendToLog("Opening Edit view and showing de values if item id = " + id + " from Data.Instance.lJugador", ts);
            return View(jgd.First.value);
        }

        // POST: Jugador/Edit/5
        [HttpPost]
        public ActionResult EditAL(int id, FormCollection collection)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                if ((collection["Club"] == "") || (collection["Salario_Base"] == ""))
                {
                    ViewBag.Message = string.Format("No puede dejar el parametro de club o salario base vacio");
                    var jgd = Data.Instance.lJugadores.Where(x => x.id == id);
                    stopwatch.Stop();
                    TimeSpan ts = stopwatch.Elapsed;
                    Log.SendToLog("Trying to edit values from item id = " + id + " from Data.Instance.lJugadores but failed", ts);
                    return View(jgd.First.value);
                }
                else
                {
                    Data.Instance.lJugadores.Obtener(id).value.nombre = collection["Nombre"];
                    Data.Instance.lJugadores.Obtener(id).value.apellido = collection["Apellido"];
                    Data.Instance.lJugadores.Obtener(id).value.posicion = collection["Posicion"];
                    Data.Instance.lJugadores.Obtener(id).value.club = collection["Club"];
                    Data.Instance.lJugadores.Obtener(id).value.salario_base = Convert.ToDouble(collection["Salario_Base"]);
                    Data.Instance.lJugadores.Obtener(id).value.compensacion_garantizada = Convert.ToDouble(collection["Compensacion_Garantizada"]);
                    stopwatch.Stop();
                    TimeSpan ts = stopwatch.Elapsed;
                    Log.SendToLog("Editing values from item id = " + id + " from Data.Instance.lJugador", ts);
                    return RedirectToAction("IndexAL");
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Jugador/Delete/5
        public ActionResult DeleteAL(int id)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var jgd = Data.Instance.lJugadores.Where(x => x.id == id);
            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;
            Log.SendToLog("Opening Delete view and showing values from item id = " + id + " from Data.Instance.lJugador to delete them", ts);
            return View(jgd.First.value);
        }

        // POST: Jugador/Delete/5
        [HttpPost]
        public ActionResult DeleteAL(int id, FormCollection collection)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                // TODO: Add delete logic here
                Data.Instance.lJugadores.Eliminar(id);
                stopwatch.Stop();
                TimeSpan ts = stopwatch.Elapsed;
                Log.SendToLog("Deleting values from item id = " + id + " from Data.Instance.lJugador", ts);
                return RedirectToAction("IndexAL");
            }
            catch
            {
                return View();
            }
        }

        //public static List<Jugador> temp = Data.Instance.Jugadores;
        public ActionResult SearchAL()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;
            Log.SendToLog("Opening Search view and showing items from Data.Instance.lJugador ", ts);
            return View(Data.Instance.lJugadores);
        }

        [HttpPost]
        public ActionResult SearchAL(FormCollection collection, string nameButton)
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
                            var jgd = Data.Instance.lJugadores.Where(x => x.nombre == nombreF);
                            stopwatch.Stop();
                            TimeSpan ts = stopwatch.Elapsed;
                            Log.SendToLog("Showing items from Data.Instance.lJugador in Search view where their name is " + nombreF, ts);
                            return View(jgd);
                        }
                        else
                        {
                            ViewBag.Message = string.Format("No se ha ingresado un parametro de busqueda");
                            stopwatch.Stop();
                            TimeSpan ts = stopwatch.Elapsed;
                            Log.SendToLog("Showing items from Data.Instance.lJugador in Search view", ts);
                            return View(Data.Instance.lJugadores);

                        }
                    case "Buscar por Apellido":
                        stopwatch.Start();
                        if (apellidoF != "")
                        {
                            var jgd = Data.Instance.lJugadores.Where(a => a.apellido == apellidoF);
                            stopwatch.Stop();
                            TimeSpan ts = stopwatch.Elapsed;
                            Log.SendToLog("Showing items from Data.Instance.lJugador in Search view where their last name is " + apellidoF, ts);
                            return View(jgd);
                        }
                        else
                        {
                            ViewBag.Message = string.Format("No se ha ingresado un parametro de busqueda");
                            stopwatch.Stop();
                            TimeSpan ts = stopwatch.Elapsed;
                            Log.SendToLog("Showing items from Data.Instance.lJugador in Search view", ts);
                            return View(Data.Instance.lJugadores);
                        }
                    case "Buscar por Posicion":
                        stopwatch.Start();
                        if (posicionF != "")
                        {
                            var jgd = Data.Instance.lJugadores.Where(b => b.posicion == posicionF);
                            stopwatch.Stop();
                            TimeSpan ts = stopwatch.Elapsed;
                            Log.SendToLog("Showing items from Data.Instance.lJugador in Search view where their position is " + posicionF, ts);
                            return View(jgd);
                        }
                        else
                        {
                            ViewBag.Message = string.Format("No se ha ingresado un parametro de busqueda");
                            stopwatch.Stop();
                            TimeSpan ts = stopwatch.Elapsed;
                            Log.SendToLog("Showing items from Data.Instance.lJugador in Search view", ts);
                            return View(Data.Instance.lJugadores);
                        }
                    case "Buscar por Salario Mayor":
                        stopwatch.Start();
                        if (salarioF != "")
                        {
                            var jgd = Data.Instance.lJugadores.Where(c => c.salario_base > Convert.ToDouble(salarioF));
                            stopwatch.Stop();
                            TimeSpan ts = stopwatch.Elapsed;
                            Log.SendToLog("Showing items from Data.Instance.lJugador in Search view where their salary is greater than" + salarioF, ts);
                            return View(jgd);
                        }
                        else
                        {
                            ViewBag.Message = string.Format("No se ha ingresado un parametro de busqueda");
                            stopwatch.Stop();
                            TimeSpan ts = stopwatch.Elapsed;
                            Log.SendToLog("Showing items from Data.Instance.lJugador in Search view", ts);
                            return View(Data.Instance.lJugadores);
                        }
                    case "Buscar por Salario Menor":
                        stopwatch.Start();
                        if (salarioF != "")
                        {
                            var jgd = Data.Instance.lJugadores.Where(d => d.salario_base < Convert.ToDouble(salarioF));
                            stopwatch.Stop();
                            TimeSpan ts = stopwatch.Elapsed;
                            Log.SendToLog("Showing items from Data.Instance.lJugador in Search view where their salary is less than " + salarioF, ts);
                            return View(jgd);
                        }
                        else
                        {
                            ViewBag.Message = string.Format("No se ha ingresado un parametro de busqueda");
                            stopwatch.Stop();
                            TimeSpan ts = stopwatch.Elapsed;
                            Log.SendToLog("Showing items from Data.Instance.lJugador in Search view", ts);
                            return View(Data.Instance.lJugadores);
                        }
                    case "Buscar por Salario Igual":
                        stopwatch.Start();
                        if (salarioF != "")
                        {
                            var jgd = Data.Instance.lJugadores.Where(f => f.salario_base == Convert.ToDouble(salarioF));
                            stopwatch.Stop();
                            TimeSpan ts = stopwatch.Elapsed;
                            Log.SendToLog("Showing items from Data.Instance.lJugador in Search view where their salary is " + salarioF, ts);
                            return View(jgd);
                        }
                        if (collection["salariofilter"].Equals(""))
                        {
                            ViewBag.Message = string.Format("No se ha ingresado un parametro de busqueda");
                            stopwatch.Stop();
                            TimeSpan ts = stopwatch.Elapsed;
                            Log.SendToLog("Showing items from Data.Instance.lJugador in Search view", ts);
                            return View(Data.Instance.lJugadores);
                        }
                        break;
                    case "Mostrar Lista Completa":
                        stopwatch.Start();
                        stopwatch.Stop();
                        TimeSpan tst = stopwatch.Elapsed;
                        Log.SendToLog("Showing items from Data.Instance.lJugador in Search view", tst);
                        return View(Data.Instance.lJugadores);
                }
                return RedirectToAction("IndexAL", Data.Instance.lJugadores);
            }
            catch
            {
                return View();
            }
        }

        // GET: Jugador/UploadFile
        [HttpGet]
        public ActionResult UploadFileAL()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;
            Log.SendToLog("Opening UploadFile view", ts);
            return View();
        }

        // POST: Jugador/UploadFile
        [HttpPost]
        public ActionResult UploadFileAL(HttpPostedFileBase file)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

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

                    method.DeleteInnecesaryLine(ReadedLines);
                    method.AddToList(ReadedLines, false);
                }

                stopwatch.Stop();
                TimeSpan ts = stopwatch.Elapsed;
                Log.SendToLog("Reading uploaded file and adding new players to Data.Instance.lJugador from UploadFile view", ts);

                return RedirectToAction("IndexAL");
            }
            catch
            {
                ViewBag.Message = "Error al subir el archivo";

                stopwatch.Stop();
                TimeSpan ts = stopwatch.Elapsed;
                Log.SendToLog("Showing error at upload file from UploadFile view", ts);

                return View();
            }
        }

        // GET: Jugador/UploadFileDelete
        [HttpGet]
        public ActionResult UploadFileDeleteAL()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;
            Log.SendToLog("Opening UploadFileDeleteAL view", ts);
            return View();
        }

        // POST: Jugador/UploadFileDelete
        [HttpPost]
        public ActionResult UploadFileDeleteAL(HttpPostedFileBase file)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
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

                    method.DeleteInnecesaryLine(ReadedLines);
                    method.DeleteFromList(ReadedLines, false);
                }

                stopwatch.Stop();
                TimeSpan ts = stopwatch.Elapsed;
                Log.SendToLog("Reading uploaded file and removing players from Data.Instance.lJugadores from UploadFileDeleteAL view", ts);
                return RedirectToAction("IndexAL");
            }
            catch(Exception ex)
            {
                stopwatch.Stop();
                TimeSpan ts = stopwatch.Elapsed;
                Log.SendToLog("Showing error at upload file from UploadFileDeleteAL view", ts);
                ViewBag.Message = "Error al subir el archivo";
                return View();
            }
        }

        #endregion
    }

    class Methods : Controller
    {
        public void DeleteInnecesaryLine(List<string[]> ReadedLines)
        {
            for (int i = 0; i < ReadedLines.Count; i++)
                if (ReadedLines[i].Contains("club") &&
                    ReadedLines[i].Contains("last_name") &&
                    ReadedLines[i].Contains("first_name") &&
                    ReadedLines[i].Contains("position") &&
                    ReadedLines[i].Contains("base_salary") &&
                    ReadedLines[i].Contains("guaranteed_compensation")
                    )
                    ReadedLines.RemoveAt(i);
        }

        public void AddToList(List<string[]> ReadedLines, bool List) //true C#List - false Artesanal List
        {
            foreach (var item in ReadedLines)
            {
                if (List)
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
                else
                {
                    Data.Instance.lJugadores.Insertar(new Jugador
                    {
                        id = Data.Instance.lJugadores.Count,
                        club = item[0],
                        apellido = item[1],
                        nombre = item[2],
                        posicion = item[3],
                        salario_base = Convert.ToDouble(item[4]),
                        compensacion_garantizada = Convert.ToDouble(item[5])
                    });
                }
            }
        }

        public void DeleteFromList(List<string[]> ReadedLines, bool List) //true C#List - false Artesanal List
        {
            foreach (var item in ReadedLines)
            {
                if (List)
                {
                    var jgdr = Data.Instance.Jugadores.Find(jug => jug.club == item[0] && jug.apellido == item[1] &&
                                                        jug.nombre == item[2] && jug.posicion == item[3] &&
                                                        jug.salario_base.Equals(Convert.ToDouble(item[4])) &&
                                                        jug.compensacion_garantizada.Equals(Convert.ToDouble(item[5])));
                    Data.Instance.Jugadores.Remove(jgdr);
                }
                else
                {
                    Jugador jgdr = new Jugador
                    {
                        club = item[0],
                        apellido = item[1],
                        nombre = item[2],
                        posicion = item[3],
                        salario_base = Convert.ToDouble(item[4]),
                        compensacion_garantizada = Convert.ToDouble(item[5])
                    };
                    var j = Data.Instance.lJugadores.Find(jug => jug.club == item[0] && jug.apellido == item[1] &&
                                                        jug.nombre == item[2] && jug.posicion == item[3] &&
                                                        jug.salario_base.Equals(Convert.ToDouble(item[4])) &&
                                                        jug.compensacion_garantizada.Equals(Convert.ToDouble(item[5])));
                    jgdr.id = j.First.value.id;

                    for (int i = 0; i < Data.Instance.lJugadores.Count; i++)
                    {
                        var player = Data.Instance.lJugadores.ElementAt(i);
                        if (player.id == jgdr.id)
                        {
                            Data.Instance.lJugadores.Eliminar(i);
                        }
                    }
                }         
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
    }
}