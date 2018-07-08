using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogWebApp.Data;
using BlogWebApp.Models;

namespace BlogWebApp.Services
{
    public class BlogQueriesService
    {
        private readonly List<User> _complexlyFilledUsers;

        public BlogQueriesService()
        {
            _complexlyFilledUsers = BlogData.Users;
        }

        public List<User> GetComplexlyFilledUsers()
        {
            return _complexlyFilledUsers;
        }

        public User GetComplexlyFilledUser(int userId)
        {
            var user = _complexlyFilledUsers
                .FirstOrDefault(u => u.Id.Equals(userId));

            return user;
        }

        public List<Post> GetPosts()
        {
            var posts = _complexlyFilledUsers
                .SelectMany(user => user.Posts)
                .OrderByDescending(post => post.CreatedAt)
                .ToList();

            return posts;
        }

        public Post GetPost(int postId)
        {
            var post = _complexlyFilledUsers
                .SelectMany(user => user.Posts)
                .FirstOrDefault(p => p.Id == postId);

            return post;
        }

        public List<ToDo> GetToDos()
        {
            var toDos = _complexlyFilledUsers
                .SelectMany(user => user.ToDos)
                .OrderByDescending(toDo => toDo.CreatedAt)
                .ToList();

            return toDos;
        }

        public ToDo GetToDo(int todoId)
        {
            var toDo = _complexlyFilledUsers
                .SelectMany(user => user.ToDos)
                .FirstOrDefault(t => t.Id == todoId);

            return toDo;
        }

        //1. Get the number of comments under the posts of a particular user(by Id)
        public List<(Post Post, int Comments)> GetNumberOfCommentsUnderUserPosts(int userId)
        {
            var data = _complexlyFilledUsers
                .Where(user => user.Id == userId)
                .SelectMany(user => user.Posts)
                .Select(post => (Post: post, CommentsCount: post.Comments.Count))
                .ToList();

            return data;
        }

        //2. Get a list of comments under the posts of a particular user (by Id), where body comment < 50 characters (list of comments)
        public List<Comment> GetShortCommentsUnderUserPosts(int userId)
        {
            var data = _complexlyFilledUsers
                .Where(user => user.Id == userId)
                .SelectMany(user => user.Posts)
                .SelectMany(post => post.Comments)
                .Where(comment => comment.Body.Length < 50)
                .ToList();

            return data;
        }

        //3. Get the list (id, name) from the list of todos that are executed for a specific user (by Id)
        public List<(int Id, string Name)> GetExecutedToDoByUser(int userId)
        {
            var data = _complexlyFilledUsers
                .Where(user => user.Id == userId)
                .SelectMany(user => user.ToDos)
                .Where(toDo => toDo.IsComplete)
                .Select(toDo => (Id: toDo.Id, Name: toDo.Name))
                .ToList();

            return data;
        }

        //4. Get a list of users in alphabetical order (ascending) with sorted To Do items by length name (descending)
        public List<User> GetUsersAscWithToDoDesc()
        {
            var data = _complexlyFilledUsers
                .OrderBy(user => user.Name)
                .Select(user =>
                {
                    user.ToDos = user.ToDos
                        .OrderByDescending(toDo => toDo.Name.Length)
                        .ToList();
                    return user;
                })
                .ToList();

            return data;
        }

        //5. Get the structure: User, Last Post, Number of comments for last post, Tasks in To Do, most popular post
        public (User User,
            Post LastPost,
            int LastPostCommentsCount,
            int UncompletedTodosCount,
            Post PostWithMaxComments,
            Post PostWithMaxLikes) GetUserData(int userId)
        {
            var data = _complexlyFilledUsers
                .Where(user => user.Id == userId)
                .Select(user => (
                    User: user,
                    LastPost: user.Posts
                        .OrderByDescending(post => post.CreatedAt)
                        .FirstOrDefault(),
                    LastPostCommentsCount: user.Posts
                        .OrderByDescending(post => post.CreatedAt)
                        .First().Comments.Count,
                    UncompletedTodosCount: user.ToDos
                        .Count(toDo => !toDo.IsComplete),
                    PostWithMaxComments: user.Posts
                        .OrderByDescending(post =>
                            post.Comments.Count(comment => comment.Body.Length > 80))
                        .FirstOrDefault(),
                    PostWithMaxLikes: user.Posts
                        .OrderByDescending(post => post.Likes)
                        .FirstOrDefault()
                )).Single();

            return data;
        }

        //6. Get the structure: Post, Longest comment of the post, Most liked comment on the post, Number of comments under the post where or 0 likes or text length <80 (pass User Id to parameters)
        public (Post Post,
            Comment LongestComment,
            Comment MostLikedComment,
            int MostUnpopularCommentsCount) GetPostData(int postId)
        {
            var data = _complexlyFilledUsers
                .SelectMany(user => user.Posts)
                .Where(post => post.Id == postId)
                .Select(post => (
                    Post: post,
                    LongestComment: post.Comments
                        .OrderByDescending(comment => comment.Body)
                        .FirstOrDefault(),
                    MostLikedComment: post.Comments
                        .OrderByDescending(comment => comment.Likes)
                        .FirstOrDefault(),
                    MostUnpopularCommentsCount: post.Comments
                        .Count(comment => comment.Likes == 0 || comment.Body.Length < 80)
                )).Single();

            return data;
        }
    }
}
