using Piranha.AttributeBuilder;
using Piranha.Data;
using Piranha.Extend.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Magnus.Models.Regions
{
    public class Languages
    {
        [Field]
        public TextField RusTitle { get; set; }

        [Field]
        public TextField KazTitle { get; set; }
    }
}
