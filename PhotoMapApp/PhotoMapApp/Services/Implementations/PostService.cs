using PhotoMapApp.Models;
using PhotoMapApp.Services.Definitions;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoMapApp.Services.Implementations
{
    class PostService: IPostService
    {
        private List<Post> _posts { get; set; }

        public PostService()
        {
            this._posts = new List<Post>();
        }

        public List<Post> getPosts()
        {
            return this._posts;
        }

        public Post getPost(int id)
        {
            return this._posts[id];
        }

        public void createPost(string name, string description, List<Tag> tags, string image)
        {
            this._posts.Add(new Post(name, description, tags, image));
        }
    }
}
