using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogWebApp.Data;
using BlogWebApp.Models;
using BlogWebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlogWebApp.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            List<User> users = BlogData.Users;

            return View(users);
        }

        public IActionResult UserDetails(int userId)
        {
            User user = BlogData.Users.FirstOrDefault(u => u.Id.Equals(userId));

            return View(user);
        }
    }
}