using System;
using System.Collections.Generic;
using System.Text;

namespace App3.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public List<Product> Product { get; set; }
    }
}
