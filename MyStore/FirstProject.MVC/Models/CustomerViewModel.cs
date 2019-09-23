using Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FirstProject.MVC.Models
{
    public class CustomerViewModel
    {
        public int Custid { get; set; }
        [Required]
        [StringLength(15)]
        public string Companyname { get; set; }

        //public string Contactname { get; set; }
        [Required]
        public string Contacttitle { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Region { get; set; }
        [Required]
        public string Postalcode { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Fax { get; set; }

        [Required]
        [MinLength(2)]
        public string FirstName { get; set; }
        [Required]
        [MinLength(5)]
        public string LastName { get; set; }

        public virtual ICollection<Orders> Orders { get; set; }
    }
}
