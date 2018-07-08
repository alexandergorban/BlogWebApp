using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogWebApp.Models;
using BlogWebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlogWebApp.Controllers
{
    public class GeneralController : Controller
    {
        private readonly BlogQueriesService _blogQueriesService;

        public GeneralController(BlogQueriesService blogQueriesService)
        {
            _blogQueriesService = blogQueriesService;
        }

        public IActionResult Index()
        {
            List<User> users = _blogQueriesService.GetComplexlyFilledUsers();

            return View(users);
        }
    }
}