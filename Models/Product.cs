using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace InventorySystemDay2.Models
{
    [Table(nameof(Product))]
    public class Product
    {
        [Key]
        [Column(TypeName ="int(10)")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [Column(TypeName ="varchar(60)")]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "int(10)")]
        public int Quantity { get; set; } = 0;

        [Column(TypeName ="boolean")]
        public bool Discontinued { get; set; } = false;
    }
}
