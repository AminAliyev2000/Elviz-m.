using Magnus.Models.Regions;
using Piranha.AttributeBuilder;
using Piranha.Models;
using System.Collections.Generic;
using Piranha.Extend.Fields;

namespace Magnus.Models
{
    [PageType(Title = "Article page")]
    public class ArticlePage : Page<ArticlePage>
    {
        [Region]
        public ImageField Image { get; set; }

        [Region]
        public HtmlField Brief { get; set; }

        [Region(Title = "Tags (use commas to separate values")]
        public StringField Tags { get; set; }

        [Region]
        public DateField Date { get; set; }

        [Region]
        public ImageField Banner { get; set; }

        public string[] GetTags() => Tags?.Value?.Split(",") ?? new string[0];

        [Region]
        public Language Language { get; set; }
    }
}