using Magnus.Models.Regions;
using Piranha.AttributeBuilder;
using Piranha.Models;
using System.Collections.Generic;
using Piranha.Extend.Fields;

namespace Magnus.Models
{
    [PageType(Title = "Partners page", UseBlocks = false)]
    [PageTypeRoute(Title = "Default", Route = "/partners")]
    public class PartnersPage : Page<PartnersPage>
    {
        [Region(ListTitle = "Partners")]
        public IList<Partner> Partners { get; set; }

        [Region]
        public ImageField Banner { get; set; }

        [Region]
        public Languages Languages { get; set; }
    }


}