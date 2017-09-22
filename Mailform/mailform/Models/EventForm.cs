using System;
using System.Linq;
using System.Collections.Generic;


namespace Mailform.Models
{
    public class EventForm
    {
        public EventForm()
        {
            Title = "";
            Items = new Dictionary<string, string>();
        }

        public EventForm(EventTemplate template) : this()
        {
            Title = template.Title;
            Items = template.Items.ToDictionary(i => "");
        }

        public string Title { get; set; }

        public Dictionary<string, string> Items{ get; set; }
    }
}
