using Piranha.AttributeBuilder;
using Piranha.Extend.Fields;
using Piranha.Models;

namespace Magnus.Models
{
    [PageType(Title = "Standard page")]
    public class StandardPage : Page<StandardPage>
    {
        [Region]
        public ImageField Banner { get; set; }

        [Region(Title = "Languages")]
        public LanguagesStandard Languages { get; set; }
    }

    public class LanguagesStandard
    {
        [Field]
        public TextField RusTitle { get; set; }

        [Field]
        public TextField KazTitle { get; set; }

        [Field]
        public HtmlField RusContent { get; set; }

        [Field]
        public HtmlField KazContent { get; set; }
    }
}