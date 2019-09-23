using Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FirstProject.MVC.Models
{
    public class ShippersViewModel
    {
        public int Shipperid { get; set; }
        [MaxLength(20)]
        public string Companyname { get; set; }
        public string Phone { get; set; }

        public virtual ICollection<Orders> Orders { get; set; }

    }
}
