using Magnus.Models.Regions;
using Piranha.AttributeBuilder;
using Piranha.Models;
using System.Collections.Generic;
using Piranha.Extend.Fields;

namespace Magnus.Models
{
    [PageType(Title = "Events Main page", UseBlocks = false)]
    [PageTypeRoute(Title = "Default", Route = "/events")]
    public class EventsMainPage : Page<EventsMainPage>
    {
        [Region(ListTitle = "Events", ListPlaceholder = "List of main events of the project")]
        public IList<Event> OutcomePoints { get; set; }

        [Region]
        public ImageField Banner { get; set; }

        [Region(Title = "Languages")]
        public Languages Languages { get; set; }
    }

    public class Event
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