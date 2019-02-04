using PhotoMapApp.Models;
using PhotoMapApp.Services.Definitions;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoMapApp.Services.Implementations
{
    public class PostService: IPostService
    {
        private List<Post> _posts { get; set; }

        public PostService()
        {
            this._posts = new List<Post>();
        }

        public List<Post> GetPosts()
        {
            return this._posts;
        }

        public Post GetPost(int id)
        {
            return this._posts[id];
        }

        public void CreatePost(string name, string description, List<Tag> tags, string image, Double latitude, Double longitude, String address, DateTime dateTime)
        {
            this._posts.Add(new Post(name, description, tags, image, latitude, longitude, address, dateTime));
        }
    }
}
