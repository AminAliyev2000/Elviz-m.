using Piranha.Extend;
using Piranha.Extend.Fields;

namespace Piranha.Extend.Blocks
{
    [BlockType(Name = "Authorized", Category = "Content",
    Icon = "fas fa-address-card")]
    public class AuthorizedBlock : Block
    {
        /// <summary>
        /// Gets/sets the text body.
        /// </summary>
        public TextField Body { get; set; }
    }
}

