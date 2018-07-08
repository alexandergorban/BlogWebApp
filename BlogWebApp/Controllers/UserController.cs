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
        private readonly BlogQueriesService _blogQueriesService;
        public UserController(BlogQueriesService blogQueriesService)
        {
            _blogQueriesService = blogQueriesService;
        }

        public IActionResult Index()
        {
            List<User> users = _blogQueriesService.GetComplexlyFilledUsers();

            return View(users);
        }

        public IActionResult UserDetails(int userId)
        {
            User user = _blogQueriesService.GetComplexlyFilledUser(userId);

            return View(user);
        }
    }
}