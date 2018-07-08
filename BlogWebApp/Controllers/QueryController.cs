using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogWebApp.Models;
using BlogWebApp.Services;
using BlogWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BlogWebApp.Controllers
{
    public class QueryController : Controller
    {
        private readonly BlogQueriesService _blogQueriesService;

        private QueryViewModel<List<(Post Post, int Comments)>> numberOfCommentsUnderUserPosts 
            = new QueryViewModel<List<(Post Post, int Comments)>>();
        private QueryViewModel<List<Comment>> shortCommentsUnderUserPosts 
            = new QueryViewModel<List<Comment>>();
        private QueryViewModel<List<(int Id, string Name)>> executedToDoByUser
            = new QueryViewModel<List<(int Id, string Name)>>();
        private QueryViewModel<List<User>> usersAscWithToDoDesc
            = new QueryViewModel<List<User>>();


        public QueryController(BlogQueriesService blogQueriesService)
        {
            _blogQueriesService = blogQueriesService;
        }

        public IActionResult Index()
        {
            return View();
        }

        //1. Show the number of comments under the posts of a particular user(by Id)
        [HttpGet]
        public IActionResult GetNumberOfCommentsUnderUserPosts()
        {
            return View(numberOfCommentsUnderUserPosts);
        }

        [HttpPost]
        public IActionResult GetNumberOfCommentsUnderUserPosts(QueryViewModel<List<(Post Post, int Comments)>> queryViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    queryViewModel.QueryResult = _blogQueriesService.GetNumberOfCommentsUnderUserPosts(queryViewModel.Id);
                    return View(queryViewModel);
                }
                catch (Exception e)
                {
                    queryViewModel.IsExistQueryResult = false;
                    return View(queryViewModel);
                }
            }

            return View(numberOfCommentsUnderUserPosts);
        }

        //2. Show a list of comments under the posts of a particular user (by Id), where body comment < 50 characters (list of comments)
        [HttpGet]
        public IActionResult GetShortCommentsUnderUserPosts()
        {
            return View(shortCommentsUnderUserPosts);
        }

        [HttpPost]
        public IActionResult GetShortCommentsUnderUserPosts(QueryViewModel<List<Comment>> queryViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    queryViewModel.QueryResult = _blogQueriesService.GetShortCommentsUnderUserPosts(queryViewModel.Id);
                    return View(queryViewModel);
                }
                catch (Exception e)
                {
                    queryViewModel.IsExistQueryResult = false;
                    return View(queryViewModel);
                }
            }

            return View(shortCommentsUnderUserPosts);
        }

        //3. Show the list (id, name) from the list of todos that are executed for a specific user (by Id)
        [HttpGet]
        public IActionResult GetExecutedToDoByUser()
        {
            return View(executedToDoByUser);
        }

        [HttpPost]
        public IActionResult GetExecutedToDoByUser(QueryViewModel<List<(int Id, string Name)>> queryViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    queryViewModel.QueryResult = _blogQueriesService.GetExecutedToDoByUser(queryViewModel.Id);
                    return View(queryViewModel);
                }
                catch (Exception e)
                {
                    queryViewModel.IsExistQueryResult = false;
                    return View(queryViewModel);
                }
            }

            return View(executedToDoByUser);
        }

        //4. Show a list of users in alphabetical order (ascending) with sorted To Do items by length name (descending)
        [HttpGet]
        public IActionResult GetUsersAscWithToDoDesc()
        {
            return View(usersAscWithToDoDesc);
        }

        [HttpPost]
        public IActionResult GetUsersAscWithToDoDesc(QueryViewModel<List<User>> queryViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    queryViewModel.QueryResult = _blogQueriesService.GetUsersAscWithToDoDesc();
                    return View(queryViewModel);
                }
                catch (Exception e)
                {
                    queryViewModel.IsExistQueryResult = false;
                    return View(queryViewModel);
                }
            }

            return View(usersAscWithToDoDesc);
        }

        //5. Show the structure: User, Last Post, Number of comments for last post, Tasks in To Do, most popular post
        [HttpGet]
        public IActionResult GetUserData()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetUserData(int id)
        {
            return View();
        }

        //6. Show the structure: Post, Longest comment of the post, Most liked comment on the post, Number of comments under the post where or 0 likes or text length <80 (pass User Id to parameters)
        [HttpGet]
        public IActionResult GetPostData()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetPostData(int id)
        {
            return View();
        }
    }
}