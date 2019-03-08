using PhotoMapApp.Models;
using PhotoMapApp.Services.Definitions;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PhotoMapApp.Services.Implementations
{
    public class PostService: IPostService
    {
        private IDatabase _databaseService;
        private List<Post> _posts { get; set; }

        public PostService(IDatabase database)
        {
            _databaseService = database;
            this._posts = _databaseService.GetPosts();
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
            CreatePost(new Post(name, description, tags, image, latitude, longitude, address, dateTime));
        }

        public void CreatePost(Post post)
        {
            this._databaseService.UpdateOrSave(post);
            this._posts.Add(post);
        }
    }
}
