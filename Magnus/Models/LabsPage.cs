using Magnus.Models.Regions;
using Piranha.AttributeBuilder;
using Piranha.Models;
using System.Collections.Generic;
using Piranha.Extend.Fields;

namespace Magnus.Models
{
    [PageType(Title = "Labs page")]
    public class LabsPage : Page<LabsPage>
    {
        [Region(ListTitle = "Labs")]
        public IList<Lab> Labs { get; set; }

        [Region]
        public ImageField Banner { get; set; }

        [Region(Title = "Languages")]
        public LanguagesStandard Languages { get; set; }
    }
}
