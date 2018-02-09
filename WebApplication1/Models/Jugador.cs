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
        [Display(Name = "Nombre")]
        public string nombre { get; set; }
        [Display(Name = "Apellido")]
        public string apellido { get; set; }
        [Display(Name = "Posicion")]
        public string posicion { get; set; }
        [Display(Name = "Club")]
        public string club { get; set; }
        [Display (Name = "Salario Base")]
        public double salario_base { get; set; }
        [Display(Name = "Compensacion Garantizada")]
        public double compensacion_garantizada { get; set; }
    }
}