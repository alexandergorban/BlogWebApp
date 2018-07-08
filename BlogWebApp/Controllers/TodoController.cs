using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogWebApp.Models;
using BlogWebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlogWebApp.Controllers
{
    public class TodoController : Controller
    {
        private readonly BlogQueriesService _blogQueriesService;

        public TodoController(BlogQueriesService blogQueriesService)
        {
            _blogQueriesService = blogQueriesService;
        }

        public IActionResult Index()
        {
            List<ToDo> toDos = _blogQueriesService.GetToDos();

            return View(toDos);
        }

        public IActionResult TodoDetails(int todoId)
        {
            ToDo toDo = _blogQueriesService.GetToDo(todoId);

            return View(toDo);
        }
    }
}