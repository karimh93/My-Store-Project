using Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FirstProject.MVC.Models
{
    public class CategoriesViewModel
    {
        public int Categoryid { get; set; }
        [Required]
        public string Categoryname { get; set; }
        [MaxLength(30)]
        public string Description { get; set; }

        public virtual ICollection<Products> Products { get; set; }


    }
}
