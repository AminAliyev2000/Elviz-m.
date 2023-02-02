using Magnus.Models.Regions;
using Piranha.AttributeBuilder;
using Piranha.Models;
using System.Collections.Generic;
using Piranha.Extend.Fields;


namespace Magnus.Models
{
    [PageType(Title = "Outcomes page", UseBlocks = false)]
    [PageTypeRoute(Title = "Default", Route = "/outcomes")]
    public class OutcomesPage : Page<OutcomesPage>
    {
        [Region(ListTitle = "Outcomes", ListPlaceholder = "List of main outcomes of the project")]
        public IList<Outcome> OutcomePoints { get; set; }

        [Region]
        public ImageField Banner { get; set; }

        [Region]
        public Languages Languages { get; set; }
    }

    public class Outcome
    {
        [Field(Title = "Title")]
        public StringField Title { get; set; }
        [Field(Title = "Russian title")]
        public StringField TitleRus { get; set; }
        [Field(Title = "Kazakhstan title")]
        public StringField TitleKaz { get; set; }
        [Field(Title = "Image")]
        public ImageField Image { get; set; }
        [Field(Title = "Content")]
        public HtmlField Content { get; set; }
        [Field(Title = "Russian content")]
        public HtmlField ContentRus { get; set; }
        [Field(Title = "Kazakhstan content")]
        public HtmlField ContentKaz { get; set; }
        [Field(Title = "Color")]
        public StringField Color { get; set; }
        [Field(Title = "Url")]
        public StringField Url { get; set; }
    }
}
