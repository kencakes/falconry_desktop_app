using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Falconry_WPF.Models
{
    public class Logbook
    {
        [Key]
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public int Weight { get; set; }
        public int FoodAmount { get; set; }
        public bool Vitamins { get; set; }
        public string Activity { get; set; }
        public string Comment { get; set; }
        public User User { get; set; }
    }
}
