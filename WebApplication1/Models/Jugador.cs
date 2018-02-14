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
        [Display(Name = "Salario Base")]
        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public double salario_base { get; set; }
        [Display(Name = "Compensacion Garantizada")]
        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public double compensacion_garantizada { get; set; }
        

        public int CompareTo(object obj)
        {
            var Jugador2 = (Jugador)obj;
            return id > Jugador2.id ? 1 : id < Jugador2.id ? -1 : 0;

            //if (id > Jugador2.id)
            //    return 1;
            //if (id < Jugador2.id)
            //    return -1;
            //return 0;
        }
    }
}