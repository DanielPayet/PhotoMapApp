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
        private List<Post> _posts { get; set; }

        public PostService(IImageService imageService)
        {
            this._posts = new List<Post> {
                new Post("Post de test", "Ceci est une description", new List<Tag>{new Tag("THOMINOU"), new Tag("Drink") }, imageService.GetSource("profil.png"), 1.2948848, 43.39494, "Rue du gros prout de Daniel", DateTime.Now),
                new Post("Post de test", "Ceci est une description", new List<Tag>{new Tag("Drink"), new Tag("Food") }, imageService.GetSource("profil.png"), 1.2948848, 43.39494, "Rue du gros prout de Daniel", DateTime.Now.AddDays(-1)),
                new Post("Post de test", "Ceci est une description", new List<Tag>{new Tag("THOMINOU")}, imageService.GetSource("profil.png"), 1.2948848, 43.39494, "Rue du gros prout de Daniel", DateTime.Now.AddDays(-8)),
                new Post("Post de test", "Ceci est une description", new List<Tag>{new Tag("THOMINOU")}, imageService.GetSource("profil.png"), 1.2948848, 43.39494, "Rue du gros prout de Daniel", DateTime.Now.AddDays(-35)),
                new Post("Post de test", "Ceci est une description", new List<Tag>{new Tag("THOMINOU")}, imageService.GetSource("profil.png"), 1.2948848, 43.39494, "Rue du gros prout de Daniel", DateTime.Now.AddDays(-845))
            };
        }

        public List<Post> GetPosts()
        {
            return this._posts;
        }

        public Post GetPost(int id)
        {
            return this._posts[id];
        }

        public void CreatePost(string name, string description, List<Tag> tags, ImageSource image, Double latitude, Double longitude, String address, DateTime dateTime)
        {
            this._posts.Add(new Post(name, description, tags, image, latitude, longitude, address, dateTime));
        }

        public void AddPost(Post post)
        {
            this._posts.Add(post);
        }
    }
}
