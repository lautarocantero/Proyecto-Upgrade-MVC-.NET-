using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.Threading.Tasks;

namespace Upgrade_Go.Models
{
    public class Producto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }
        [Required]
        public string Titulo { get; set; }
        [Required]
        public string Descripcion { get; set; }

        public byte[] Imagen { get; set; }
        [Required]

        public float Precio { get; set; }

        public string Marca { get; set; }

        public string Tipo { get; set; }

    }
}