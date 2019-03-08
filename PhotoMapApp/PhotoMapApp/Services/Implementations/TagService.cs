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

        public TagService(IDatabase database)
        {
            this._tags = database.GetTags();
            if(_tags.Count == 0) {
                this._tags = new List<Tag> {
                    new Tag("Drink"),
                    new Tag("Food"),
                    new Tag("ToSee")
                };
                this._tags.ForEach(tag => database.UpdateOrSave(tag));
            }
        }

        public List<Tag> GetTags()
        {
            return this._tags;
        }

        public Tag GetTag(int id)
        {
            return this._tags[id];
        }
    }
}
