using Piranha.AttributeBuilder;
using Piranha.Extend.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Magnus.Models.Regions
{
    public class Lab
    {
        [Field(Title = "City")]
        public StringField City { get; set; }

        [Field(Title = "University site url")]
        public StringField Url { get; set; }
        
        [Field(Title = "Image")]
        public ImageField Image { get; set; }
    }
}
