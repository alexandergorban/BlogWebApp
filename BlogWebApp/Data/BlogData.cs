using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogWebApp.Models;
using BlogWebApp.Services;

namespace BlogWebApp.Data
{
    public static class BlogData
    {
        public static List<User> Users { get; set; }

        public static async void LoadData()
        {
            BlogLoadService blogLoadService = new BlogLoadService();
            Users = await blogLoadService.GetComplexlyFilledUsers();
        }
    }
}
