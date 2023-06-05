using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace App3.Entities.Dto
{
    public  class ItemsViewDto
    {
        public List<Product> Items { get; set; }
        public List<Category> CategoryList { get; set; }
        public Command LoadItemsCommand { get; set; }
    }
}
