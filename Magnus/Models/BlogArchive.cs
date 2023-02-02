using Magnus.Models.Regions;
using Piranha.AttributeBuilder;
using Piranha.Models;
using Piranha.Extend.Fields;

namespace Magnus.Models
{
    [PageType(Title = "Blog archive", UseBlocks = false)]
    public class BlogArchive  : ArchivePage<BlogArchive>
    {
        /// <summary>
        /// Gets/sets the archive hero.
        /// </summary>
        [Region]
        public Hero Hero { get; set; }

        [Region]
        public ImageField Banner { get; set; }

        [Region]
        public Languages Languages { get; set; }
    }
}