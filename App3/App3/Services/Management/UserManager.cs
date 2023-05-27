using App3.Entities;
using App3.Services.Interface;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App3.Services.Management
{
    public class UserManager : IUserServices
    {
        public User GetUsers()
        {
            User user = new User() { Age = 26, FullName = "Emre Toksöz", Gender = true, Point = 15 };
            return user;
        }
    }
}
