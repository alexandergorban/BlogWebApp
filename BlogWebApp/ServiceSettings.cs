using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogWebApp
{
    public static class ServiceSettings
    {
        public static class BlogSource
        {
            public static string Url = "https://5b128555d50a5c0014ef1204.mockapi.io/";

            public static class Endpoints
            {
                public static string Users = "users";
                public static string Posts = "posts";
                public static string Comments = "comments";
                public static string ToDos = "todos";
                public static string Address = "address";
            }
        }
    }
}
