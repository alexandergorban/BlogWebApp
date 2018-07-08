using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogWebApp.Models;

namespace BlogWebApp.ViewModels
{
    public class PostQueryViewModel
    {
        public Post Post { get; set; }
        public Comment LongestComment { get; set; }
        public Comment MostLikedComment { get; set; }
        public int MostUnpopularCommentsCount { get; set; }
    }
}
