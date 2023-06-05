using System;
using System.Collections.Generic;
using System.Text;

namespace App3.Entities.Dto
{
    public  class ProductAndCategoryDto
    {
        public List<Product> Product { get; set; }
        public List<Category> Category { get; set; 
        }public Product ProductSimple { get; set; }
        public Category CategorySimple { get; set; }
    }
}
