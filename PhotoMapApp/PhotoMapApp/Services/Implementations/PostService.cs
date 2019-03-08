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
        private IDatabaseService _databaseService;
        private List<Post> _posts { get; set; }

        public PostService(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
            _posts = _databaseService.GetPosts();
        }

        public List<Post> GetPosts()
        {
            return _posts;
        }

        public Post GetPost(int id)
        {
            return _posts[id];
        }

        public void CreatePost(string name, string description, List<Tag> tags, string image, Double latitude, Double longitude, String address, DateTime dateTime)
        {
            CreatePost(new Post(name, description, tags, image, latitude, longitude, address, dateTime));
        }

        public void CreatePost(Post post)
        {
            _databaseService.UpdateOrSave(post);
            _posts.Add(post);
        }

        public void Delete(Post post)
        {
            _databaseService.Delete(post);
            _posts.Remove(post);
        }
    }
}
