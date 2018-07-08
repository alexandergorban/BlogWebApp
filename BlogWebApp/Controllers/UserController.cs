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
        private readonly BlogLoadService _blogLoadService;

        public UserController(BlogLoadService blogLoadService)
        {
            _blogLoadService = blogLoadService;
        }

        public IActionResult Index()
        {
            List<User> users = BlogData.Users;

            return View(users);
        }
    }
}