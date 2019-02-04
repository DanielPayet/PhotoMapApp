using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PhotoMapApp.Models
{
    class Post
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Tag> Tags { get; set; }
        public string Image { get; set; }

        public Post(string name, string description, List<Tag> tags, string image)
        {
            this.Name = name;
            this.Description = description;
            this.Name = name;
            this.Tags = new List<Tag>(tags);
            this.Image = image;
        }
    }
}
