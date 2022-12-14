using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Falconry_WPF.Models
{
    public class Bird
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Breed { get; set; }
        public string Latin { get; set; }
        
        // A can have  has multiple logbooks
        public ICollection<Logbook> Logbooks { get; set; }
        
        // A bird has one owner
        public int UserId { get; set; }
    }
}
