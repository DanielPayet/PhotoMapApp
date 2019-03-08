using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Maps;
using Xamarin.Forms;

namespace PhotoMapApp.Models
{
    public class Post: IEquatable<Post>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Tag> Tags { get; set; }
        public string Image { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Address { get; set; }
        public DateTime DateTime { get; set; }
        public Post() { }

        public Post(string name, string description, List<Tag> tags, string image, double latitude, double longitude, string address, DateTime dateTime)
        {
            this.Name = name;
            this.Description = description;
            this.Name = name;
            this.Tags = new List<Tag>(tags);
            this.Image = image;
            this.Latitude = latitude;
            this.Longitude = longitude;
            this.Address = address;
            this.DateTime = dateTime;
        }

        public Position GetPosition()
        {
            return new Position(this.Latitude, this.Longitude);
        }

        public bool Equals(Post post)
        {
            return Id == post.Id;
        }
    }
}
