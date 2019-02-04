using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoMapApp.Models
{
    public class Tag
    {
        public string Name { get; set; }

        public Tag(string name)
        {
            this.Name = '#' + name;
        }
    }
}
