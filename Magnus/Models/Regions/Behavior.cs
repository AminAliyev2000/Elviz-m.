using Piranha.AttributeBuilder;
using Piranha.Extend.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Magnus.Models.Regions
{
    public class Behavior
    {
        [Field]
        public CheckBoxField IsHiddenInMenu { get; set; }

        [Field]
        public CheckBoxField IsPrivate { get; set; }
    }
}
