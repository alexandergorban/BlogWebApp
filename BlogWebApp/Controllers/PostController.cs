using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogWebApp.Models;
using BlogWebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlogWebApp.Controllers
{
    public class PostController : Controller
    {
        private readonly BlogQueriesService _blogQueriesService;
        public PostController(BlogQueriesService blogQueriesService)
        {
            _blogQueriesService = blogQueriesService;
        } 
        public IActionResult Index()
        {
            List<Post> posts = _blogQueriesService.GetPosts();

            return View(posts);
        }

        public IActionResult PostDetails(int postId)
        {
            Post post = _blogQueriesService.GetPost(postId);

            return View(post);
        }
    }
}