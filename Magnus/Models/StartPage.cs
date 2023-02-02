using Magnus.Models.Regions;
using Piranha.AttributeBuilder;
using Piranha.Extend.Fields;
using Piranha.Models;
using System.Collections.Generic;

namespace Magnus.Models
{
    [PageType(Title = "Start page")]
    [PageTypeRoute(Title = "Default", Route = "/start")]
    public class StartPage : Page<StartPage>
    {
        [Region]
        public ImageField Banner { get; set; }

        [Region]
        public HtmlField Description { get; set; }

        [Region]
        public About About { get; set; }

        [Region(Title = "Languages")]
        public LanguagesStart Languages { get; set; }
    }

    public class About
    {
        [Field]
        public ImageField Image { get; set; }

        [Field]
        public HtmlField Text { get; set; }
    }

    public class LanguagesStart
    {
        [Field]
        public TextField RusTitle { get; set; }

        [Field]
        public TextField KazTitle { get; set; }

        [Field]
        public HtmlField RusDescription { get; set; }

        [Field]
        public HtmlField KazDescription { get; set; }

        [Field]
        public HtmlField RusAbout { get; set; }

        [Field]
        public HtmlField KazAbout { get; set; }
    }
}