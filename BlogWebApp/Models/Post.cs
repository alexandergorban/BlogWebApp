using System;
using System.Collections.Generic;
using System.Text;
using BlogWebApp.Interfaces;

namespace BlogWebApp.Models
{
    public class Post : IEndpoint
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public int Likes { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
