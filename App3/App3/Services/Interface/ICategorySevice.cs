using App3.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App3.Services.Interface
{
    public interface ICategorySevice:IDataStore<Category>
    {
        Task<IEnumerable<Category>> GetProductCategoryAsync();
        Task<IEnumerable<Category>> GetItems();

    }
}
