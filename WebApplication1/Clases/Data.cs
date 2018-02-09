using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.Clases
{
    public class Data
    {
        private static Data instance = null;

        public static Data Instance
        {
            get {
                if (instance == null) instance = new Data();
                return instance;
            }
        }

        public List<Jugador> Jugadores = new List<Jugador>();
    }
}