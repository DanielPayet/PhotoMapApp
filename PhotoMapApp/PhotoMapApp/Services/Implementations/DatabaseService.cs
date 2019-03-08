using System;
using System.IO;
using LiteDB;
using PhotoMapApp.Models;
using System.Collections.Generic;

namespace PhotoMapApp.Services.Implementations
{
    public class DatabaseService : IDatabaseService
    {
        private readonly LiteDatabase _database;
        public DatabaseService() {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var dataBasePath = Path.Combine(path, "database.db");
            this._database = new LiteDatabase(dataBasePath);
        }

        public List<Post> GetPosts()
        {
            List<Post> list = new List<Post>(); 
            var collection = this._database.GetCollection<Post>("post");
            var elements = collection.FindAll();
            foreach(Post element in elements)
            {
                list.Add(element);
            }
            return list;
        }

        public void UpdateOrSave(Post post)
        {
            var collection = _database.GetCollection<Post>("post");
            if (!collection.Update(post)) {
                collection.Insert(post);
            }
        }

        public void Delete(Post post)
        {
            var collection = _database.GetCollection<Post>("post");
            collection.Delete(post.Id);
        }

        public List<Tag> GetTags()
        {
            List<Tag> list = new List<Tag>();
            var collection = _database.GetCollection<Tag>("tag");
            var elements = collection.FindAll();
            foreach (Tag element in elements)
            {
                list.Add(element);
            }
            return list;
        }

        public void UpdateOrSave(Tag tag)
        {
            var collection = _database.GetCollection<Tag>("tag");
            if (!collection.Update(tag))
            {
                collection.Insert(tag);
            }
        }

        public void Delete(Tag tag)
        {
            var collection = _database.GetCollection<Tag>("tag");
            collection.Delete(tag.Id);
        }
    }
}
