using System;
using System.Collections.Generic;
using System.Text;

namespace App3.Entities
{
    public class User
    {
        public int Id { get; set; }
        public bool Gender { get; set; }
        public int Point { get; set; }
        public int Reference { get; set; }
        public bool IsActive { get; set; }
        public DateTime RegisterDate { get; set; }
        public string Password { get; set; }
        public string AddUserCount { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public DateTime BonesPointDay { get; set; }
    }
}
