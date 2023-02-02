using Magnus.Models.Regions;
using Piranha.AttributeBuilder;
using Piranha.Models;
using System.Collections.Generic;
using Piranha.Extend.Fields;
using System.ComponentModel.DataAnnotations;
using System;

namespace Magnus.Models
{
    [PostType(Title = "Blog post", UseBlocks = false)]
    public class BlogPost  : Post<BlogPost>
    {
        [Region(Title = "Main Content")]
        public MarkdownField Body { get; set; }
        /// <summary>
        /// Gets/sets the post hero.
        /// </summary>
        [Region]
        public ImageField Image { get; set; }

        [Region]
        public TextField Author { get; set; }

        [Region]
        public Hero Hero { get; set; }

        [Region]
        public IList<Comment> Comments { get; set; }

        [Region]
        public ImageField Banner { get; set; }

        [Region]
        public Languages Languages { get; set; }
    }

    public class Comment
    {
        [Field]
        public StringField Name { get; set; }

        [Field]
        public StringField Text { get; set; }

        [Field()]
        public DateField Date { get; set; }
    }
}