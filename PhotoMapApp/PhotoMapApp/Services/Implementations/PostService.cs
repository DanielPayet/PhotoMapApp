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
                new Post("Post de test", "Ceci est une description", new Tag("THOMINOU"), imageService.GetSource("profil.png"), 1.2948848, 43.39494, "Rue du gros prout de Daniel", DateTime.Now),
                new Post("Post de test", "Ceci est une description", new Tag("THOMINOU"), imageService.GetSource("profil.png"), 1.2948848, 43.39494, "Rue du gros prout de Daniel", DateTime.Now),
                new Post("Post de test", "Ceci est une description", new Tag("THOMINOU"), imageService.GetSource("profil.png"), 1.2948848, 43.39494, "Rue du gros prout de Daniel", DateTime.Now),
                new Post("Post de test", "Ceci est une description", new Tag("THOMINOU"), imageService.GetSource("profil.png"), 1.2948848, 43.39494, "Rue du gros prout de Daniel", DateTime.Now)
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

        public void CreatePost(string name, string description, Tag tag, ImageSource image, Double latitude, Double longitude, String address, DateTime dateTime)
        {
            this._posts.Add(new Post(name, description, tag, image, latitude, longitude, address, dateTime));
        }
    }
}
