using Magnus.Models.Regions;
using Piranha.AttributeBuilder;
using Piranha.Models;
using System.Collections.Generic;
using Piranha.Extend.Fields;

namespace Magnus.Models
{
    [PageType(Title = "Events", UseBlocks = true)]
    public class EventsPage : Page<EventsPage>
    {
        [Region]
        public Behavior Behavior { get; set; }

        [Region]
        public ImageField Banner { get; set; }

        [Region(Title = "Languages")]
        public LanguagesStandard Languages { get; set; }
    }    
}