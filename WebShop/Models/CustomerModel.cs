using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Models
{
    public class CustomerModel
    {
        [Key]
        public int CustomerId { get; set; }

        [Required]
        [Column("Name")]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [Column("Street")]
        [MaxLength(50)]
        public string Street { get; set; }

        [Required]
        [Column("ZipCode")]
        [MaxLength(50)]
        public int ZipCode { get; set; }

        [Required]
        [Column("City")]
        [MaxLength(50)]
        public string City { get; set; }

        [Required]
        [Column("Phone")]
        [MaxLength(50)]
        public string Phone { get; set; }

        [Required]
        [Column("Email")]
        [MaxLength(50)]
        public string Email { get; set; }

        [Required]
        [Column("UserName")]
        [MaxLength(50)]
        public string UserName { get; set; }

        [Required]
        [Column("Password")]
        [MaxLength(50)]
        public string Password { get; set; }
    }
}
