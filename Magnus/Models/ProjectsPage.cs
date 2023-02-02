using Piranha.AttributeBuilder;
using Piranha.Extend.Fields;
using Piranha.Models;
using Magnus.Models.Regions;

namespace Magnus.Models
{
    [PageType(Title = "Projects page")]
    public class ProjectsPage : Page<ProjectsPage>
    {
        [Region]
        public ImageField Banner { get; set; }

        [Region]
        public ImageField Image { get; set; }

        [Region(Title = "Languages")]
        public LanguagesStandard Languages { get; set; }
    }
}