using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogWebApp.Models;

namespace BlogWebApp.ViewModels
{
    public class UserQueryViewModel
    {
        public User User { get; set; }
        public Post LastPost { get; set; }
        public int LastPostCommentsCount { get; set; }
        public int UncompletedTodosCount { get; set; }
        public Post PostWithMaxComments { get; set; }
        public Post PostWithMaxLikes { get; set; }
    }
}
