using App3.Entities;
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
    public class CategoryManager : ICategorySevice
    {
        public Task<bool> AddItemAsync(Category item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Category> GetItemAsync(string id)
        {
            throw new NotImplementedException();
        }

     

        public IEnumerable<Category> GetItems()
        {
            using (ET_AdsContext db= new ET_AdsContext())
            {
                return  db.Categories.ToList();
            }
        }

        public Task<IEnumerable<Category>> GetItemsAsync(bool forceRefresh = false)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Category>> GetProductCategoryAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateItemAsync(Category item)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<Category>> ICategorySevice.GetItems()
        {
            throw new NotImplementedException();
        }
    }
}
