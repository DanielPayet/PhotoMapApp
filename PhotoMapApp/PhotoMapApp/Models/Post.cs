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
        public Tag Tag { get; set; }
        public string Image { get; set; }
        public Double Latitude { get; set; }
        public Double Longitude { get; set; }
        public String Address { get; set; }
        public DateTime DateTime { get; set; }

        public Post(string name, string description,Tag tag, string image, Double latitude, Double longitude, String address, DateTime dateTime)
        {
            this.Name = name;
            this.Description = description;
            this.Name = name;
            this.Tag = tag;
            this.Image = image;
            this.Latitude = latitude;
            this.Longitude = longitude;
            this.Address = address;
            this.DateTime = dateTime;
        }
    }
}
