using App3;
using App3.Models;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Entities.Models
{
    public class ET_AdsContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        //private string connectionString = "Data Source=server_name;Initial Catalog=database_name;User ID=username;Password=password;";    
   
        protected async override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"");
            string connectionString = @"server=";
            SqlConnection connection = new SqlConnection(connectionString);
            await connection.OpenAsync();
        }
            //optionsBuilder.UseSqlServer($"Filename={App.DatabasePath}");
    }
}
