using App3.Entities;
using App3.Entities.Dto;
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
        List<Product> GetItemByCategoryId(int id);
        Task<List<Category>> GetProductCategory();
        Task<ProductAndCategoryDto> ProductAndCategory();
    }
}
