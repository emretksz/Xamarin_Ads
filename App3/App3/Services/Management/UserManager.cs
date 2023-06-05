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
    public class UserManager : IUserServices
    {
        public async Task<string> CheckUserForCreate(User user)
        {
            if (user==null)
            {
                return "0";
            }
            using (ET_AdsContext db = new ET_AdsContext())
            {
                var checkUser = await db.Users.FirstOrDefaultAsync(a => a.UserName == user.UserName);
                if (checkUser!=null)
                {
                    return "-1";
                }
            }
            return "1";
        }

        public async Task<string> AddUser(User user)
        {
            try
            {
                if (user == null)
                {
                    return "0";
                }
                using (ET_AdsContext db = new ET_AdsContext())
                {
                    user.RegisterDate = DateTime.UtcNow;
                    user.BonesPointDay = DateTime.UtcNow;

                    db.Users.Add(user);
                    await db.SaveChangesAsync();
                }
                return "1";

            }
            catch(Exception ex)
            {
                return "0";
            }
        }

        public User GetUsers()
        {
            User user = new User() { Age = 26, FullName = "Emre Toksöz", Gender = true, Point = 15 };
            return user;
        }
    }
}
