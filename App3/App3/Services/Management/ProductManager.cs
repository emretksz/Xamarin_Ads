using App3.Models;
using App3.Services.Interface;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App3.Services.Management
{
    public class ProductManager : IProductService
    {
        public Task<bool> AddItemAsync(Product item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<Product> GetItemAsync(string id)
        {
            using (ET_AdsContext db = new ET_AdsContext())
            {
                return await db.Products.FirstOrDefaultAsync(a=>a.Id.ToString()==id);
            }
        }

        public async Task<IEnumerable<Product>> GetItemsAsync(bool forceRefresh = false)
        {
            using (ET_AdsContext db=new ET_AdsContext() )
            {
                return await db.Products.ToListAsync();
            }
        }
        public IEnumerable<Product> GetAll()
        {
            using (ET_AdsContext db=new ET_AdsContext() )
            {
                return  db.Products.ToList();
            }
        }

        public Task<bool> UpdateItemAsync(Product item)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Product>> GetItemByCategoryIdAsync(int id)
        {
            ///sayfalama eklenecek
            using (ET_AdsContext db = new ET_AdsContext())
            {
                return await db.Products.Where(a=>a.CategoryId==id).ToListAsync();
            }
        }

        public async Task<List<Category>> GetProductCategory()
        {
            try
            {
                using (ET_AdsContext db = new ET_AdsContext())
                {
                    var q = await db.Categories.ToListAsync();
                    return await db.Categories.ToListAsync();
                }
            }
            catch (Exception ex )
            {

                throw;
            }
        }
    }
}
