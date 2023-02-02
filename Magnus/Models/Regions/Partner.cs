using Piranha.AttributeBuilder;
using Piranha.Extend.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Magnus.Models.Regions
{
    public class Partner
    {
        [Field(Title = "Logo")]
        public ImageField Logo { get; set; }
        [Field(Title = "Contact is coordinating the programme")]
        public CheckBoxField IsCoordinating { get; set; }
        [Field(Title = "Contact person")]
        public StringField Person { get; set; }
        [Field(Title = "Contact is co-coordinating the programme")]
        public CheckBoxField IsCoCoordinating { get; set; }
        [Field(Title = "Contact e-mail")]
        public StringField Email { get; set; }
        [Field(Title = "University site url")]
        public StringField Url { get; set; }
        [Field(Title = "Name of the univ.")]
        public StringField University { get; set; }
        [Field(Title = "Russian name of the univ.")]
        public StringField UniversityRus { get; set; }
        [Field(Title = "Kazakhstan name of the univ.")]
        public StringField UniversityKaz { get; set; }
        [Field(Title = "Abbreviated name of the univ.")]
        public StringField UniversityAbr { get; set; }
        [Field(Title = "Country")]
        public StringField Country { get; set; }
        [Field(Title = "Russian name of country")]
        public StringField CountryRus { get; set; }
        [Field(Title = "Kazakhstan name of country")]
        public StringField CountryKaz { get; set; }
        [Field(Title = "Country Code for the map")]
        public StringField CountryCode { get; set; }
    }
}
