using Magnus.Models.Regions;
using Piranha.AttributeBuilder;
using Piranha.Models;
using System.Collections.Generic;
using Piranha.Extend.Fields;

namespace Magnus.Models
{
    [PageType(Title = "Links page", UseBlocks = false)]
    public class LinksPage : Page<LinksPage>
    {
        [Region(ListExpand = false)]
        public IList<Link> Links { get; set; }

        [Region]
        public ImageField Banner { get; set; }

        [Region]
        public Languages Languages { get; set; }
    }

    public class Link
    {
        [Field]
        public StringField Title { get; set; }
        [Field]
        public StringField Url { get; set; }
    }
}
