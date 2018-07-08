using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BlogWebApp.Interfaces;
using BlogWebApp.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SQLitePCL;

namespace BlogWebApp.Services
{
    public class BlogLoadService
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;

        public BlogLoadService(IConfiguration configuration, HttpClient client)
        {
            _configuration = configuration;
            _client = client;
        }

        private async Task<List<T>> GetDataCollectionByEndpointAsync<T>(string endpoint) where T : IEndpoint
        {
            List<T> data;

            using (HttpResponseMessage response = await _client.GetAsync(ServiceSettings.BlogSource.Url + endpoint))
            {
                string result = await response.Content.ReadAsStringAsync();
                data = JsonConvert.DeserializeObject<List<T>>(result);
            }

            return data;
        }

        private async Task<List<User>> GetUsersAsync()
        {
            return await GetDataCollectionByEndpointAsync<User>(ServiceSettings.BlogSource.Endpoints.Users);
        }

        private async Task<List<Post>> GetPostsAsync()
        {
            return await GetDataCollectionByEndpointAsync<Post>(ServiceSettings.BlogSource.Endpoints.Posts);
        }

        private async Task<List<Comment>> GetCommentsAsync()
        {
            return await GetDataCollectionByEndpointAsync<Comment>(ServiceSettings.BlogSource.Endpoints.Comments);
        }

        private async Task<List<Address>> GetAddressesAsync()
        {
            return await GetDataCollectionByEndpointAsync<Address>(ServiceSettings.BlogSource.Endpoints.Address);
        }

        private async Task<List<ToDo>> GetToDosAsync()
        {
            return await GetDataCollectionByEndpointAsync<ToDo>(ServiceSettings.BlogSource.Endpoints.ToDos);
        }

        async Task<List<User>> GetComplexlyFilledUsers()
        {
            List<User> users = await GetUsersAsync();
            List<Post> posts = await GetPostsAsync();
            List<Comment> comments = await GetCommentsAsync();
            List<Address> addresses = await GetAddressesAsync();
            List<ToDo> todos = await GetToDosAsync();

            posts = (from post in posts
                     join comment in comments on post.Id equals comment.PostId into userCommentsGroup
                     select new Post()
                     {
                         Id = post.Id,
                         UserId = post.UserId,
                         CreatedAt = post.CreatedAt,
                         Title = post.Title,
                         Body = post.Body,
                         Likes = post.Likes,
                         Comments = userCommentsGroup.ToList(),
                     }).ToList();

            users = (from user in users
                     join post in posts on user.Id equals post.UserId into userPostsGroup
                     join todo in todos on user.Id equals todo.UserId into userToDosGroup
                     join comment in comments on user.Id equals comment.UserId into userCommentsGroup
                     join address in addresses on user.Id equals address.UserId
                     select new User()
                     {
                         Id = user.Id,
                         CreatedAt = user.CreatedAt,
                         Name = user.Name,
                         Avatar = user.Avatar,
                         Email = user.Email,
                         Address = address,
                         Posts = userPostsGroup.ToList(),
                         Comments = userCommentsGroup.ToList(),
                         ToDos = userToDosGroup.ToList()
                     }).ToList();

            return users;
        }
    }
}
