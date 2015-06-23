using InterbankV2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterbankV2.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember on this computer")]
        public bool RememberMe { get; set; }

        public virtual ICollection<Simulador> Simuladores { get; set; }
    }

    public class UsuariosContext : DbContext
    {
        public UsuariosContext()
            : base("SimuladorContext")
        {

        }
        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Simulador> Simuladors { get; set; }
    }
}
