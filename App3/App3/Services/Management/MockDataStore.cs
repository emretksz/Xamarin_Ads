using App3.Entities;
using App3.Services.Interface;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App3.Services.Management
{
    public class MockDataStore : IDataStore<Product>
    {
        //readonly List<Item> items;
        readonly List<Product> items;

        public MockDataStore()
        {

            //items = new List<Product>()
            //{
            //    new Product { Id = 1.ToString(), Text = "First item", Description="This is an item description." },
            //    new Item { Id = 2.ToString(), Text = "Second item", Description="This is an item description." },
            //    new Item { Id = 3.ToString(), Text = "Third item", Description="This is an item description." },
            //    new Item { Id = 4.ToString(), Text = "Fourth item", Description="This is an item description." },
            //    new Item { Id = 5.ToString(), Text = "Fifth item", Description="This is an item description." },
            //    new Item { Id = 6.ToString(), Text = "Sixth item", Description="This is an item description." }
            //};
        }

        public async Task<bool> AddItemAsync(Product item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Product item)
        {
            var oldItem = items.Where((arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((arg) => arg.Id.ToString() == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Product> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id.ToString() == id));
        }

        public async Task<IEnumerable<Product>> GetItemsAsync(bool forceRefresh = false)
        {
            using (ET_AdsContext db = new ET_AdsContext())
            {
                return await db.Products.ToListAsync();
            }
        }

        public async Task<IEnumerable<Product>> GetItemByCategoryIdAsync(int id)
        {
            using (ET_AdsContext db = new ET_AdsContext())
            {
                return await db.Products.Where(a => a.CategoryId == id).ToListAsync();
            }
        }
    }
}