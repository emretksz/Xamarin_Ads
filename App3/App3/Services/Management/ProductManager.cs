using App3.Entities;
using App3.Entities.Dto;
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
        public List<Product> GetItemByCategoryId(int id)
        {
            ///sayfalama eklenecek
            using (ET_AdsContext db = new ET_AdsContext())
            {
                return db.Products.Where(a=>a.CategoryId==id).ToList();
            }
        }

        public async Task<List<Category>> GetProductCategory()
        {
            try
            {
                using (ET_AdsContext db = new ET_AdsContext())
                {
                    return await db.Categories.ToListAsync();
                }
            }
            catch (Exception ex )
            {

                throw;
            }
        }

        public async Task<ProductAndCategoryDto> ProductAndCategory()
        {

            //TEST İÇİN YAZDIM BBURASI KONTROL EDİLECEK! ŞU AN ÇALIŞIYOR
            ProductAndCategoryDto dto = new ProductAndCategoryDto();
            using (ET_AdsContext db = new ET_AdsContext())
            {
                var result = await (from a in db.Products
                              join b in db.Categories on a.CategoryId equals b.Id into categoryGroup
                              from c in categoryGroup.DefaultIfEmpty()
                              select new ProductAndCategoryDto
                              {
                                  ProductSimple = a,
                                  CategorySimple = c
                              }).AsNoTracking().ToListAsync();

                dto.Product = new List<Product>();
                dto.Category = new List<Category>();
                foreach (var item in result)
                {
                    dto.Product.Add(item.ProductSimple);
                    dto.Category.Add(item.CategorySimple);
                }
                return dto;
            }
        }
    }
}
