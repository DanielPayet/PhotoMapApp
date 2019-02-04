using PhotoMapApp.Models;
using PhotoMapApp.Services.Definitions;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoMapApp.Services.Implementations
{
    class TagService: ITagService
    {
        private List<Tag> _tags { get; set; }

        public TagService()
        {
            this._tags = new List<Tag> {
                new Tag("Drink"),
                new Tag("Food"),
                new Tag("ToSee")
            };
        }

        public List<Tag> getTags()
        {
            return this._tags;
        }

        public Tag getTag(int id)
        {
            return this._tags[id];
        }
    }
}
