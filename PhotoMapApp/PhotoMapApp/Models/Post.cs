using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PhotoMapApp.Models
{
    public class Post
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Tag> Tags { get; set; }
        public string Image { get; set; }
        public Double Latitude { get; set; }
        public Double Longitude { get; set; }
        public String Address { get; set; }
        public DateTime DateTime { get; set; }

        public Post(string name, string description, List<Tag> tags, string image, Double latitude, Double longitude, String address, DateTime dateTime)
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
    }
}
