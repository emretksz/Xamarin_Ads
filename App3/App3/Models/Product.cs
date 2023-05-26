using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace App3.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string Point { get; set; }
        public string ImageURL { get; set; }
        public DateTime RegsterDate { get; set; }
        public bool IsActive { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
