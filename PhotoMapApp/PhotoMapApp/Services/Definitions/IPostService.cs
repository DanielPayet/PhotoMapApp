﻿using PhotoMapApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PhotoMapApp.Services.Definitions
{
    public interface IPostService
    {
        List<Post> GetPosts();
        Post GetPost(int id);
        void CreatePost(string name, string description, List<Tag> tag, string image, Double latitude, Double longitude, String address, DateTime dateTime);
        void CreatePost(Post post);
        void Delete(Post post);
    }
}
