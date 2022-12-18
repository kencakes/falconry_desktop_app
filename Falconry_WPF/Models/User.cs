using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Falconry_WPF.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        
        public string Username { get; set; }
        public string Password { get; set; }
        // A user can have many birds
        public ICollection<Bird> Birds { get; set; }
    }
}
