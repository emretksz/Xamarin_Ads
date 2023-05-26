using App3.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App3.Services.Interface
{
    public interface IProductService:IDataStore<Product>
    {
         IEnumerable<Product> GetAll();
        Task<IEnumerable<Product>> GetItemByCategoryIdAsync(int id);
        Task<List<Category>> GetProductCategory();
    }
}
