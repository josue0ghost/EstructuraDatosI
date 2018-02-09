using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Jugador
    {
        [Key]
        public int id { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string club { get; set; }
        public string posicion { get; set; }
        public decimal salario_base { get; set; }
        public decimal compensacion_garantizada { get; set; }
    }
}