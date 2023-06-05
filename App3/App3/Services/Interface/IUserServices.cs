using App3.Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App3.Services.Interface
{
    public interface IUserServices
    {
        User GetUsers();
         Task<string>CheckUserForCreate(User user);
        Task<string> AddUser(User user);
    }
}
