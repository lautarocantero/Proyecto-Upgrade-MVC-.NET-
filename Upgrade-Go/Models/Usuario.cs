using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using System.Linq;
using System.Web;

namespace Upgrade_Go.Models
{
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }
        [Required]
        public string Mail { get; set; }
        public string Nombre { get; set; }
        public string Contraseña { get; set; }

        public DateTime Cumpleaños { get; set; }

        public int Dni { get; set; }

        public int Tarjeta { get; set; }

        public List<Producto> Productos { get; set; }

    }
}
