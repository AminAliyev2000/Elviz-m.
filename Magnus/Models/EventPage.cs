using Magnus.Models.Regions;
using Piranha.AttributeBuilder;
using Piranha.Models;
using System.Collections.Generic;
using Piranha.Extend.Fields;

namespace Magnus.Models
{
    [PageType(Title = "Event page")]
    public class EventPage : Page<EventPage>
    {
        [Region]
        public EventDetails Details { get; set; }

        [Region]
        public IList<ImageField> Photos { get; set; }

        [Region]
        public IList<DocumentField> Downloads { get; set; }

        [Region]
        public HtmlField Brief { get; set; }

        [Region]
        public ImageField Banner { get; set; }

        [Region]
        public Language Language { get; set; }
    }

    public class Language
    {
        [Field(Title = "English")]
        public CheckBoxField English { get; set; }

        [Field(Title = "Russian")]
        public CheckBoxField Russian { get; set; }

        [Field(Title = "Kazakhstan")]
        public CheckBoxField Kazakhstan { get; set; }
    }

    public class EventDetails
    {
        [Field(Title = "Only for authorized")]
        public CheckBoxField OnlyForAuthorized { get; set; }

        [Field(Title = "Type of event")]
        public SelectField<EventType> TypeOfEvent { get; set; }

        [Field(Title = "Begin")]
        public DateField Start { get; set; }

        [Field(Title = "End")]
        public DateField Stop { get; set; }

        [Field(Title = "Place")]
        public StringField Place { get; set; }

        [Field(Title = "City code")]
        public StringField CountryCode { get; set; }
        
        [Field]
        public StringField RegistrationUrl { get; set; }

        [Field]
        public ImageField Image { get; set; }

    }

    public enum EventType
    {
        ProjectMeetings,
        SummerDays,
        TrainingWorkshops,
        DisseminationEvents
    }
}
