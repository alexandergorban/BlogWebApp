using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogWebApp.Models;

namespace BlogWebApp.Services
{
    public class BlogQueriesService
    {
        private readonly List<User> _complexlyFilledUsers;

        public BlogQueriesService(List<User> complexlyFilledUsers)
        {
            _complexlyFilledUsers = complexlyFilledUsers;
        }

        public List<(Post Post, int Comments)> GetNumberOfCommentsUnderUserPosts(int userId)
        {
            var data = _complexlyFilledUsers
                .Where(user => user.Id == userId)
                .SelectMany(user => user.Posts)
                .Select(post => (Post: post, CommentsCount: post.Comments.Count))
                .ToList();

            return data;
        }

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
